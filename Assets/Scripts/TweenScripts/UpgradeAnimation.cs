using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAnimation : MonoBehaviour
{
    public bool triggered;
    public UpgradeSO targetUpgrade;
    public Vector3 initialScale;
    public Vector3 finalScale;
    public float speed = 0f;
    public float delay = 0f;
    public AnimationCurve curve;

    public void Start()
    {
        initialScale = new Vector3 (0f,0f,0f);
        LeanTween.scale(gameObject, initialScale, 0f);
        UpgradeManager.Instance.upgradeEvent += CheckUpgrade;

        if (targetUpgrade == null) print(gameObject);
    }



    public void CheckUpgrade(ActionSO a) {
        if (a == targetUpgrade) {
            UpgradeManager.Instance.upgradeEvent -= CheckUpgrade;
            Upgrade();
        }
    
    }

    public void Upgrade()
    {
        if (!triggered) {
            triggered = true;
        LeanTween.scale(gameObject, finalScale, speed).setDelay(delay).setEase(curve);
        }
    }
}
