using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerManager : MonoBehaviour
{
    public int cattleUpgrades = 0;
    public UpgradeSO cattleUpgrade;
    public GameObject[] cattleGO;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.updateGameState += UpdateVisuals;
    }

    // Update is called once per frame
    public void UpdateVisuals()
    {
        cattleUpgrades = UpgradeManager.Instance.CheckUpgradeNumber(cattleUpgrade);
        for (int i = 0; i < cattleUpgrades; i++)
        {
            if (cattleGO.Length-1 < i) return;
            cattleGO[i].GetComponentInChildren<Animator>().SetBool("isOn", true);
        }

    }
}
