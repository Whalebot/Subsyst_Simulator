using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ressource Event", menuName = "Game Event/Ressource Event")]

public class RessourceEventSO : GameEventSO
{

    public override bool CheckRequirements()
    {
        return true;
    }

    public override void ExecuteEvent()
    {
        base.ExecuteEvent();


    }
}
