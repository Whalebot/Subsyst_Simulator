using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Upgrade Event", menuName = "Game Event")]

public class UpgradeEventSO : GameEventSO
{
    public UpgradeSO upgrade;
    public int threshold;
    public int RNG;
    public GameEventSO triggerEvent;
    public override bool CheckRequirements()
    {
        if (UpgradeManager.Instance.CheckUpgradeNumber(upgrade) > threshold)
        {
           // Debug.Log("Upgrade Event");
            return true;
        }
        else return false;
    }

    public override void ExecuteEvent()
    {
        base.ExecuteEvent();
        int val = Random.Range(0, 100);
        if (val > RNG) {
            triggerEvent.ExecuteEvent();
            EventManager.Instance.triggeredGameEvents.Add(this);
            EventManager.Instance.pendingGameEvents.Remove(this);
        }

    }
}
