using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Replace Event", menuName = "Game Event/Replace Event")]

public class ReplaceUpgradeEventSO : GameEventSO
{
    public ActionSO[] oldActions;
    public ActionSO[] newActions;

    public override bool CheckRequirements()
    {
        return true;
    }

    public override void ExecuteEvent()
    {
        base.ExecuteEvent();
        for (int i = 0; i < oldActions.Length; i++)
        {
            MenuManager.Instance.ReplaceAction(oldActions[i], newActions[i]);
            FoodManager.Instance.ReplaceAction(oldActions[i], newActions[i]);
        }
      
    }
}

