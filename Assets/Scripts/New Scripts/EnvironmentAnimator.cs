using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAnimator : MonoBehaviour
{
    Animator anim;
    public UpgradeSO targetUpgrade;
    public ActionSO productionUpgrade;
    public int level;


    private void Start()
    {
        anim = GetComponent<Animator>();
        UpgradeManager.Instance.upgradeEvent += CheckUpgrade;
    }

    private void Update()
    {
        if (GameManager.paused && !GameManager.pauseAnimations)
            anim.speed = 0;

        else anim.speed = 1;
    }

    // Start is called before the first frame update
    public void CheckUpgrade(ActionSO a)
    {
        bool foundAction = a == productionUpgrade;
        ActionSO temp = a;

        if (!foundAction)
        {
            foreach (var item in MenuManager.Instance.substitutes)
            {
                if (productionUpgrade == item.mainItem)
                    foreach (var sub in item.substitutes)
                    {
                        if (a == sub)
                        {
                            foundAction = true;
                            temp = sub;
                        }
                    }
            }
        }

        if (a == targetUpgrade && targetUpgrade != null || foundAction && productionUpgrade != null && level <= UpgradeManager.Instance.CheckUpgradeNumber(temp))
        {
            UpgradeManager.Instance.upgradeEvent -= CheckUpgrade;
            Upgrade();
        }

    }

    private void OnEnable()
    {
        if (UpgradeManager.Instance == null) return;
        if (UpgradeManager.Instance.obtainedUpgrades.Contains(targetUpgrade) && targetUpgrade != null)
        {
            Upgrade();
            return;
        }



        if (productionUpgrade != null)
        {
            bool foundAction = UpgradeManager.Instance.obtainedUpgrades.Contains(productionUpgrade);
            ActionSO temp = productionUpgrade;

            if (!foundAction)
            {
                foreach (var item in MenuManager.Instance.substitutes)
                {
                    if (productionUpgrade == item.mainItem)
                        foreach (var sub in item.substitutes)
                        {
                            if (UpgradeManager.Instance.obtainedUpgrades.Contains(sub))
                            {
                                foundAction = true;
                                temp = sub;
                            }
                        }
                }
            }

            if (foundAction && productionUpgrade != null && level <= UpgradeManager.Instance.CheckUpgradeNumber(temp))
            {
                Upgrade();
            }
        }

    }

    void Upgrade()
    {
        UpgradeManager.Instance.upgradeEvent -= CheckUpgrade;
        anim.SetBool("isOn", true);
    }
}
