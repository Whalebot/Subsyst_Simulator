using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New AI", menuName = "AI")]
public class AIBehaviour : ScriptableObject
{
    public int populationGoal;
    public UpgradeSO[] upgradeGoals;
    public ActionSO[] bannedActions;
    public ActionSO[] preferredMoneyProduction;
    public ActionSO[] preferredFoodProduction;
    public ActionSO[] preferredEnergyProduction;
    public ActionSO[] preferredWasteProduction;
}
