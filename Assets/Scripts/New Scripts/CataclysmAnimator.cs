using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CataclysmAnimator : MonoBehaviour
{
    public GameEventSO cataclysm;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EventManager.Instance.cataclysmTrigger += TriggerEvent;
        TimeManager.Instance.advanceGameEvent += ExecuteFrame;
    }

    void ExecuteFrame() { 
    }

    void TriggerEvent(GameEventSO p) {
        if (p == cataclysm || EventManager.Instance.triggeredGameEvents.Contains(cataclysm)) {
            anim.SetBool("isCataclysm", true); 
        }
    }
}
