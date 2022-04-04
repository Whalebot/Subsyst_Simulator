using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    Renderer rend;
    Material mat;
    public bool mixColors;
    public int meatValue;
    public int vegetableValue;
    public int waste;
    public int pollution;
    public float ressourceRatioMod;
    public Color mainColor;
    public Color mixedColor;
    public Color meatColor;
    public Color vegetableColor;
    public Color pollutionColor;
    public GameManager gameManager;
    public float pollutionMultiplier;
    public float pollutionClamp;
    public float lerpValue = 0.5F;
    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();
        mat = rend.material;



    }


    // Update is called once per frame
    void Update()
    {
        meatValue = UpgradeManager.Instance.MeatUpgrades();
        vegetableValue = UpgradeManager.Instance.VegetableUpgrades();
        waste = gameManager.Waste;
        pollution = gameManager.Pollution;
        mixColors = !gameManager.disableGraphics;
        CalculateMainColor();

    }

    void CalculateMainColor()
    {
        if (GameManager.paused) return;
        float sum = Mathf.Clamp(meatValue + vegetableValue, 0.01F, 100000000);
        float foodRatio = (float)meatValue / sum;
        float energyRatio = (float)vegetableValue / sum;
        float wasteRatio = (float)waste / sum;
        float ressourceRatio = Mathf.Clamp(sum / ressourceRatioMod, 0.01F, 1);
        float pollutionRatio = Mathf.Clamp((float)pollution / 5000, 0, pollutionClamp);

        //print("Sum: " + sum + " Food Ratio: " + foodRatio);

        // mixedColor = ((foodRatio * foodColor) + (energyRatio * energyColor) + (wasteRatio * wasteColor));
        //mixedColor = ((foodRatio * foodColor) + (energyRatio * energyColor) + (wasteRatio * wasteColor)) * ((1 - pollutionRatio * pollutionMultiplier));
        mixedColor = (mainColor * (1 - ressourceRatio)) + (((foodRatio * meatColor) + (energyRatio * vegetableColor)) * (ressourceRatio)) * (1 - pollutionRatio * pollutionMultiplier);
        if (mixColors)
        {

            mat.color = Color.Lerp(mat.color, mixedColor, lerpValue);
        }
        else mat.color = Color.Lerp(mat.color, mainColor, lerpValue);
    }

    private void OnValidate()
    {
        rend = GetComponent<Renderer>();
        mat = rend.sharedMaterial;
        CalculateMainColor();
    }
}
