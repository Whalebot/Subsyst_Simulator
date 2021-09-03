using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrowAnimator : MonoBehaviour
{
    public GameObject objeto;
    public float xPositive;
    public float xNegative;
    public AnimationCurve curve;
    public float tiempo;
    public bool timeIsRunning;

    public Transform position1;
    public Transform position2;
    public Vector3 posVec1;
    public Vector3 posVec2;

    //public bool usesX;
    //public bool usesY;

    public void Start()
    {
        posVec1 = new Vector3(position1.position.x, position1.position.y, position1.position.z);
        posVec2 = new Vector3(position2.position.x, position2.position.y, position2.position.z);

        timeIsRunning = true;
        StartCoroutine(TimeScheduleCoroutine());
    }

    IEnumerator TimeScheduleCoroutine()
    {
        while (timeIsRunning == true)
        {
            //if (usesX == true)
            //{
                LeanTween.move(objeto, posVec1, tiempo).setEase(curve);
                yield return new WaitForSeconds(tiempo);
                LeanTween.move(objeto, posVec2, tiempo).setEase(curve);
                yield return new WaitForSeconds(tiempo);
            //}
            
            //else if (usesY == true)
            //{
            //    LeanTween.move(objeto, xPositive, tiempo).setEase(curve);
            //    yield return new WaitForSeconds(tiempo);
            //    LeanTween.move(objeto, xNegative, tiempo).setEase(curve);
            //    yield return new WaitForSeconds(tiempo);
            //}


        }




    }

}
