using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;


public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public enum UpgradeScaling { Multiplication, Additive }
    public UpgradeScaling upgradeScaling;

    public List<UpgradeSO> allUpgrades;
    public List<ProductionSO> allProduction;
    public List<ProductionSO> availableProduction;
    public List<ActionSO> obtainedUpgrades;
    public List<ActionSO> unlockedActions;

    public delegate void ActionEvent(ActionSO a);
    public ActionEvent upgradeEvent;

    public List<ActionSO> meatUpgrades;
    public List<ActionSO> agricultureUpgrades;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    //[Button]
    //void LoadProduction()
    //{
    //    string[] assetNames = AssetDatabase.FindAssets("t:ProductionSO", new[] { "Assets/ScriptableObjects" });


    //    foreach (string SOName in assetNames)
    //    {
    //        var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
    //        var item = AssetDatabase.LoadAssetAtPath<ProductionSO>(SOpath);
    //        allProduction.Add(item);
    //    }

    //    string[] assetNames2 = AssetDatabase.FindAssets("t:UpgradeSO", new[] { "Assets/ScriptableObjects" });


    //    foreach (string SOName in assetNames2)
    //    {
    //        var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
    //        var item = AssetDatabase.LoadAssetAtPath<UpgradeSO>(SOpath);
    //        allUpgrades.Add(item);
    //    }
    //}
    public int MeatUpgrades()
    {
        int sum = 0;
        foreach (var item in meatUpgrades)
        {
            sum += CheckUpgradeNumber(item);
        }
        return sum;
    }
    public int VegetableUpgrades()
    {
        int sum = 0;
        foreach (var item in agricultureUpgrades)
        {
            sum += CheckUpgradeNumber(item);
        }
        return sum;
    }

    public int CheckUpgradeNumber(ActionSO p)
    {
        int upgradeNumber = 0;
        foreach (var item in obtainedUpgrades)
        {
            if (item == p) upgradeNumber++;
        }
        return upgradeNumber;
    }
    public Ressources CheckCost(ProductionSO p)
    {
          int upgradeNumber = CheckUpgradeNumber(p);
       // print(p + " " + upgradeNumber);
        return p.upgradeLevels[upgradeNumber].cost;
        //Ressources temp = new Ressources();


        //GameManager.Instance.SetRessources(p.upgradeLevels[upgradeNumber].cost, temp);
        //for (int i = 0; i < upgradeNumber; i++)
        //{
        //    if (upgradeScaling == UpgradeScaling.Multiplication)
        //        GameManager.Instance.MultiplyRessources(temp, 1);
        //    else GameManager.Instance.AddRessources(temp, p.upgradeLevels[upgradeNumber].cost);
        //}
        //return temp;
    }
    public Ressources CheckResult(ProductionSO p)
    {
        int upgradeNumber = CheckUpgradeNumber(p);
        return p.upgradeLevels[upgradeNumber].result;
        //Ressources temp = new Ressources();
        //GameManager.Instance.SetRessources(p.upgradeLevels[upgradeNumber].result, temp);
        //for (int i = 0; i < upgradeNumber; i++)
        //{
        //    if (upgradeScaling == UpgradeScaling.Multiplication)
        //        GameManager.Instance.MultiplyRessources(temp, 1);
        //    else GameManager.Instance.AddRessources(temp, p.upgradeLevels[upgradeNumber].result);
        //}
        //return temp;
    }

    public void UnlockAction(ActionSO p)
    {
        unlockedActions.Add(p);
        if (p.GetType() == typeof(UpgradeSO))
        {


        }
        else if (p.GetType() == typeof(ProductionSO))
        {

        }


    }

    //Does not update game state
    public void UnlockUpgrade(ActionSO p)
    {

        if (!TutorialScript.Instance.inTutorial)
            TimeManager.isStarted = true;
        //if (!GameManager.Instance.CheckRessources(p))
        //{
        //    print("ERROR: BUTTON SHOULD BE UNAVAILABLE, Can't afford upgrade " + p.name);
        //    return;
        //}

        obtainedUpgrades.Add(p);

        if (p.GetType() == typeof(UpgradeSO))
        {
            UpgradeSO u = (UpgradeSO)p;
            GameManager.Instance.Money -= u.price;
        }
        else if (p.GetType() == typeof(ProductionSO))
        {
            ProductionSO u = (ProductionSO)p;
            GameManager.Instance.Money -= u.upgradeLevels[CheckUpgradeNumber(u)].upgradeCost;
        }
        upgradeEvent?.Invoke(p);
        GameManager.Instance.updateGameState?.Invoke();
    }

    //Does not update game state
    public void FreeUpgrade(ActionSO p)
    {
        if (!TutorialScript.Instance.inTutorial)
            TimeManager.isStarted = true;

        obtainedUpgrades.Add(p);
        upgradeEvent?.Invoke(p);
        GameManager.Instance.updateGameState?.Invoke();
    }
}
