using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionSwitch : Interactable
{
    public bool isOn;
    public GameEventSO toggleOffEvent;
    public SwitchState state;
    public UpgradeSO requiredUpgrade;
    public GameObject activateObject;
    public GameObject deactiveObject;
    public GameObject lockObject;

    // Start is called before the first frame update

    public override void ActivateButton()
    {
        if (lockObject != null)
            lockObject.SetActive(false);
        button.interactable = true;
        EnableSwitch();
    }

    public override void DisableButton()
    {

        if (lockObject != null)
        {
            if (state == SwitchState.RequiresUpgrade)
                lockObject.SetActive(true);
        }
        button.interactable = false;
    }

    private void Start()
    {

        button.onClick.AddListener(delegate { ExecuteAction(); });

        CheckRequirements();
    }

    public override void CheckRequirements()
    {
        if (!UpgradeManager.Instance.obtainedUpgrades.Contains(requiredUpgrade))
        {
            state = SwitchState.RequiresUpgrade;
            DisableButton();
            return;
        }
        else if (state == SwitchState.RequiresUpgrade) ActivateButton();
    }

    public override void ExecuteAction()
    {
        if (state == SwitchState.RequiresUpgrade) return;

        if (state == SwitchState.Activated)
        {
            DisableSwitch();
        }
        else
        {
            EnableSwitch();
        }
    }

    void EnableSwitch()
    {
        state = SwitchState.Activated;
        activateObject.SetActive(true);
        deactiveObject.SetActive(false);
        if (action.GetType() == typeof(ProductionSO))
        {
            FoodManager.Instance.AddAutomaticProduction((ProductionSO)action);
        }
        else if (action.GetType() == typeof(EventActionSO))
        {
            EventActionSO eventAction = (EventActionSO)action;
            eventAction.eventSO.ExecuteEvent();
        }
    }

    void DisableSwitch()
    {
        state = SwitchState.Deactivated;
        activateObject.SetActive(false);
        deactiveObject.SetActive(true);
        if (action.GetType() == typeof(ProductionSO))
        {
            FoodManager.Instance.RemoveAutomaticProduction((ProductionSO)action);
        }
        else if (action.GetType() == typeof(EventActionSO))
        {

            toggleOffEvent.ExecuteEvent();
        }
    }

    public override void Selected()
    {
        base.Selected();
        if (state == SwitchState.RequiresUpgrade)
        {
            CursorScript.Instance.BlockCursor();
        }
    }

    public enum SwitchState
    {
        Activated, Deactivated, RequiresUpgrade
    }
}
