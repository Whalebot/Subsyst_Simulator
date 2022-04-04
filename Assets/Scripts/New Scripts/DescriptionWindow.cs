using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class DescriptionWindow : MonoBehaviour
{
    public ActionSO action;

    [FoldoutGroup("Gameobject Components")] public GameObject costMoney;
    [FoldoutGroup("Gameobject Components")] public GameObject costFood;
    [FoldoutGroup("Gameobject Components")] public GameObject costEnergy;
    [FoldoutGroup("Gameobject Components")] public GameObject costWaste;
    [FoldoutGroup("Gameobject Components")] public GameObject costPollution;
    [FoldoutGroup("Gameobject Components")] public GameObject resultMoney;
    [FoldoutGroup("Gameobject Components")] public GameObject resultFood;
    [FoldoutGroup("Gameobject Components")] public GameObject resultEnergy;
    [FoldoutGroup("Gameobject Components")] public GameObject resultWaste;
    [FoldoutGroup("Gameobject Components")] public GameObject resultPollution;

    [FoldoutGroup("Gameobject Components")] public GameObject costs;
    [FoldoutGroup("Gameobject Components")] public GameObject outcome;

    [FoldoutGroup("Gameobject Components")] public Image iconImage;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI titleText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI priceText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI descriptionText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI costMoneyText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI costFoodText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI costEnergyText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI costWasteText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI costPollutionText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI resultMoneyText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI resultFoodText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI resultEnergyText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI resultWasteText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI resultPollutionText;
    RectTransform rect;
    public ContentSizeFitter sizeFitter;

    public Color defaultColor;
    // Start is called before the first frame update
    void Awake()
    {

        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        //TimeManager.Instance.advanceTimeEvent += UpdateUI;
        //FoodManager.Instance.productionEvent += UpdateUI;
    }

    private void OnEnable()
    {
        //UpdateDescription((ProductionSO)action);

    }
    private void OnDisable()
    {
        //UpdateDescription((ProductionSO)action);

    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) UpdateUI();
    }

    void UpdateUI(ProductionSO p)
    {
        if (!gameObject.activeInHierarchy) return;
        if (action != null)
        {
            if (action.GetType() == typeof(ProductionSO))
                UpdateDescription((ProductionSO)action);
            else if (action.GetType() == typeof(UpgradeSO))
                UpdateDescription((UpgradeSO)action);
        }

    }

    void UpdateUI()
    {
        if (!gameObject.activeInHierarchy) return;
        if (action != null)
        {
            if (action.GetType() == typeof(ProductionSO))
                UpdateDescription((ProductionSO)action);
            else if (action.GetType() == typeof(UpgradeSO))
                UpdateDescription((UpgradeSO)action);
        }

    }

    [Button]
    public void UpdateDescription(ProductionSO p)
    {
        action = p;
        int upgradeLevel = UpgradeManager.Instance.CheckUpgradeNumber(p);


        if (action.icon != null)
            iconImage.sprite = action.icon;
        else iconImage.gameObject.SetActive(false);

        titleText.text = action.title;
        descriptionText.text = action.description;

        costMoney.SetActive(p.upgradeLevels[upgradeLevel].cost.money != 0);
        costFood.SetActive(p.upgradeLevels[upgradeLevel].cost.food != 0);
        costEnergy.SetActive(p.upgradeLevels[upgradeLevel].cost.energy != 0);
        costWaste.SetActive(p.upgradeLevels[upgradeLevel].cost.waste != 0);
        costPollution.SetActive(p.upgradeLevels[upgradeLevel].cost.pollution != 0);

        resultMoney.SetActive(p.upgradeLevels[upgradeLevel].result.money != 0);
        resultFood.SetActive(p.upgradeLevels[upgradeLevel].result.food != 0);
        resultEnergy.SetActive(p.upgradeLevels[upgradeLevel].result.energy != 0);
        resultWaste.SetActive(p.upgradeLevels[upgradeLevel].result.waste != 0);
        resultPollution.SetActive(p.upgradeLevels[upgradeLevel].result.pollution != 0);

        costs.SetActive(true);
        outcome.SetActive(p.upgradeLevels[upgradeLevel].result.money != 0 || p.upgradeLevels[upgradeLevel].result.food != 0 || p.upgradeLevels[upgradeLevel].result.waste != 0 || p.upgradeLevels[upgradeLevel].result.energy != 0 || p.upgradeLevels[upgradeLevel].result.pollution != 0);

        costMoneyText.text = "" + p.upgradeLevels[upgradeLevel].cost.money;
        costFoodText.text = "" + p.upgradeLevels[upgradeLevel].cost.food;
        costEnergyText.text = "" + p.upgradeLevels[upgradeLevel].cost.energy;
        costWasteText.text = "" + p.upgradeLevels[upgradeLevel].cost.waste;
        costPollutionText.text = "" + p.upgradeLevels[upgradeLevel].cost.pollution;

        bool[] missing = GameManager.Instance.FindMissingRessources(p.upgradeLevels[upgradeLevel].cost);
        if (missing[0]) costEnergyText.color = Color.red;
        else costEnergyText.color = defaultColor;
        if (missing[1]) costFoodText.color = Color.red;
        else costFoodText.color = defaultColor;
        if (missing[2]) costWasteText.color = Color.red;
        else costWasteText.color = defaultColor;
        if (missing[5]) costMoneyText.color = Color.red;
        else costMoneyText.color = defaultColor;
        if (missing[6]) costPollutionText.color = Color.red;
        else costPollutionText.color = defaultColor;


        resultMoneyText.text = "" + p.upgradeLevels[upgradeLevel].result.money;
        resultFoodText.text = "" + p.upgradeLevels[upgradeLevel].result.food;
        resultEnergyText.text = "" + p.upgradeLevels[upgradeLevel].result.energy;
        resultWasteText.text = "" + p.upgradeLevels[upgradeLevel].result.waste;
        resultPollutionText.text = "" + p.upgradeLevels[upgradeLevel].result.pollution;



        StartCoroutine("SetDirty");
    }

    public void UpdateDescription(ActionSO p)
    {
        if (p.GetType() == typeof(UpgradeSO))
        {
            UpgradeSO ps = (UpgradeSO)p;

            action = ps;
            if (action.icon != null)
                iconImage.sprite = action.icon;
            else iconImage.gameObject.SetActive(false);

            titleText.text = action.title;
            descriptionText.text = action.description;
            priceText.text = "" + ps.price;

            if (ps.price > GameManager.Instance.Money) priceText.color = Color.red;
            else priceText.color = defaultColor;

            costMoney.SetActive(ps.price != 0);
            costs.SetActive(false);
            outcome.SetActive(false);
            costMoneyText.text = "" + ps.price;

            if (ps.price > GameManager.Instance.Money) costMoneyText.color = Color.red;
            else costMoneyText.color = defaultColor;


        }
        else
        {
            ProductionSO ps = (ProductionSO)p;

            action = ps;
            if (action.icon != null)
                iconImage.sprite = action.icon;
            else iconImage.gameObject.SetActive(false);

            titleText.text = action.title;
            descriptionText.text = action.description;

            int upgradeLevel = UpgradeManager.Instance.CheckUpgradeNumber(ps) + 1;
            if (upgradeLevel >= ps.upgradeLevels.Length) upgradeLevel = upgradeLevel - 1;

            priceText.text = "" + ps.upgradeLevels[upgradeLevel].upgradeCost;
            if (ps.upgradeLevels[upgradeLevel].upgradeCost > GameManager.Instance.Money) priceText.color = Color.red;
            else priceText.color = defaultColor;


            costMoney.SetActive(ps.upgradeLevels[upgradeLevel].cost.money != 0);
            costFood.SetActive(ps.upgradeLevels[upgradeLevel].cost.food != 0);
            costEnergy.SetActive(ps.upgradeLevels[upgradeLevel].cost.energy != 0);
            costWaste.SetActive(ps.upgradeLevels[upgradeLevel].cost.waste != 0);
            costPollution.SetActive(ps.upgradeLevels[upgradeLevel].cost.pollution != 0);

            resultMoney.SetActive(ps.upgradeLevels[upgradeLevel].result.money != 0);
            resultFood.SetActive(ps.upgradeLevels[upgradeLevel].result.food != 0);
            resultEnergy.SetActive(ps.upgradeLevels[upgradeLevel].result.energy != 0);
            resultWaste.SetActive(ps.upgradeLevels[upgradeLevel].result.waste != 0);
            resultPollution.SetActive(ps.upgradeLevels[upgradeLevel].result.pollution != 0);

            costs.SetActive(true);
            outcome.SetActive(ps.upgradeLevels[upgradeLevel].result.money != 0 || ps.upgradeLevels[upgradeLevel].result.food != 0 || ps.upgradeLevels[upgradeLevel].result.waste != 0 || ps.upgradeLevels[upgradeLevel].result.energy != 0 || ps.upgradeLevels[upgradeLevel].result.pollution != 0);

            costMoneyText.text = "" + ps.upgradeLevels[upgradeLevel].cost.money;
            costFoodText.text = "" + ps.upgradeLevels[upgradeLevel].cost.food;
            costEnergyText.text = "" + ps.upgradeLevels[upgradeLevel].cost.energy;
            costWasteText.text = "" + ps.upgradeLevels[upgradeLevel].cost.waste;
            costPollutionText.text = "" + ps.upgradeLevels[upgradeLevel].cost.pollution;

            bool[] missing = GameManager.Instance.FindMissingRessources(ps.upgradeLevels[upgradeLevel].cost);
            if (missing[0]) costEnergyText.color = Color.red;
            else costEnergyText.color = defaultColor;
            if (missing[1]) costFoodText.color = Color.red;
            else costFoodText.color = defaultColor;
            if (missing[2]) costWasteText.color = Color.red;
            else costWasteText.color = defaultColor;
            if (missing[5]) costMoneyText.color = Color.red;
            else costMoneyText.color = defaultColor;
            if (missing[6]) costPollutionText.color = Color.red;
            else costPollutionText.color = defaultColor;


            resultMoneyText.text = "" + ps.upgradeLevels[upgradeLevel].result.money;
            resultFoodText.text = "" + ps.upgradeLevels[upgradeLevel].result.food;
            resultEnergyText.text = "" + ps.upgradeLevels[upgradeLevel].result.energy;
            resultWasteText.text = "" + ps.upgradeLevels[upgradeLevel].result.waste;
            resultPollutionText.text = "" + ps.upgradeLevels[upgradeLevel].result.pollution;


        }
        StartCoroutine("SetDirty");
    }

    IEnumerator SetDirty()
    {
        sizeFitter.enabled = false;
        yield return new WaitForEndOfFrame();
        sizeFitter.enabled = true;
        yield return new WaitForEndOfFrame();
        sizeFitter.enabled = false;
        yield return new WaitForEndOfFrame();
        sizeFitter.enabled = true;
    }
}
