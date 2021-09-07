using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInner : MonoBehaviour
{
    public GameObject mainDiv;

    public Transform originalPosition;
    public Vector3 originalVector;
    public Transform outPosition;
    public Vector3 outVector;

    public Vector3 scaleZero;
    public Vector3 scaleOne;

    public float tiempo1;

    public float delay1;

    public AnimationCurve curve;

    public float inDistance;
    public float outDistance;

    public void Start()
    {
        originalVector = new Vector3(originalPosition.position.x, originalPosition.position.y, originalPosition.position.z);
        outVector = new Vector3(outPosition.position.x, outPosition.position.y, outPosition.position.z);

        scaleOne = new Vector3(1f, 1f, 1f);

        //LeanTween.move(mainDiv, outVector, 0f);

        LeanTween.moveLocalY(mainDiv, outDistance, 0f);

        //mainDiv.gameObject.SetActive(true);
    }

    public void Z1TabSwipesUp()
    {
        //LeanTween.move(mainDiv, originalVector, tiempo1).setDelay(delay1).setEase(curve);
        LeanTween.moveLocalY(mainDiv, inDistance, tiempo1).setDelay(delay1).setEase(curve);
        // (); DEL SIGUIENTE PASO.
    }
}
