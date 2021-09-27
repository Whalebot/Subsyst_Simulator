using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ressource Event", menuName = "Game Event/Ressource Event")]

public class RessourceEventSO : GameEventSO
{
    public Ressources positiveThresholds;
    public Ressources negativeThresholds;
    public GameEventSO triggerEvent;
    public override bool CheckRequirements()
    {

        return GameManager.Instance.CheckRessources(positiveThresholds) && GameManager.Instance.CheckMissingRessources(negativeThresholds);
    }

    public override void ExecuteEvent()
    {
        base.ExecuteEvent();
        if (triggerEvent != null)
            if (type == EventType.AddToPending)
            {
                EventManager.Instance.pendingGameEvents.Add(triggerEvent);
            }
            else
                triggerEvent.ExecuteEvent();

    }
}
