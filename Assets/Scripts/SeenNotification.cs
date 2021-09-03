using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeenNotification : MonoBehaviour
{
    public Image newsButton;

    public void ChangeColorOfButton()
    {
        newsButton.color = new Color32(230, 230, 230, 255);
    }

    public void RefreshedColorOfButton()
    {
        newsButton.color = new Color32(255, 227, 115, 255);
    }
}
