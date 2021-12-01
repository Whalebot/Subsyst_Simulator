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
}
