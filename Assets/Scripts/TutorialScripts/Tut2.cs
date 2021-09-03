using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut2 : MonoBehaviour
{
    public GameObject mainDiv;

    public GameObject[] txt;

    public Transform originalPosition;
    public Vector3 originalVector;
    public Transform outPosition;
    public Vector3 outVector;

    public Vector3 scaleZero;
    public Vector3 scaleOne;

    public float tiempo1;

    public float delay1;

    public AnimationCurve curve;

    public int counter;
    public int subCounter;

    public void Start()
    {
        originalVector = new Vector3(originalPosition.position.x, originalPosition.position.y, originalPosition.position.z);
        outVector = new Vector3(outPosition.position.x, outPosition.position.y, outPosition.position.z);

        scaleOne = new Vector3(1f, 1f, 1f);

        LeanTween.scale(mainDiv, scaleZero, 0f);

        //cover.gameObject.SetActive(true);
    }

    public void Z0OpenTxtDiv()
    {
        LeanTween.scale(mainDiv, scaleOne, tiempo1).setEase(curve).setDelay(delay1);


    }

    public void Z1SwitchTxt()
    {
        counter++;
        subCounter = (counter - 1);

        if (counter < txt.Length)
        {
            txt[subCounter].gameObject.SetActive(false);
            txt[counter].gameObject.SetActive(true);
        }
       

        //if (counter >= txt.Length)
        //{
        //    Debug.Log(txt.Length);
        //}
        
    }

    public void Z2CloseTxtDiv()
    {
        LeanTween.scale(mainDiv, scaleZero, tiempo1).setEase(curve);

    }
}
