using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Replace Event", menuName = "Game Event/Event Reward")]

public class EventRewardSO : GameEventSO
{
    public Ressources reward;
    public override bool CheckRequirements()
    {
        return true;
    }

    public override void ExecuteEvent()
    {
        base.ExecuteEvent();
        GameManager.Instance.AddRessources(reward);
    }
}

