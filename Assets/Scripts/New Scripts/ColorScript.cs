using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    Renderer rend;
    Material mat;
    public bool mixColors;
    public Color mainColor;
    public Color mixedColor;
    public Color foodColor;
    public Color energyColor;
    public Color wasteColor;
    public GameManager gameManager;
    public float pollutionMultiplier;
    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();
        mat = rend.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (mixColors)
        {
            CalculateMainColor();
            mat.color = mixedColor;
        }
        else
            mat.color = mainColor;
    }

    void CalculateMainColor()
    {
        int sum = gameManager.Food + gameManager.Energy + gameManager.Waste;
        float foodRatio = (float)gameManager.Food / sum;
        float energyRatio = (float)gameManager.Energy / sum;
        float wasteRatio = (float)gameManager.Waste / sum;
        float pollutionRatio = (float)gameManager.Pollution / sum;

        //print("Sum: " + sum + " Food Ratio: " + foodRatio);

        mixedColor = ((foodRatio * foodColor) + (energyRatio * energyColor) + (wasteRatio * wasteColor)) * ((1 - pollutionRatio * pollutionMultiplier));
    }

    private void OnValidate()
    {
        CalculateMainColor();
    }
}
