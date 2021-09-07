using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialColorTabs : MonoBehaviour
{

    public bool timeIsRunning;

    public float waitTime1;
    public float waitTime2;
    public float waitTime3;
    public float waitTime4;
    public float waitTime5;
    public float waitTime6;
    public float waitTime7;

    public float microWait;

    public TextMeshProUGUI food;
    public TextMeshProUGUI ener;
    public TextMeshProUGUI wast;


    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
        StartCoroutine(TimeScheduleCoroutine());
    }

    IEnumerator TimeScheduleCoroutine()
    {

        yield return new WaitForSeconds(waitTime1);


        while (timeIsRunning == true)
        {
            yield return new WaitForSeconds(waitTime2);

            //4 microWaits;
            yield return new WaitForSeconds(microWait);
            food.text = "425.0";
            ener.text = "160.0";
            yield return new WaitForSeconds(microWait);
            food.text = "320.0";
            ener.text = "320.0";
            yield return new WaitForSeconds(microWait);
            food.text = "160.0";
            ener.text = "425.0";
            yield return new WaitForSeconds(microWait);
            food.text = "75.0";
            ener.text = "500.0";

            yield return new WaitForSeconds(waitTime3 - (4*microWait));
            yield return new WaitForSeconds(waitTime4);

            yield return new WaitForSeconds(microWait);
            ener.text = "425.0";
            wast.text = "160.0";

            yield return new WaitForSeconds(microWait);
            ener.text = "320.0";
            wast.text = "320.0";

            yield return new WaitForSeconds(microWait);
            ener.text = "160.0";
            wast.text = "425.0";

            yield return new WaitForSeconds(microWait);
            ener.text = "75.0";
            wast.text = "500.0";

            yield return new WaitForSeconds(waitTime5 - (4*microWait));
            yield return new WaitForSeconds(waitTime6);

            yield return new WaitForSeconds(microWait);
            wast.text = "425.0";
            food.text = "160.0";
            ener.text = "75.0";

            yield return new WaitForSeconds(microWait);
            wast.text = "320.0";
            food.text = "320.0";

            yield return new WaitForSeconds(microWait);
            wast.text = "160.0";
            food.text = "425.0";

            yield return new WaitForSeconds(microWait);
            wast.text = "120.0";
            food.text = "500.0";


            

            yield return new WaitForSeconds(waitTime7 - (4*microWait));

        }



    }
}
