using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsPanelSwiper : MonoBehaviour
{
    public bool needsRender;
    public GameObject noNotificationGO;
    public float autoCloseWaitTime;
    public string previousUp;
    public float distanceUp = 0f;
    public string previousDown;
    public float distanceDown = 0f;
    public float speed = 0f;
    public float delay = 0f;


    public AnimationCurve curve;

    public void Start()
    {
        LeanTween.moveLocalY(gameObject, distanceUp, 0f); // DIV STARTS OUT

    }


    public void NewsPanelInner()
    {
        LeanTween.moveLocalY(gameObject, distanceUp, speed).setEase(curve);
        
    }

    public void NewsPanelOuter()
    {
        LeanTween.moveLocalY(gameObject, distanceDown, speed).setEase(curve);

        if (needsRender == true)
        {
            if (noNotificationGO.activeInHierarchy == true)
            {
                StartCoroutine(AutoClosePanelCoroutine());
            }
        }
    }

    IEnumerator AutoClosePanelCoroutine()
        {
            
            yield return new WaitForSeconds(autoCloseWaitTime);
            NewsPanelInner();
        }

    }
