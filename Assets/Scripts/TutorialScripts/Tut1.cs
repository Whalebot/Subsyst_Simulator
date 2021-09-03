using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut1 : MonoBehaviour
{
    public GameObject cover;

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

        cover.gameObject.SetActive(true);
    }

    public void Z1CoverSwipesUp()
    {
        //LeanTween.move(cover, outVector, tiempo1).setEase(curve);
        LeanTween.moveLocalY(cover, outDistance, tiempo1).setEase(curve);

        // (); DEL SIGUIENTE PASO.
    }
}
