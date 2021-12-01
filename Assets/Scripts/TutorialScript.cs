using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public bool inTutorial;
    public int tutorialStep;
    public GameObject[] tutorialWindows;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void DisplayTutorial()
    {
        foreach (var item in tutorialWindows)
        {
            item.SetActive(false);
        }
        tutorialStep = 0;
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
