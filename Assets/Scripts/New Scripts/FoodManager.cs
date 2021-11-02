using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class FoodManager : BaseFacility
{
    public static FoodManager Instance { get; private set; }
    public float foodMultiplier = 1;
    public List<ProductionSO> productionTypes;
    public List<ProductionSO> unlockedAutomaticProductionTypes;
    public Ressources upkeep;
    public Ressources income;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.advanceTimeEvent += UpdateUpkeep;
        TimeManager.Instance.advanceGameEvent += AdvanceGameState;
    }



    //Executes the production and updates game state
    [Button]
    public void ExecuteProduction(ProductionSO p)
    {
        Ressources tempCost = UpgradeManager.Instance.CheckCost(p);
        if (!GameManager.Instance.CheckRessources(tempCost))
        {
            print("ERROR: BUTTON SHOULD BE UNAVAILABLE, Can't afford production " + p.name);
            return;
        }
        GameManager.Instance.SubtractRessources(tempCost);
        Ressources tempResult = UpgradeManager.Instance.CheckResult(p);
        GameManager.Instance.AddRessources(tempResult);
        GameManager.Instance.updateGameState?.Invoke();
    }

    //Does not update game state
    public void ExecuteAutomaticProduction(ProductionSO p)
    {
        Ressources tempCost = UpgradeManager.Instance.CheckCost(p);
        if (!GameManager.Instance.CheckRessources(tempCost))
        {
            print("ERROR: AUTOMATION SHOULD BE UNAVAILABLE, Can't afford production " + p.name);
            return;
        }
        GameManager.Instance.SubtractRessources(tempCost);
        Ressources tempResult = UpgradeManager.Instance.CheckResult(p);
        GameManager.Instance.AddRessources(tempResult);
    }

    public void AddAutomaticProduction(ProductionSO p)
    {
        unlockedAutomaticProductionTypes.Add(p);
    }

    public void RemoveAutomaticProduction(ProductionSO p)
    {
        unlockedAutomaticProductionTypes.Remove(p);
    }
    public int CheckProductionNumber(ProductionSO p)
    {
        int upgradeNumber = 0;
        foreach (var item in unlockedAutomaticProductionTypes)
        {
            if (item == p) upgradeNumber++;
        }
        return upgradeNumber;
    }

    public void ReplaceAction(ActionSO oldActions, ActionSO newActions)
    {
        if (unlockedAutomaticProductionTypes.Contains((ProductionSO)oldActions))
        {
            unlockedAutomaticProductionTypes.Remove((ProductionSO)oldActions);
            unlockedAutomaticProductionTypes.Add((ProductionSO)newActions);
        }
    }

    public void RemoveAllAutomaticProduction(ProductionSO p)
    {
        int a = CheckProductionNumber(p);
        for (int i = 0; i < a; i++)
        {
            unlockedAutomaticProductionTypes.Remove(p);
        }
    }


    public void CalculateAutomation()
    {
        Ressources sumCost = new Ressources();
        Ressources sumResult = new Ressources();
        foreach (var item in unlockedAutomaticProductionTypes)
        {
            GameManager.Instance.AddRessources(sumCost, item.cost);
            GameManager.Instance.AddRessources(sumResult, item.result);
        }
        Ressources profit = new Ressources();
        GameManager.Instance.SubtractRessources(sumResult, sumCost);
    }

    //    private static bool IsEqualTo(ProductionSO p){
    //        return 
    //}

    public Ressources Upkeep()
    {
        upkeep = new Ressources();
        income = new Ressources();
        foreach (var item in unlockedAutomaticProductionTypes)
        {
            Ressources cost = UpgradeManager.Instance.CheckCost(item);
            Ressources result = UpgradeManager.Instance.CheckResult(item);
            GameManager.Instance.AddRessources(upkeep, cost);
            GameManager.Instance.AddRessources(income, result);
        }
        income.money = GameManager.Instance.Population;
        GameManager.Instance.AddRessources(upkeep, EventManager.Instance.populationUpkeep);

        return upkeep;
    }

    public bool CheckUpkeep()
    {
        bool[] missingRessources = GameManager.Instance.FindMissingRessources(upkeep, income);
        bool foundMissing = false;
        for (int i = 0; i < missingRessources.Length; i++)
        {
            if (missingRessources[i] && i != 6) foundMissing = true;
        } 
        return !foundMissing;
    }

    public void UpdateUpkeep()
    {
        Upkeep();
    }

    public override void AdvanceGameState()
    {
        base.AdvanceGameState();
        foreach (var item in unlockedAutomaticProductionTypes)
        {
            ExecuteAutomaticProduction(item);
        }
    }
}
