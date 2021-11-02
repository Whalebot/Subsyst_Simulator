using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiogasMaterial : MonoBehaviour
{
    Renderer rend;
    public Material oil;
    public Material bioGas;
    public UpgradeSO upgrade;

    public void Start()
    {
        rend = GetComponent<Renderer>();
        TimeManager.Instance.advanceTimeEvent += CheckUpgrades;
    }

    // Start is called before the first frame update
    private void CheckUpgrades()
    {
        if (UpgradeManager.Instance.obtainedUpgrades.Contains(upgrade)) rend.material = bioGas;
    }
}
