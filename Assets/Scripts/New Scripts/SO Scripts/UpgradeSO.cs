using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeSO : ActionSO
{
    public int price;
    public int upgradeLimit = 1;
    public ProductionSO unlockedProduction;
}
