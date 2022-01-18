using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    protected Button button;
    public ActionSO action;
    public delegate void InteractableEvent(Interactable i);
    public InteractableEvent executeEvent;
    public bool canPress;
    private void Awake()
    {
        button = GetComponent<Button>();

    }

    private void Start()
    {
        if (button != null)
            button.onClick.AddListener(delegate { ExecuteAction(); });
    }


    public virtual void ExecuteAction()
    {
        executeEvent?.Invoke(this);
        canPress = false;
        if (TutorialScript.Instance.inTutorial)
        {
            if (!TutorialScript.Instance.CheckInteraction(this)) {

                print("Dob");
               
                return;
            }
            //Check
        }
        canPress = true;
    }

    public virtual void CheckRequirements()
    {

    }

    private void OnEnable()
    {
        GameManager.Instance.updateGameState += CheckRequirements;
    }

    private void OnDisable()
    {
        GameManager.Instance.updateGameState -= CheckRequirements;
    }

    public virtual void ActivateButton()
    {
    }

    public virtual void DisableButton()
    {
    }


    public virtual void Selected()
    {
        MenuManager.Instance.DisplayDescriptionWindow(action);
        CursorScript.Instance.Hover();
    }

    public virtual void Deselected()
    {
        MenuManager.Instance.HideDescriptionWindow();
        CursorScript.Instance.ResetCursor();
    }
}
