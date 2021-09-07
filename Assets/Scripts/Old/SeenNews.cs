using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeenNews : MonoBehaviour
{
    public bool needsRender;
    public GameObject notificationIcon;
    public GameObject soundObj;
    public GameObject noNotification;
    public bool newNotifications;

    public Animator pulseAnimator;
    public AudioSource audSource;
    

    // Start is called before the first frame update
    void Start()
    {
        notificationIcon.gameObject.SetActive(false);
        soundObj.gameObject.SetActive(false);

        newNotifications = false;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (newNotifications == true)
    //    {
    //        notificationIcon.gameObject.SetActive(true);
    //        soundObj.gameObject.SetActive(true);

    //    }
    //}

    public void ArrivingNotification()
    {
        newNotifications = true;
        notificationIcon.gameObject.SetActive(true);
        pulseAnimator.Play("pulse_notification", -1, 0f);
        soundObj.gameObject.SetActive(true);
        audSource.Play();

        if (needsRender == true) // THIS CONDITION SHOULD BE FALSE IF AI IS PLAYING.
        {
            noNotification.gameObject.SetActive(false);

        }
    }

    public void SeeNotification()
    {
        notificationIcon.gameObject.SetActive(false);
        soundObj.gameObject.SetActive(false);

        newNotifications = false;
    }
}
