using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
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

    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;
    public bool uiEnabled;
    public enum UIMode { Default, TopOnly, BottomOnly, None }
    public UIMode uiMode;

    public Image foodFill;
    public Image energyFill;
    public Image wasteFill;

    Color foodColor;
    Color energyColor;
    Color wasteColor;
    public Color positiveColor;
    public Color negativeColor;



    Ressources oldRessources;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        TimeManager.Instance.advanceGameEvent += UpdateText;
        gameManager.updateGameState += UpdateText;
        EventManager.Instance.populationGrowth += DisplayPopulationGrowth;

        foodColor = foodFill.color;
        energyColor = energyFill.color;
        wasteColor = wasteFill.color;

        oldRessources = new Ressources();
        gameManager.SetRessources(gameManager.ressources, oldRessources);
        UpdateText();
    }

    public void ToggleUI()
    {
        if (uiMode == UIMode.None)
            uiMode = UIMode.Default;
        else
            uiMode = uiMode + 1;

        switch (uiMode)
        {
            case UIMode.Default:
                canvas1.enabled = true;
                canvas2.enabled = true;
                canvas3.enabled = true;
                canvas4.enabled = true;
                break;
            case UIMode.TopOnly:
                canvas1.enabled = true;
                canvas2.enabled = false;
                canvas3.enabled = true;
                canvas4.enabled = true;
                break;
            case UIMode.BottomOnly:
                canvas1.enabled = false;
                canvas2.enabled = true;
                canvas3.enabled = true;
                canvas4.enabled = true;
                break;
            case UIMode.None:
                canvas1.enabled = false;
                canvas2.enabled = false;
                canvas3.enabled = false;
                canvas4.enabled = false;
                break;
            default:
                break;
        }
    }

    public void DisableUI()
    {
        uiEnabled = false;
        canvas1.enabled = false;
        canvas2.enabled = false;
    }
    public void EnableUI()
    {
        uiEnabled = true;
        canvas1.enabled = true;
        canvas2.enabled = true;
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

    IEnumerator DisplayGrowth()
    {
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
        if (oldRessources.energy != gameManager.Energy)
        {
            if (oldRessources.energy > gameManager.Energy)
                StartCoroutine(FlashRed(energyFill, energyColor));
            else
                StartCoroutine(FlashWhite(energyFill, energyColor));

            FeedbackNumbers(energyText.transform, gameManager.Energy - oldRessources.energy);
        }
        if (oldRessources.food != gameManager.Food)
        {
            if (oldRessources.food > gameManager.Food)
                StartCoroutine(FlashRed(foodFill, foodColor));
            else
                StartCoroutine(FlashWhite(foodFill, foodColor));

            FeedbackNumbers(foodText.transform, gameManager.Food - oldRessources.food);
        }
        if (oldRessources.waste != gameManager.Waste)
        {
            if (oldRessources.waste > gameManager.Waste)
                StartCoroutine(FlashRed(wasteFill, wasteColor));
            else
                StartCoroutine(FlashWhite(wasteFill, wasteColor));
            FeedbackNumbers(wasteText.transform, gameManager.Waste - oldRessources.waste);
        }

        if (oldRessources.naturalCapital != gameManager.NaturalCapital) FeedbackNumbers(naturalCapitalText.transform, gameManager.NaturalCapital - oldRessources.naturalCapital);
        if (oldRessources.approval != gameManager.Approval) FeedbackNumbers(approvalText.transform, gameManager.Approval - oldRessources.approval);
        if (oldRessources.population != gameManager.Population) FeedbackNumbers(populationText.transform, gameManager.Population - oldRessources.population);
        if (oldRessources.money != gameManager.Money) FeedbackNumbers(moneyText.transform, gameManager.Money - oldRessources.money);
        if (oldRessources.pollution != gameManager.Pollution) FeedbackNumbers(pollutionText.transform, gameManager.Pollution - oldRessources.pollution);
        if (oldRessources.bees != gameManager.Bees) FeedbackNumbers(beeText.transform, gameManager.Bees - oldRessources.bees);

        //Save current ressources as old ressources for next check
        gameManager.SetRessources(gameManager.ressources, oldRessources);
    }

    IEnumerator FlashWhite(Image img, Color c)
    {
        img.color = (positiveColor + c) / 2;
        yield return new WaitForSeconds(0.3f);
        foodFill.color = foodColor;
        energyFill.color = energyColor;
        wasteFill.color = wasteColor;
    }

    IEnumerator FlashRed(Image img, Color c)
    {
        img.color = (negativeColor + c) / 2;
        yield return new WaitForSeconds(0.3f);
        foodFill.color = foodColor;
        energyFill.color = energyColor;
        wasteFill.color = wasteColor;
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
