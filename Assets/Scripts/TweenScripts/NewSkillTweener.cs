using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkillTweener : MonoBehaviour
{
    ////public float distance = 0f;
    //public float speed = 0f;
    ////public float delay = 0f;
    //Color blue = new Color(0, 86 / 255, 255 / 255, 1);
    //Color fadeBlue = new Color(0, 86 / 255, 255 / 255, 0.2f);
    //Material mats;


    ////public AnimationCurve curve;
    //public LeanTweenType easeType;


    ////public void SkillPanelUpper()
    ////{
    ////    LeanTween.moveLocalY(gameObject, distance, speed).setEase(curve);
    ////}

    //public void NewSkillNoticer()
    //{
    //    LeanTween.value(gameObject, fadeBlue, blue, 0.3f).setOnUpdate((Color val) => mats.color = val);
    public void yeah()
    {
        LeanTween.scaleY(gameObject, 1, 1);
    }
    //}
}
