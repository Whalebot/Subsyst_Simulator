using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New AI", menuName = "AI")]
public class AIBehaviour : ScriptableObject
{
    [Header("Read Only")]
    public int upgradeStep;
    [Header("Settings")]
    public int populationGoal;
    public int maxPollution;
    public bool sustainable;
    public UpgradeSO[] upgradeGoals;
    public ActionSO[] preferredMoneyProduction;
    public ActionSO[] preferredFoodProduction;
    public ActionSO[] preferredEnergyProduction;
    public ActionSO[] preferredWasteProduction;
}
