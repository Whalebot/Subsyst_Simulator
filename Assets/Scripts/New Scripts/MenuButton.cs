using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : Interactable
{
    public GameObject setActiveTarget;
    bool isActive;
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        if (!canPress) return;

        if (setActiveTarget.activeInHierarchy)
        {
            MenuManager.Instance.CloseAllTabs();
            isActive = false;
            setActiveTarget.SetActive(false);
            InputManager.Instance.ResetInteractables();

        }
        else
        {
            MenuManager.Instance.CloseAllTabs();
            isActive = true;
            setActiveTarget.SetActive(true);
            InputManager.Instance.FindInteractablesInObjectTab(setActiveTarget);
        }

    }

    public override void Selected()
    {
        base.Selected();
        CursorScript.Instance.SetDescription(gameObject.name);
    }

    public override void Deselected()
    {
        base.Deselected();
        CursorScript.Instance.ResetDescription();
    }
}
