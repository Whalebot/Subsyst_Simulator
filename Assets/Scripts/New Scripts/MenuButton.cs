using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : Interactable
{
    public GameObject setActiveTarget;
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        MenuManager.Instance.CloseAllTabs();
        setActiveTarget.SetActive(true);
    }
}
