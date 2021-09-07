using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOffer : MonoBehaviour
{
    public GameObject tutorialCanvas;

    public void TurnOffTutorialObj()
    {
        tutorialCanvas.gameObject.SetActive(false);
    }
}
