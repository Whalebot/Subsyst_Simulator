using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatTweener : MonoBehaviour
{
    public float boatDistance = 0f;
    public float boatSpeed = 0f;
    public float boatDelay = 0f;


    public AnimationCurve boatCurve;
    //public LeanTweenType easeType;


    public void TransportFood()
    {
        LeanTween.moveLocalX(gameObject, boatDistance, boatSpeed).setEase(boatCurve).setLoopPingPong(1);
    }

    
}
