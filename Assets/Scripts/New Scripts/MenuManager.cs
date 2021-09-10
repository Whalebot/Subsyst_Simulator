using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    public GameObject foodTab;
    public GameObject energyTab;
    public GameObject wasteTab;
    public GameObject populationTab;
    public MenuButton foodButton;
    public MenuButton energyButton;
    public MenuButton wasteButton;

    public GameObject meatTab;
    public GameObject vegetableTab;
    public GameObject insectTab;
    public GameObject algaeTab;
    public FoodTabButton meatButton;
    public FoodTabButton vegetableButton;
    public FoodTabButton insectButton;
    public FoodTabButton algaeButton;

    //    public List<Interactable> interactables;
    public Interactable[] interactables;
    public UpgradeManager upgradeManager;
    public DescriptionWindow productionDescriptionWindow;
    public DescriptionWindow upgradeDescriptionWindow;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    [Button]
    public void FindAllInteractables()
    {
        interactables = (Interactable[])GameObject.FindObjectsOfType<Interactable>(true);
        foreach (var item in interactables)
        {
            if (item.GetType() == typeof(ActionButton))
            {
                ActionButton a = item.GetComponent<ActionButton>();
                if (a.action.GetType() == typeof(ProductionSO))
                {
                    if (item.GetComponent<ActionButton>().requiredUpgrade != null)
                    { }
                    else
                        upgradeManager.UnlockAction(item.GetComponent<ActionButton>().action);
                }

            }
        }
    }

    public void ReplaceAction(ActionSO oldAction, ActionSO newAction)
    {
        foreach (var item in interactables)
        {
            if (item.action == oldAction)
            {
                item.action = newAction;
            }
        }

    }

    public void DisplayDescriptionWindow(ActionSO a)
    {
        if (a == null) return;
        if (a.GetType() == typeof(ProductionSO))
        {
            productionDescriptionWindow.gameObject.SetActive(true);
            productionDescriptionWindow.UpdateDescription((ProductionSO)a);
        }
        else if (a.GetType() == typeof(UpgradeSO))
        {
            upgradeDescriptionWindow.gameObject.SetActive(true);
            upgradeDescriptionWindow.UpdateDescription((UpgradeSO)a);
        }
    }
    public void HideDescriptionWindow()
    {

        productionDescriptionWindow.gameObject.SetActive(false);
        upgradeDescriptionWindow.gameObject.SetActive(false);

    }
    public Interactable FindInteractable(ActionSO a)
    {
        if (a == null) return null;
        Interactable temp = null;

        foreach (Interactable item in interactables)
        {
            if (item.action == a)
            {
                temp = item;
            }
        }

        if (!temp.gameObject.activeInHierarchy)
        {

            if (temp.transform.IsChildOf(foodTab.transform))
            {
                if (!foodTab.activeInHierarchy)
                {
                    temp = foodButton;
                }
                else
                {
                    if (temp.transform.IsChildOf(meatTab.transform)) { temp = meatButton; }
                    else if (temp.transform.IsChildOf(vegetableTab.transform)) { temp = vegetableButton; }
                    else if (temp.transform.IsChildOf(insectTab.transform)) { temp = insectButton; }
                    else if (temp.transform.IsChildOf(algaeTab.transform)) { temp = algaeButton; }
                }
            }
            if (temp.transform.IsChildOf(wasteTab.transform)) { temp = wasteButton; }
            if (temp.transform.IsChildOf(energyTab.transform)) { temp = energyButton; }
        }

        return temp;
    }

    public void CloseFoodTabs()
    {
        meatTab.SetActive(false);
        vegetableTab.SetActive(false);
        insectTab.SetActive(false);
        algaeTab.SetActive(false);
    }


    public void CloseAllTabs()
    {
        foodTab.SetActive(false);
        energyTab.SetActive(false);
        wasteTab.SetActive(false);
        populationTab.SetActive(false);
    }
}
