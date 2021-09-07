using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButton : Interactable
{
    public ButtonState state;
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

        if (requiredUpgrade != null)
        {
            if (!UpgradeManager.Instance.obtainedUpgrades.Contains(requiredUpgrade))
            {
                
                state = ButtonState.RequiresUpgrade;
                DisableButton();
                return;
            }
        }

        

        if (GameManager.Instance.CheckRessources(UpgradeManager.Instance.CheckCost(action)))
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

    public override void ExecuteAction()
    {
        if (action != null) Telemetry.Instance.StartCoroutine(Telemetry.Instance.Post(action));
        if (action.GetType() == typeof(UpgradeSO))
        {
            //UpgradeSO a = (UpgradeSO)action;
            UpgradeManager.Instance.UnlockUpgrade((UpgradeSO)action);
        }
        else
        {
            FoodManager.Instance.ExecuteProduction((ProductionSO)action);
        }
        //CheckRequirements();
    }

    public override void Selected()
    {
        base.Selected();
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
