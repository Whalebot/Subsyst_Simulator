using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public float tutorialCounter;

    public float tabsOut;
    public float tabsIn;

    public float inDistance;
    public float outDistance;
    public Vector3 scaleInitial;
    public Vector3 scaleFinal;
    public float tiempo;
    public float tiempoRapido;
    public float delay;
    public float fastDelay;
    public AnimationCurve curve;
    public AnimationCurve normalCurve;

    public GameObject resourceTabs;
    public GameObject milestoneTabs;

    public GameObject[] startScreen;
    public GameObject[] produceScreen;
    public GameObject[] foodRunsOutScreen;
    public GameObject[] produceEnergyScreen;
    public GameObject[] produceMoreFoodScreen;
    public GameObject[] pollutionScreen;
    public GameObject[] approvalScreen;
    public GameObject[] populationScreen;
    public GameObject[] upgradesScreen;
    public GameObject[] playScreen;

    public void Start()
    {
        startScreen[0].gameObject.SetActive(true);

        scaleInitial = new Vector3(0f, 0f, 1f);
        scaleFinal = new Vector3(1f, 1f, 1f);

        LeanTween.moveLocalY(resourceTabs, tabsOut, 0f);
        LeanTween.moveLocalY(milestoneTabs, tabsOut, 0f);


        //LeanTween.scale(produceScreen[0], scaleInitial, 0f);
    }

    public void Screen0()
    {
        LeanTween.moveLocalY(startScreen[0], outDistance, tiempo).setEase(curve);
        Screen1();
    }

    public void Screen1()
    {
        LeanTween.scale(produceScreen[0], scaleFinal, tiempo).setDelay(delay).setEase(normalCurve);

    }

    public void Screen2()
    {
        LeanTween.scale(produceScreen[0], scaleInitial, tiempoRapido).setEase(normalCurve);
        LeanTween.scale(produceScreen[1], scaleFinal, tiempo).setDelay(fastDelay).setEase(normalCurve);


    }

    public void Screen3()
    {
        LeanTween.scale(produceScreen[1], scaleInitial, tiempoRapido).setEase(normalCurve);
        LeanTween.moveLocalY(resourceTabs, tabsIn, 1f).setDelay(1.5f).setEase(normalCurve);
        LeanTween.moveLocalY(milestoneTabs, tabsIn, 1f).setDelay(1.5f).setEase(normalCurve);
        LeanTween.scale(produceScreen[2], scaleFinal, tiempo).setDelay(2.5f).setEase(normalCurve);
        //LeanTween.scale(produceScreen[3], scaleFinal, tiempo).setDelay(3.5f).setEase(normalCurve);
        StartCoroutine(TurnOnArrow1());



    }

    IEnumerator TurnOnArrow1()
    {

        yield return new WaitForSeconds(3.5f);
        produceScreen[3].gameObject.SetActive(true);




    }
}
