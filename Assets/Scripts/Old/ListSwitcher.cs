using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSwitcher : MonoBehaviour
{
    public GameObject list;
    public GameObject UpArrow;
    public GameObject DownArrow;
    public bool switcher;

    public void Start()
    {
        switcher = false;
        list.gameObject.SetActive(false);
        DownArrow.gameObject.SetActive(true);
        UpArrow.gameObject.SetActive(false);
    }

    public void switchList()
    {
        switcher = !switcher;
        list.gameObject.SetActive(switcher);
        if (switcher == false)
        {
            DownArrow.gameObject.SetActive(true);
            UpArrow.gameObject.SetActive(false);
        }
        else
        {
            DownArrow.gameObject.SetActive(false);
            UpArrow.gameObject.SetActive(true);

        }

    }
}
