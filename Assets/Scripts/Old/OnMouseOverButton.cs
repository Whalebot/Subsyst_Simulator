using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOverButton : MonoBehaviour
{
    public GameObject descriptor;

    public void Start()
    {
        descriptor.gameObject.SetActive(false);
    }

    public void OnMouseOver()
    {
        descriptor.gameObject.SetActive(true);
    }
}
