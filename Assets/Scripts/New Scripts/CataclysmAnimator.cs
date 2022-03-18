using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CataclysmAnimator : MonoBehaviour
{
    public GameEventSO cataclysm;
    public GameEventSO resetEvent;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EventManager.Instance.cataclysmTrigger += TriggerEvent;
        EventManager.Instance.cataclysmTrigger += ResetEvent;
        TimeManager.Instance.advanceGameEvent += ExecuteFrame;
    }

    void ExecuteFrame()
    {
    }

    void TriggerEvent(GameEventSO p)
    {
        if (p == cataclysm)
        {
            anim.SetBool("isCataclysm", true);
        }
    }

    void ResetEvent(GameEventSO p)
    {
        if (p == resetEvent && resetEvent != null)
        {
            anim.SetBool("isCataclysm", false);
        }
    }
}
