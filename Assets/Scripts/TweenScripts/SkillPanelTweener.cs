using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelTweener : MonoBehaviour
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


    public void SkillPanelUpper()
    {
        LeanTween.moveLocalY(gameObject, distanceUp, speed).setEase(curve);
    }

    public void SkillPanelDowner()
    {
        LeanTween.moveLocalY(gameObject, -distanceDown, speed).setEase(curve);
    }

}
