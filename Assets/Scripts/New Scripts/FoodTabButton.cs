using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTabButton : Interactable
{
    public GameObject setActiveTarget;
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        MenuManager.Instance.CloseFoodTabs();
        setActiveTarget.SetActive(true);
        InputManager.Instance.FindInteractablesInObject(transform.parent.parent.gameObject);
        InputManager.Instance.activeInteractable = this;
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
