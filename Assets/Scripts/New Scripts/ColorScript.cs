using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    Renderer rend;
    Material mat;
    public bool mixColors;
    public int food;
    public int energy;
    public int waste;
    public int pollution;
    public Color mainColor;
    public Color mixedColor;
    public Color foodColor;
    public Color energyColor;
    public Color wasteColor;
    public GameManager gameManager;
    public float pollutionMultiplier;
    public float pollutionClamp;
    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();
        mat = rend.material;
    }

    // Update is called once per frame
    void Update()
    {
        food = gameManager.Food;
        energy = gameManager.Energy;
        waste = gameManager.Waste;
        pollution = gameManager.Pollution;
        CalculateMainColor();

    }

    void CalculateMainColor()
    {
        float sum = food + energy + waste;
        float foodRatio = (float)food / sum;
        float energyRatio = (float)energy / sum;
        float wasteRatio = (float)waste / sum;
        float ressourceRatio = Mathf.Clamp(sum / 1000, 0, 1);
        float pollutionRatio = Mathf.Clamp((float)pollution / 5000, 0, pollutionClamp);

        //print("Sum: " + sum + " Food Ratio: " + foodRatio);

        // mixedColor = ((foodRatio * foodColor) + (energyRatio * energyColor) + (wasteRatio * wasteColor));
        //mixedColor = ((foodRatio * foodColor) + (energyRatio * energyColor) + (wasteRatio * wasteColor)) * ((1 - pollutionRatio * pollutionMultiplier));
        mixedColor = (mainColor * (1 - ressourceRatio)) + (((foodRatio * foodColor) + (energyRatio * energyColor) + (wasteRatio * wasteColor)) * (ressourceRatio)) * (1 - pollutionRatio * pollutionMultiplier);
        if (mixColors)
        {
            mat.color = mixedColor;
        }
        else mat.color = mainColor;
    }

    private void OnValidate()
    {
        rend = GetComponent<Renderer>();
        mat = rend.sharedMaterial;
        CalculateMainColor();
    }
}
