using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ActionSO : ScriptableObject
{
    public string title;
    public string description;
    public Sprite icon;
    public CameraTargets cameraTarget;
    public UpgradeSO dependantUpgrade;

}

[System.Serializable]
public class UpgradeLevel {
    public int upgradeCost;

    [Header("Cost")]
    [InlineProperty] public Ressources cost;
    [Header("Result")]
    [InlineProperty] public Ressources result;
}