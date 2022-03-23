using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAnimator : MonoBehaviour
{
    Animator anim;
    public UpgradeSO targetUpgrade;

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
        if (a == targetUpgrade)
        {
            UpgradeManager.Instance.upgradeEvent -= CheckUpgrade;
            Upgrade();
        }

    }

    void Upgrade()
    {
        UpgradeManager.Instance.upgradeEvent -= CheckUpgrade;
        anim.SetBool("isOn", true);
    }
}
