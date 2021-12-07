using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class TutorialScript : MonoBehaviour
{
    public bool inTutorial;
    public int tutorialStep;
    public GameObject tutorialParent;
    public GameObject[] tutorialWindows;


    // Start is called before the first frame update
    void Start()
    {

    }
    [Button]
    public void DisplayTutorial()
    {
        tutorialParent.SetActive(true);
        foreach (var item in tutorialWindows)
        {
            item.SetActive(false);
        }
        tutorialStep = 0;
        tutorialWindows[tutorialStep].SetActive(true);
    }

    public void PreviousTutorial()
    {
        foreach (var item in tutorialWindows)
        {
            item.SetActive(false);
        }
        tutorialStep++;
        tutorialWindows[tutorialStep].SetActive(true);
    }

    public void NextTutorial()
    {
        foreach (var item in tutorialWindows)
        {
            item.SetActive(false);
        }
        tutorialStep++;
        tutorialWindows[tutorialStep].SetActive(true);
    }
}
