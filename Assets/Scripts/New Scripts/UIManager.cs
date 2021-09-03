using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    [FoldoutGroup("Feedback Text")] public GameObject populationGrowthWindow;
    [FoldoutGroup("Feedback Text")] public TextMeshProUGUI oldPopulationText;
    [FoldoutGroup("Feedback Text")] public TextMeshProUGUI newPopulationText;
    [FoldoutGroup("Feedback Text")] public bool showFeedbackText;
    [FoldoutGroup("Feedback Text")] public GameObject feedbackText;
    [FoldoutGroup("Feedback Text")] public Vector3 offset;
    int offsetCounter;

    [FoldoutGroup("Text Components")] public TextMeshProUGUI energyText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI foodText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI wasteText;

    [FoldoutGroup("Text Components")] public TextMeshProUGUI naturalCapitalText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI approvalText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI populationText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI moneyText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI pollutionText;
    [FoldoutGroup("Text Components")] public TextMeshProUGUI beeText;

    [FoldoutGroup("Text Components")] public TextMeshProUGUI dayText;



    public Image foodFill;
    public Image energyFill;
    public Image wasteFill;

    Ressources oldRessources;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        TimeManager.Instance.advanceGameEvent += UpdateText;
        gameManager.updateGameState += UpdateText;
        EventManager.Instance.populationGrowth += DisplayPopulationGrowth;
        oldRessources = new Ressources();
        gameManager.SetRessources(gameManager.ressources, oldRessources);
        UpdateText();
    }

    void UpdateText()
    {
        CompareUpdatedRessources();




        dayText.text = "DAY " + TimeManager.Instance.day;

        energyText.text = "" + gameManager.Energy;
        foodText.text = "" + gameManager.Food;
        wasteText.text = "" + gameManager.Waste;
        naturalCapitalText.text = "" + gameManager.NaturalCapital;
        approvalText.text = "" + gameManager.Approval;
        populationText.text = "" + gameManager.Population;
        moneyText.text = "" + gameManager.Money;
        pollutionText.text = "" + gameManager.Pollution;
        beeText.text = "" + gameManager.Bees;
    }

    void DisplayPopulationGrowth()
    {
        StartCoroutine("DisplayGrowth");

    }

    IEnumerator DisplayGrowth() {
        populationGrowthWindow.SetActive(true);
        oldPopulationText.text = "" + oldRessources.population;
        newPopulationText.text = "" + gameManager.Population;
        yield return new WaitForSeconds(TimeManager.Instance.framesPerTime / 10f);
        populationGrowthWindow.SetActive(false);
    }

    //This makes me sad
    //If value changed, spawn feedback numbers
    void CompareUpdatedRessources()
    {
        UpdateFill();
        if (oldRessources.energy != gameManager.Energy) FeedbackNumbers(energyText.transform, gameManager.Energy - oldRessources.energy);
        if (oldRessources.food != gameManager.Food) FeedbackNumbers(foodText.transform, gameManager.Food - oldRessources.food);
        if (oldRessources.waste != gameManager.Waste) FeedbackNumbers(wasteText.transform, gameManager.Waste - oldRessources.waste);
        if (oldRessources.naturalCapital != gameManager.NaturalCapital) FeedbackNumbers(naturalCapitalText.transform, gameManager.NaturalCapital - oldRessources.naturalCapital);
        if (oldRessources.approval != gameManager.Approval) FeedbackNumbers(approvalText.transform, gameManager.Approval - oldRessources.approval);
        if (oldRessources.population != gameManager.Population) FeedbackNumbers(populationText.transform, gameManager.Population - oldRessources.population);
        if (oldRessources.money != gameManager.Money) FeedbackNumbers(moneyText.transform, gameManager.Money - oldRessources.money);
        if (oldRessources.pollution != gameManager.Pollution) FeedbackNumbers(pollutionText.transform, gameManager.Pollution - oldRessources.pollution);
        if (oldRessources.bees != gameManager.Bees) FeedbackNumbers(beeText.transform, gameManager.Bees - oldRessources.bees);

        //Save current ressources as old ressources for next check
        gameManager.SetRessources(gameManager.ressources, oldRessources);
    }

    public void UpdateFill()
    {
        int highestValue = 0;
        highestValue = gameManager.Food;
        if (gameManager.Energy > highestValue) { highestValue = gameManager.Energy; }
        else if (gameManager.Waste > highestValue) { highestValue = gameManager.Waste; }

        foodFill.fillAmount = (float)gameManager.Food / highestValue;
        energyFill.fillAmount = (float)gameManager.Energy / highestValue;
        wasteFill.fillAmount = (float)gameManager.Waste / highestValue;
        //foodFill    
    }

    public void FeedbackNumbers(Transform other, int value)
    {
        if (showFeedbackText)
        {
            GameObject text = Instantiate(feedbackText, other.position + offset, Quaternion.identity, transform);
            TextMeshProUGUI tmp = text.GetComponentInChildren<TextMeshProUGUI>();

            //Add proper sign and color depending on positive or negative value
            if (value < 0)
            {
                tmp.text = "" + value;
                tmp.color = Color.red;
            }
            else
            {
                tmp.text = "+" + value;
                tmp.color = Color.green;
            }
            //  offsetCounter += numberOffset;
        }
    }

}
