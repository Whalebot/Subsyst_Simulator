using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationAnimator : MonoBehaviour
{

    public GameObject notificationIcon;
    public float wait;

    void Start()
    {
        StartCoroutine(NotificationBlinkCoroutine());
    }

    IEnumerator NotificationBlinkCoroutine()
    {

        notificationIcon.gameObject.SetActive(true);
        yield return new WaitForSeconds(wait);
        notificationIcon.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        notificationIcon.gameObject.SetActive(true);
        yield return new WaitForSeconds(wait);
        notificationIcon.gameObject.SetActive(false);
        yield return new WaitForSeconds(wait);
        notificationIcon.gameObject.SetActive(true);




    }
}
