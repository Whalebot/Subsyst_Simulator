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
    [FoldoutGroup("Gameobject Components")] public GameObject resultMoney;
    [FoldoutGroup("Gameobject Components")] public GameObject resultFood;
    [FoldoutGroup("Gameobject Components")] public GameObject resultEnergy;
    [FoldoutGroup("Gameobject Components")] public GameObject resultWaste;
    [FoldoutGroup("Gameobject Components")] public GameObject resultPollution;
    [FoldoutGroup("Gameobject Components")] public GameObject outcome;

    [FoldoutGroup("Gameobject Components")] public Image iconImage;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI titleText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI descriptionText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI costMoneyText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI costFoodText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI costEnergyText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI costWasteText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI resultMoneyText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI resultFoodText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI resultEnergyText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI resultWasteText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI resultPollutionText;
    RectTransform rect;
    public ContentSizeFitter sizeFitter;
    // Start is called before the first frame update
    void Awake()
    {

        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        //UpdateDescription((ProductionSO)action);

    }
    private void OnDisable()
    {
        //UpdateDescription((ProductionSO)action);

    }

    [Button]
    public void UpdateDescription(ProductionSO p)
    {
        action = p;
        if (action.icon != null)
            iconImage.sprite = action.icon;
        else iconImage.gameObject.SetActive(false);

        titleText.text = action.title;
        descriptionText.text = action.description;

        costMoney.SetActive(action.cost.money != 0);
        costFood.SetActive(action.cost.food != 0);
        costEnergy.SetActive(action.cost.energy != 0);
        costWaste.SetActive(action.cost.waste != 0);

        resultMoney.SetActive(action.result.money != 0);
        resultFood.SetActive(action.result.food != 0);
        resultEnergy.SetActive(action.result.energy != 0);
        resultWaste.SetActive(action.result.waste != 0);
        resultPollution.SetActive(action.result.pollution != 0);

        outcome.SetActive(action.result.money != 0 || action.result.food != 0 || action.result.waste != 0 || action.result.energy != 0 || action.result.pollution != 0);

        Ressources temp = UpgradeManager.Instance.CheckCost(action);
        costMoneyText.text = "" + temp.money;
        costFoodText.text = "" + temp.food;
        costEnergyText.text = "" + temp.energy;
        costWasteText.text = "" + temp.waste;

        Ressources tempResult = UpgradeManager.Instance.CheckResult(action);
        resultMoneyText.text = "" + tempResult.money;
        resultFoodText.text = "" + tempResult.food;
        resultEnergyText.text = "" + tempResult.energy;
        resultWasteText.text = "" + tempResult.waste;
        resultPollutionText.text = "" + tempResult.pollution;

        StartCoroutine("SetDirty");
    }

    public void UpdateDescription(UpgradeSO p)
    {
        action = p;
        if (action.icon != null)
            iconImage.sprite = action.icon;
        else iconImage.gameObject.SetActive(false);

        titleText.text = action.title;
        descriptionText.text = action.description;

        costMoney.SetActive(action.cost.money != 0);
        costFood.SetActive(action.cost.food != 0);
        costEnergy.SetActive(action.cost.energy != 0);
        costWaste.SetActive(action.cost.waste != 0);

        resultMoney.SetActive(action.result.money != 0);
        resultFood.SetActive(action.result.food != 0);
        resultEnergy.SetActive(action.result.energy != 0);
        resultWaste.SetActive(action.result.waste != 0);
        resultPollution.SetActive(action.result.pollution != 0);

        outcome.SetActive(action.result.money != 0 || action.result.food != 0 || action.result.waste != 0 || action.result.energy != 0 || action.result.pollution != 0);

        Ressources temp = UpgradeManager.Instance.CheckCost(action);
        costMoneyText.text = "" + temp.money;
        costFoodText.text = "" + temp.food;
        costEnergyText.text = "" + temp.energy;
        costWasteText.text = "" + temp.waste;

        Ressources tempResult = UpgradeManager.Instance.CheckResult(action);
        resultMoneyText.text = "" + tempResult.money;
        resultFoodText.text = "" + tempResult.food;
        resultEnergyText.text = "" + tempResult.energy;
        resultWasteText.text = "" + tempResult.waste;
        resultPollutionText.text = "" + tempResult.pollution;

        StartCoroutine("SetDirty");
    }

    IEnumerator SetDirty()
    {
        sizeFitter.enabled = false;
        yield return new WaitForEndOfFrame();
        sizeFitter.enabled = true;
    }
}
