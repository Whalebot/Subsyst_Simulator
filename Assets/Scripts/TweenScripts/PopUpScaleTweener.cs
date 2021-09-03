using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScaleTweener : MonoBehaviour
{
    public Vector3 scaleStart = new Vector3 (0f, 0f, 0f);
    public Vector3 scaleBig = new Vector3(1f, 1f, 1f);

    public float speed;
    public float delay;
    public AnimationCurve ease;

    public void Start()
    {
        LeanTween.scale(gameObject, scaleStart, 0f);

    }

    public void PopUpScaler()
    {
        LeanTween.scale(gameObject, scaleBig, speed).setDelay(delay).setEase(ease);
    }

    public void PopDownScaler()
    {
        LeanTween.scale(gameObject, scaleStart, 0f).setDelay(delay).setEase(ease);

    }
}
