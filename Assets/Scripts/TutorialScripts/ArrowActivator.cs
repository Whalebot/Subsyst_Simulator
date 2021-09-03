using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowActivator : MonoBehaviour
{
    public GameObject arrow;

    //public Transform originalPosition;
    //public Vector3 originalVector;
    //public Transform outPosition;
    //public Vector3 outVector;

    //public Vector3 scaleZero;
    //public Vector3 scaleOne;

    public float tiempo1;

    public float delay1;

    public AnimationCurve curve;

    //public int counter;
    //public int subCounter;

    //public void Start()
    //{
    //    originalVector = new Vector3(originalPosition.position.x, originalPosition.position.y, originalPosition.position.z);
    //    outVector = new Vector3(outPosition.position.x, outPosition.position.y, outPosition.position.z);

    //    scaleOne = new Vector3(1f, 1f, 1f);


    //    cover.gameObject.SetActive(true);
    //}

    public void ZActivateArrow()
    {
        arrow.gameObject.SetActive(true);
    }

    public void ZDeactivateArrow()
    {
        arrow.gameObject.SetActive(false);
    }

    public void YPingPongScaleArrow()
    {
        LeanTween.scaleX(arrow, 0f, 0f);
        LeanTween.scaleY(arrow, 0f, 0f);

        LeanTween.scaleX(arrow, 1f, tiempo1).setEase(curve).setDelay(delay1);
        LeanTween.scaleY(arrow, 1f, tiempo1).setEase(curve).setDelay(delay1);


    }
}
