using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Upgrade Event", menuName = "Game Event")]

public class UpgradeEventSO : GameEventSO
{
    public UpgradeSO upgrade;
    public int threshold;
    public Ressources resourceMultiplier;
    public override bool CheckRequirements()
    {
        if (UpgradeManager.Instance.CheckUpgradeNumber(upgrade) > threshold)
        {
            return true;
        }
        else return false;
    }

    public override void ExecuteEvent()
    {
        base.ExecuteEvent();

    }
}
