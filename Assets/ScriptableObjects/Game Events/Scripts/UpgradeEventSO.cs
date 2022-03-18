using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Upgrade Event", menuName = "Game Event/Upgrade Event")]

public class UpgradeEventSO : GameEventSO
{
    public UpgradeSO upgrade;
    public UpgradeSO preventionUpgrade;

    public int threshold;
    public int RNG = 1000;
    public GameEventSO triggerEvent;


    public override bool CheckRequirements()
    {
        if (UpgradeManager.Instance.obtainedUpgrades.Contains(preventionUpgrade)) return false;
        if (UpgradeManager.Instance.CheckUpgradeNumber(upgrade) > threshold + thresholdCounter)
        {
            int val = Random.Range(0, 100);

            return true;
            //if (val < RNG)
            //{
            //    thresholdCounter = 0;
            //    return true;
            //}
            //else
            //{
            //    thresholdCounter++;

            //    return false;
            //}
        }
        else return false;
    }

    public override void ExecuteEvent()
    {
        base.ExecuteEvent();

        if (type == EventType.AddToPending)
        {
            EventManager.Instance.pendingGameEvents.Add(triggerEvent);
            triggerEvent.thresholdCounter = UpgradeManager.Instance.CheckUpgradeNumber(upgrade) - threshold;
        }
        else
            triggerEvent.ExecuteEvent();


    }
}
