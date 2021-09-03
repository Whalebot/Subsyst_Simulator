using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePanelSwiper : MonoBehaviour
{
    public string previousUp;
    public float distanceUp = 0f;
    public string previousDown;
    public float distanceDown = 0f;
    public float speed = 0f;
    public float delay = 0f;

    //public MoneyManager moneyManager;

    public AnimationCurve curve;
    //public LeanTweenType easeType;


    public void SidePanelInner()
    {
        LeanTween.moveLocalX(gameObject, distanceUp, speed).setEase(curve);
    }

    public void SidePanelOuter()
    {
        LeanTween.moveLocalX(gameObject, -distanceDown, speed).setEase(curve);
    }
}
