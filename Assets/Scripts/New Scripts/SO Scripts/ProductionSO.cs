using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Production Method", menuName = "Production")]
public class ProductionSO : ActionSO
{
    public int upgradeLimit = 3;
    public UpgradeLevel[] upgradeLevels;
}
