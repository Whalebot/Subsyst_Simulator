using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButton : Interactable
{
    public ButtonType buttonType;
    public enum ButtonType { Production, Upgrade }
    public ButtonState state;
    public bool techButton;
    public UpgradeSO requiredUpgrade;
    public GameObject lockObject;
    public GameObject maxObject;
    public GameObject descriptionWindow;
    public TextMeshProUGUI upgradeLevel;
    bool unlocked;
    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(delegate { ExecuteAction(); });
        TimeManager.Instance.advanceTimeEvent += CheckRequirements;
        CheckRequirements();
    }



    public override void CheckRequirements()
    {
        if (state == ButtonState.UpgradeMaxed) return;
        Ressources tempRessources = new Ressources();

        //Check if fully upgraded, disable button if it is
        if (buttonType == ButtonType.Upgrade)
        {
            if (action.GetType() == typeof(UpgradeSO))
            {
                UpgradeSO a = (UpgradeSO)action;
                if (upgradeLevel != null)
                {
                    upgradeLevel.text = "" + UpgradeManager.Instance.CheckUpgradeNumber(a);
                }
                if (UpgradeManager.Instance.CheckUpgradeNumber(a) >= a.upgradeLimit && state != ButtonState.UpgradeMaxed)
                {

                    state = ButtonState.UpgradeMaxed;
                    TimeManager.Instance.advanceTimeEvent -= CheckRequirements;
                    DisableButton();
                    return;
                }
            }
            if (action.GetType() == typeof(ProductionSO))
            {
                ProductionSO a = (ProductionSO)action;
                if (upgradeLevel != null)
                {
                    upgradeLevel.text = "" + UpgradeManager.Instance.CheckUpgradeNumber(a);
                }
                if (UpgradeManager.Instance.CheckUpgradeNumber(a) >= a.upgradeLimit && state != ButtonState.UpgradeMaxed)
                {
                    state = ButtonState.UpgradeMaxed;
                    TimeManager.Instance.advanceTimeEvent -= CheckRequirements;
                    DisableButton();
                    return;
                }
            }
        }
        else if (buttonType == ButtonType.Production)
        {
            if (!TimeManager.canSmallScale && !TutorialScript.Instance.inTutorial)
            {
                DisableButton();
                return;
            }
        }
        //Check if it needs upgrade
        if (requiredUpgrade != null)
        {
            if (!UpgradeManager.Instance.obtainedUpgrades.Contains(requiredUpgrade))
            {
                state = ButtonState.RequiresUpgrade;
                DisableButton();
                return;
            }
        }


        //Check ressources
        if (buttonType == ButtonType.Upgrade && action.GetType() == typeof(ProductionSO))
        {
            if (UpgradeManager.Instance.CheckUpgradeNumber(action) == 0)
            {
                UpgradeManager.Instance.FreeUpgrade(action);
                ActivateButton();
            }


            ProductionSO a = (ProductionSO)action;
        //    print(action + " " + UpgradeManager.Instance.CheckUpgradeNumber(a) + " " + a.upgradeLevels[UpgradeManager.Instance.CheckUpgradeNumber(a)].upgradeCost);
            if (a.upgradeLevels[UpgradeManager.Instance.CheckUpgradeNumber(a)].upgradeCost > GameManager.Instance.Money)
            {
                state = ButtonState.MissingRessources;
                DisableButton();
                return;
            }
            else
            {
                state = ButtonState.CanPress;
                ActivateButton();
            }
        }
        else
        {
            if (GameManager.Instance.CheckRessources(action))
            {
                if (!unlocked) UpgradeManager.Instance.UnlockAction(action);
                unlocked = true;
                state = ButtonState.CanPress;
                ActivateButton();
            }
            else
            {
                state = ButtonState.MissingRessources;
                DisableButton();
            }
        }

    }

    public override void ExecuteAction()
    {
        base.ExecuteAction();
        if (!canPress) return;



        if (action != null && Telemetry.Instance.sendTelemetry) Telemetry.Instance.StartCoroutine(Telemetry.Instance.Post(action));
        if (buttonType == ButtonType.Upgrade)
        {
            UpgradeManager.Instance.UnlockUpgrade(action);
            if (techButton) gameObject.SetActive(false);
        }
        else
        {
            FoodManager.Instance.ExecuteProduction((ProductionSO)action);
            TimeManager.canSmallScale = false;
            CheckRequirements();
        }
        //CheckRequirements();
    }

    public override void Selected()
    {
       // base.Selected();

        MenuManager.Instance.DisplayDescriptionWindow(this);
        CursorScript.Instance.Hover();

        if (state != ButtonState.CanPress) CursorScript.Instance.BlockCursor();

    }

    public override void ActivateButton()
    {
        if (lockObject != null)
            lockObject.SetActive(false);
        button.interactable = true;
    }

    public override void DisableButton()
    {
        if (maxObject != null)
        {
            if (state == ButtonState.UpgradeMaxed) maxObject.SetActive(true);
        }
        if (lockObject != null)
        {
            if (state == ButtonState.RequiresUpgrade)
                lockObject.SetActive(true);
        }
        button.interactable = false;
    }

    public enum ButtonState
    {
        CanPress, MissingRessources, RequiresUpgrade, UpgradeMaxed
    }
}
