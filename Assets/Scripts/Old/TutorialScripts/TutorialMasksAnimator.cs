using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMasksAnimator : MonoBehaviour
{

    public Animator masksAnimator;
    public int sequencerInteger;

    public GameObject tutorialCanvas;
    public GameObject masksParent;
    //public GameObject maskedSolid;

    //public void Start()
    //{
    //    maskedSolid.gameObject.SetActive(true);
    //}

    public void SetSequencer()
    {
        if (tutorialCanvas.activeInHierarchy == true)
        {
            sequencerInteger++;
            masksAnimator.SetInteger("Sequencer", sequencerInteger);
        }
        
    }

    public void TurnOffMasks()
    {
        masksParent.gameObject.SetActive(false);
    }
}
