using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Febucci.UI;

public class TutorialScript : MonoBehaviour
{
    public static TutorialScript Instance { get; private set; }
    public bool inTutorial;
    public int tutorialStep;
    public GameObject tutorialParent;
    // public GameObject[] tutorialWindows;
    public TutorialSetup[] tutorialWindows;
    public List<Interactable> currentInteractables;

    public GameObject forwardButton, backButton;
    public GameObject restartButton;

    public GameObject tutorialSFX;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public bool CheckInteraction(Interactable interactable)
    {
        if (currentInteractables.Count > 0)
        {
            if (currentInteractables.Contains(interactable))
            {
                NextTutorial();
                return true;
            }
            else return false;
        }
        return false;
    }

    [Button]
    public void DisplayTutorial()
    {
        inTutorial = true;

        tutorialParent.SetActive(true);
        foreach (var item in tutorialWindows)
        {
            item.window.SetActive(false);
        }
        tutorialStep = 0;

        restartButton.SetActive(false);
        UpdateWindows();
    }
    void UpdateWindows()
    {
        tutorialWindows[tutorialStep].window.SetActive(true);
        currentInteractables.Clear();
        foreach (var item in tutorialWindows[tutorialStep].interactables)
        {
            currentInteractables.Add(item);
        }
        backButton.SetActive(tutorialStep > 0);
        forwardButton.SetActive(tutorialWindows[tutorialStep].interactables.Count == 0);
    }

    [Button]
    public void PreviousTutorial()
    {

        foreach (var item in tutorialWindows)
        {
            item.window.SetActive(false);
        }
        tutorialStep--;
        UpdateWindows();
        if (Application.isPlaying)
            Instantiate(tutorialSFX);
    }

    private void Update()
    {
        if (!inTutorial) return;
        if (Input.GetMouseButtonDown(0))
        {
            TextAnimatorPlayer[] all = tutorialWindows[tutorialStep].window.gameObject.GetComponentsInChildren<TextAnimatorPlayer>();
            if (all.Length > 0)
            {
                TextAnimatorPlayer temp = all[all.Length - 1];
                if (temp != null)
                {
                    if (!temp.textAnimator.allLettersShown)
                    {
                        temp.SkipTypewriter();
                        return;
                    }
                }
            }
        }
    }
    [Button]
    public void NextTutorial()
    {


        foreach (var item in tutorialWindows)
        {
            item.window.SetActive(false);
        }
        tutorialStep++;
        if (tutorialStep >= tutorialWindows.Length)
        {

            EndTutorial();
        }
        else
            UpdateWindows();
        if (Application.isPlaying)
            Instantiate(tutorialSFX);
    }
    [Button]
    public void EndTutorial()
    {
        inTutorial = false;
        tutorialStep = 0;
        tutorialParent.SetActive(false);
        restartButton.SetActive(true);
        if (Application.isPlaying)
        {


            GameManager.Instance.StartGame();
        }
    }
}

[System.Serializable]
public class TutorialSetup
{
    public GameObject window;
    public List<Interactable> interactables;
}