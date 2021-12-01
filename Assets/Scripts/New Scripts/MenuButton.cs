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

        if (setActiveTarget.activeInHierarchy) {
            MenuManager.Instance.CloseAllTabs();
            isActive = false;
            setActiveTarget.SetActive(false);
            InputManager.Instance.ResetInteractables();
        }
        else {
            MenuManager.Instance.CloseAllTabs();
            isActive = true;
            setActiveTarget.SetActive(true);
            InputManager.Instance.FindInteractablesInObject(setActiveTarget);
        }

    }
}
