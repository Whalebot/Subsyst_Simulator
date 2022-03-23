using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGameObject : MonoBehaviour
{
    public GameEventSO gameEvent;
    public GameObject activatedObject;
    public GameObject deactivatedObject;
    public GameEventSO resetEvent;

    void Start()
    {
        EventManager.Instance.cataclysmTrigger += TriggerEvent;
        EventManager.Instance.cataclysmTrigger += ResetEvent;
        TimeManager.Instance.advanceGameEvent += ExecuteFrame;
    }

    void ExecuteFrame()
    {
    }

    void TriggerEvent(GameEventSO p)
    {
        if (p == gameEvent)
        {
            activatedObject.SetActive(true);
            deactivatedObject.SetActive(false);
        }
    }

    void ResetEvent(GameEventSO p)
    {
        if (p == resetEvent && resetEvent != null)
        {
            activatedObject.SetActive(false);
            deactivatedObject.SetActive(true);
        }
    }
}
