using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;
    public GameObject paper;
    void Awake() {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenPaper() {
        paper.SetActive(true);
    }

    public void ClosePaper()
    {
        paper.SetActive(false);
    }
}
