using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AI : MonoBehaviour
{
    public static AI Instance;
    public bool isAIActive;

    public bool showAIActions;

    [InlineEditor] public AIBehaviour behaviour;
    public int upgradeGoalCount;
    public UpgradeSO upgradeGoal;
    public bool foundAllUpgrades;
    public ActionSO nextAction;
    public AIBehaviour[] bots;
    public GameObject botCursor;
    public AICursor cursorScript;
    public Camera mainCam;
    public bool turboAI;
    public int overshoot = 5;
    public int overshootCounter = 0;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.advanceTimeEvent += CalculateNextAction;
        UpgradeManager.Instance.upgradeEvent += CheckUpgrade;
    }


    public void ToggleAI()
    {
        isAIActive = !isAIActive;
        cursorScript.gameObject.SetActive(isAIActive);
    }

    public void SelectAI(int i)
    {
        if (i == -1)
        {
            isAIActive = false;
            return;
        }
        behaviour = bots[i];
        isAIActive = true;
        upgradeGoalCount = 0;
        FindNextUpgradeGoal();
    }

    [Button]
    public void CalculateNextAction()
    {
        if (!isAIActive) return;

        if (overshootCounter > 0)
        {
            overshootCounter--;
            MoveCursorToNextAction();
            return;
        }

        //If sustainable, check you can afford upkeep before trying to upgrade
        if (behaviour.sustainable)
        {
        
            //Returns true if player can't afford upkeep
            if (CheckAutomaticUpkeep())
            {
                print("Can't afford upkeep");
                MoveCursorToNextAction();
                return;
            }

            //If population is smaller than goal, prioritize food
            if (GameManager.Instance.Population < behaviour.populationGoal)
            {
                if (GameManager.Instance.Food < GameManager.Instance.Population)
                {
                    print("Lacking food");
                    Ressources productionCost = UpgradeManager.Instance.CheckCost(behaviour.preferredFoodProduction[0]);
                    if (GameManager.Instance.CheckRessources(productionCost))
                    {
                        nextAction = behaviour.preferredFoodProduction[0];
                        MoveCursorToNextAction();
                        return;
                    }
                    else
                    {
                        FindProductionMethods(behaviour.preferredFoodProduction[0]);
                        MoveCursorToNextAction();
                        return;
                    }
                }
            }

            if (behaviour.maxPollution < GameManager.Instance.Pollution)
            {
                for (int j = 0; j < behaviour.preferredWasteProduction.Length; j++)
                {
                    if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredWasteProduction[j]) && GameManager.Instance.CheckRessources(UpgradeManager.Instance.CheckCost(behaviour.preferredWasteProduction[j])))
                    {
                        MoveCursorToNextAction();
                        nextAction = behaviour.preferredWasteProduction[j];
                        print("Trying to clean up");
                        break;
                    }
                }
            }
        }

       
        if (foundAllUpgrades)
        {
            CheckAutomaticUpkeep();
            MoveCursorToNextAction();
            return;
        }

        Ressources tempCost = UpgradeManager.Instance.CheckCost(upgradeGoal);
        //Check if AI can afford next upgrade, if yes, next action is buying the upgrade.
        if (GameManager.Instance.CheckRessources(tempCost))
        {
            print("Can afford upgrade");
            nextAction = upgradeGoal;
        }
        //Check what ressources the AI is missing
        else
        {
            print("Can't afford upgrade, look for shit");
            FindProductionMethods(upgradeGoal);
        }

        MoveCursorToNextAction();
    }

    void CheckUpgrade(ActionSO a)
    {
        if (upgradeGoal == (UpgradeSO)a)
        {
            FindNextUpgradeGoal();
        }
    }

    public bool CheckAutomaticUpkeep()
    {
        bool[] temp = GameManager.Instance.FindMissingRessources(FoodManager.Instance.Upkeep());
        ActionSO tempAction = (ActionSO)ScriptableObject.CreateInstance("ActionSO");

        for (int i = 0; i < 9; i++)
        {
            bool found = false;
            if (!temp[i]) continue;
            switch (i)
            {
                case 0:
                    print("Missing Energy for upkeep");
                    for (int j = 0; j < behaviour.preferredEnergyProduction.Length; j++)
                    {
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredEnergyProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredEnergyProduction[j];
                            break;
                        }
                    }
                    break;
                case 1:
                    print("Missing Food for upkeep");
                    for (int j = 0; j < behaviour.preferredFoodProduction.Length; j++)
                    {
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredFoodProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredFoodProduction[j];
                            break;
                        }
                    }
                    break;
                case 2:
                    print("Missing Waste for upkeep");
                    for (int j = 0; j < behaviour.preferredWasteProduction.Length; j++)
                    {
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredWasteProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredWasteProduction[j];
                            break;
                        }
                    }
                    break;
                case 5:
                    print("Missing Money for upkeep");
                    for (int j = 0; j < behaviour.preferredMoneyProduction.Length; j++)
                    {
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredMoneyProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredMoneyProduction[j];
                            break;
                        }
                    }
                    break;

                default:
                    print("Missing something for upkeep");
                    break;
            }
            if (found)
            {
                Ressources productionCost = UpgradeManager.Instance.CheckCost(tempAction);

                if (GameManager.Instance.CheckRessources(productionCost))
                {
                    print("Can't afford upkeep, trying to: " + tempAction);
                    nextAction = tempAction;
                    if (tempAction.GetType() == typeof(ProductionSO)) overshootCounter = overshoot;
                    return true;
                }
                else
                {
                    FindProductionMethodsNoLoop(tempAction);
                    return true;
                }
            }
        }
        // print("Can afford upkeep");
        return false;
    }

    public void FindProductionMethods(ActionSO a)
    {
        Ressources tempCost = UpgradeManager.Instance.CheckCost(a);
        bool[] temp = GameManager.Instance.FindMissingRessources(tempCost);

        ActionSO tempAction = (ActionSO)ScriptableObject.CreateInstance("ActionSO");

        for (int i = 0; i < 9; i++)
        {
            bool found = false;

            if (!temp[i]) continue;
            switch (i)
            {
                case 0:
                    for (int j = 0; j < behaviour.preferredEnergyProduction.Length; j++)
                    {
                        print("Looking for energy");
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredEnergyProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredEnergyProduction[j];
                            break;
                        }
                    }
                    break;
                case 1:
                    for (int j = 0; j < behaviour.preferredFoodProduction.Length; j++)
                    {
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredFoodProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredFoodProduction[j];
                            break;
                        }
                    }
                    break;
                case 2:
                    for (int j = 0; j < behaviour.preferredWasteProduction.Length; j++)
                    {
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredWasteProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredWasteProduction[j];
                            break;
                        }
                    }
                    break;
                case 5:
                    for (int j = 0; j < behaviour.preferredMoneyProduction.Length; j++)
                    {
                        print("Looking for money");
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredMoneyProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredMoneyProduction[j];
                            break;
                        }
                    }
                    break;

                default:
                    print("Missing something for production");
                    break;
            }
            if (found)
            {
                Ressources productionCost = UpgradeManager.Instance.CheckCost(tempAction);
                print("Found missing production: " + tempAction);
                if (GameManager.Instance.CheckRessources(productionCost))
                {
                    nextAction = tempAction;
                    if (tempAction.GetType() == typeof(ProductionSO)) overshootCounter = overshoot;

                    return;
                }
                else
                {
                    print("Looking for shit to: " + tempAction);
                    FindProductionMethodsNoLoop(tempAction);
                }
            }
        }
    }

    public void FindProductionMethodsNoLoop(ActionSO a)
    {
        Ressources tempCost = UpgradeManager.Instance.CheckCost(a);
        bool[] temp = GameManager.Instance.FindMissingRessources(tempCost);

        ActionSO tempAction = (ActionSO)ScriptableObject.CreateInstance("ActionSO");
        bool missing = false;
        for (int i = 0; i < 9; i++)
        {
            bool found = false;
            if (!temp[i]) continue;
            switch (i)
            {
                case 0:
                    for (int j = 0; j < behaviour.preferredEnergyProduction.Length; j++)
                    {
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredEnergyProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredEnergyProduction[j];
                            break;
                        }
                    }
                    break;
                case 1:
                    for (int j = 0; j < behaviour.preferredFoodProduction.Length; j++)
                    {
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredFoodProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredFoodProduction[j];
                            break;
                        }
                    }
                    break;
                case 2:
                    for (int j = 0; j < behaviour.preferredWasteProduction.Length; j++)
                    {
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredWasteProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredWasteProduction[j];
                            break;
                        }
                    }
                    break;
                case 5:
                    for (int j = 0; j < behaviour.preferredMoneyProduction.Length; j++)
                    {
                        if (UpgradeManager.Instance.unlockedActions.Contains(behaviour.preferredMoneyProduction[j]))
                        {
                            found = true;
                            tempAction = behaviour.preferredMoneyProduction[j];
                            break;
                        }
                    }
                    break;

                default:
                    print("Missing something for production");
                    break;
            }

            if (found)
            {
                missing = true;
                Ressources productionCost = UpgradeManager.Instance.CheckCost(tempAction);

                if (GameManager.Instance.CheckRessources(productionCost))
                {
                    nextAction = tempAction;
                    if (tempAction.GetType() == typeof(ProductionSO)) overshootCounter = overshoot;

                    return;
                }
            }
        }
        if (missing)
        {
            nextAction = null;
            print("Wait");
        }
    }

    [Button]
    public void MoveCursorToNextAction()
    {
        if (nextAction == null)
        {
            print("Bot is waiting");
            return;
        }
        Interactable temp = MenuManager.Instance.FindInteractable(nextAction);
        if (!showAIActions)
            temp.ExecuteAction();
        else
        {
            if (temp.GetType() == typeof(MenuButton))
                StartCoroutine("MoveAICursorWorld", (MenuButton)temp);
            else
                StartCoroutine("MoveAICursor", temp);
        }
    }

    IEnumerator MoveAICursor(Interactable t)
    {
        float distance = Vector2.Distance(botCursor.transform.position, t.transform.position);
        LeanTween.move(botCursor.gameObject, t.transform.position, (TimeManager.Instance.framesPerTime / 120F));
        while (distance > 2)
        {
            distance = Vector2.Distance(botCursor.transform.position, t.transform.position);
            yield return null;
        }

        StartCoroutine("SimulateClick", t.gameObject);
        //t.ExecuteAction();
    }


    IEnumerator MoveAICursorWorld(MenuButton t)
    {
        float distance = Vector2.Distance(mainCam.WorldToScreenPoint(botCursor.transform.position), mainCam.WorldToScreenPoint(t.transform.position));
        LeanTween.move(botCursor.gameObject, mainCam.WorldToScreenPoint(t.transform.position), (TimeManager.Instance.framesPerTime / 90F));
        while (distance > 2)
        {
            distance = Vector2.Distance(botCursor.transform.position, mainCam.WorldToScreenPoint(t.transform.position));
            yield return null;
        }
        StartCoroutine("SimulateClick", t.gameObject);
    }

    IEnumerator SimulateClick(GameObject g)
    {
        if (cursorScript.gameObject.activeInHierarchy)
            cursorScript.PerformClick();
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(g, pointer, ExecuteEvents.pointerEnterHandler);
        ExecuteEvents.Execute(g, pointer, ExecuteEvents.pointerDownHandler);


        yield return new WaitForSeconds(0.1F);
        //print(g); g.GetComponent<Interactable>().ExecuteAction();
        ExecuteEvents.Execute(g, pointer, ExecuteEvents.submitHandler);
        ExecuteEvents.Execute(g, pointer, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.Execute(g, pointer, ExecuteEvents.pointerExitHandler);


    }

    public void FindNextUpgradeGoal()
    {
        if (foundAllUpgrades) return;
        if (upgradeGoalCount >= behaviour.upgradeGoals.Length)
        {
            foundAllUpgrades = true;
            upgradeGoal = null;
            print("All Upgrades Found");
        }
        else
        {
            upgradeGoal = behaviour.upgradeGoals[upgradeGoalCount];
            upgradeGoalCount++;
        }
    }

    public void CalculateOptimalProductionMethod()
    {
        //Optimize for money
        List<ProductionSO> validProductions = new List<ProductionSO>();
        ProductionSO bestProduction = null;
        for (int i = 0; i < validProductions.Count; i++)
        {
            if (bestProduction == null)
            {
                bestProduction = validProductions[i];
                continue;
            }
            int gained = validProductions[i].result.money - validProductions[i].cost.money;
            if (gained > bestProduction.result.money - bestProduction.cost.money) bestProduction = validProductions[i];
        }
    }

}
