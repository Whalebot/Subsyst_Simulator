using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerManager : MonoBehaviour
{
    public int maxMoney;
    public int maxPopulation;

    public int cattleUpgrades = 0;
    public UpgradeSO cattleUpgrade;
    public GameObject[] cattleGO;
    public GameObject[] foodGO;
    public GameObject[] trashGO;
    public GameObject[] peopleGO;
    public GameObject[] populationGO;
    public GameObject[] natCapGO;

    public GameObject[] oilGO;
    public int oilUpgrades = 0;
    public UpgradeSO oilUpgrade;
    public GameObject[] wasteGO;
    public int wasteUpgrades = 0;
    public UpgradeSO wasteUpgrade;
    public UpgradeAnimation[] populationAnimations;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.updateGameState += UpdateVisuals;
    }

    // Update is called once per frame
    public void UpdateVisuals()
    {

        if (GameManager.Instance.Money > maxMoney) maxMoney = GameManager.Instance.Money;
        if (GameManager.Instance.Population > maxPopulation) maxPopulation = GameManager.Instance.Population;

        cattleUpgrades = UpgradeManager.Instance.CheckUpgradeNumber(cattleUpgrade);
        for (int i = 0; i < cattleUpgrades; i++)
        {
            if (cattleGO.Length - 1 < i) return;
            cattleGO[i].GetComponentInChildren<Animator>().SetBool("isOn", true);
        }
        for (int i = 0; i < foodGO.Length; i++)
        {
            if (foodGO.Length - 1 < i) return;

            foodGO[i].SetActive(100 * Mathf.Pow(10, i) < GameManager.Instance.Food);
        }
        for (int i = 0; i < trashGO.Length; i++)
        {
            if (trashGO.Length - 1 < i) return;

            trashGO[i].GetComponentInChildren<Animator>().SetBool("isOn", i < GameManager.Instance.Pollution / 1000);
        }

        for (int i = 0; i < populationGO.Length; i++)
        {
            if (populationGO.Length - 1 < i) return;

            populationGO[i].GetComponentInChildren<Animator>().SetBool("isOn", 100 * Mathf.Pow(10, i) < maxMoney);
        }
        for (int i = 0; i < peopleGO.Length; i++)
        {
            if (peopleGO.Length - 1 < i) return;

            peopleGO[i].GetComponentInChildren<Animator>().SetBool("isOn", 10 * Mathf.Pow(10, i) < GameManager.Instance.Population);
        }
        oilUpgrades = UpgradeManager.Instance.CheckUpgradeNumber(oilUpgrade);
        for (int i = 0; i < oilUpgrades; i++)
        {
            if (oilGO.Length - 1 < i) return;
            oilGO[i].GetComponentInChildren<Animator>().SetBool("isOn", true);
        }
        wasteUpgrades = UpgradeManager.Instance.CheckUpgradeNumber(wasteUpgrade);
        for (int i = 0; i < wasteUpgrades; i++)
        {
            if (wasteGO.Length - 1 < i) return;
            wasteGO[i].GetComponentInChildren<Animator>().SetBool("isOn", true);

        }

        for (int i = 0; i < populationAnimations.Length; i++)
        {
            if (populationAnimations.Length - 1 < i) return;
            if (10 * Mathf.Pow(5, i) < maxPopulation)
            {
                populationAnimations[i].Upgrade();
            }
        }
    }
}
