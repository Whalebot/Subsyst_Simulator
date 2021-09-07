using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDelayer : MonoBehaviour
{
    public Tut2 mainScreenManager;
    public ArrowActivator arrowAct;
    public float waitTime;

    public void Start()
    {
        StartCoroutine(BgDelayCoroutine());

    }

    IEnumerator BgDelayCoroutine()
    {
        // FIRST WAIT.        
        yield return new WaitForSeconds(waitTime);
        mainScreenManager.Z1SwitchTxt();
        yield return new WaitForSeconds(waitTime/5);
        arrowAct.ZActivateArrow();

    }
}
