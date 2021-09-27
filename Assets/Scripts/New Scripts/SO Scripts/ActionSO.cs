using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ActionSO : ScriptableObject
{
    public string title;
    public string description;
    public Sprite icon;
    [Header("Cost")]
    [InlineProperty] public Ressources cost;

 
    [Header("Result")]
    [InlineProperty] public Ressources result;
    public float costMultiplier = 1.5F;
    public UpgradeSO dependantUpgrade;

}
