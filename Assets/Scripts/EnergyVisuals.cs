using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnergyVisuals : MonoBehaviour
{
    public bool isOn;
    public Renderer rend;
    public Material mat;
    public Material energyMat;
    // Start is called before the first frame update
    void Awake()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;

    }

    private void OnValidate()
    {
     
    }

    [Button]
    public void EnergyMaterial()
    {
        isOn = true;
        rend.material = energyMat;
    }
    [Button]
    public void BaseMaterial()
    {
        isOn = false;
        rend.material = mat;
    }
}
