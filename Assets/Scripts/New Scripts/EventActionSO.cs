using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Event Action", menuName = "EventAction")]
public class EventActionSO : ActionSO
{
    public GameEventSO eventSO;
}
