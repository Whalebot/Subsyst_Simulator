using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecenterButtonSlider : MonoBehaviour
{
    public float distanceUp;
    public string previousUp;
    public float distanceDown;
    public string previousDown;
    public float speed;

    public AnimationCurve curve;


    public void SlideInButton()
    {
        LeanTween.moveLocalY(gameObject, distanceUp, speed).setEase(curve);
    }

    public void SlideOutButton()
    {
        LeanTween.moveLocalY(gameObject, -distanceDown, speed).setEase(curve);
    }
}
