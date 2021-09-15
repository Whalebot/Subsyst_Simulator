using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public static NewsManager Instance { get; private set; }

    public bool seenNotification;
    public GameObject notification;
    public GameObject newspaper;
    public GameObject newsPrefab;
    public Transform newsParent;
    private void Awake()
    {
        Instance = this;
    }

    public void SpawnNews(GameEventSO so)
    {
        GameObject GO = Instantiate(newsPrefab, newsParent);
        NewsDescription desc = GO.GetComponent<NewsDescription>();
        desc.SetValues(so);
    }

    public void CloseNews()
    {
        newspaper.SetActive(false);
    }

    public void OpenNews()
    {
        seenNotification = true;
        notification.SetActive(false);
        newspaper.SetActive(true);
    }

    public void ActivateNotification()
    {
        seenNotification = false;
        notification.SetActive(true);
    }
}
