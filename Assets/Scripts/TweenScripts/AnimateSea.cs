using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSea : MonoBehaviour
{
    public GameObject sea;

    public float inDistance;
    public float outDistance;
    public float time;
    public AnimationCurve curve;

    public bool timeIsRunning;

    public void Start()
    {
        timeIsRunning = true;
        StartCoroutine(SeaAnimCouroutine());

    }

    IEnumerator SeaAnimCouroutine()
    {
        while (timeIsRunning == true)
        {
            LeanTween.moveLocalY(sea, outDistance, time).setEase(curve);
            yield return new WaitForSeconds(time);
            LeanTween.moveLocalY(sea, inDistance, time).setEase(curve);
            yield return new WaitForSeconds(time);



        }




    }
}
