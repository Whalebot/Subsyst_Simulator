using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PostProcessingScript : MonoBehaviour
{
    public float ppValue;
    public Volume pp;
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.advanceTimeEvent += UpdatePP;
    }


    void UpdatePP()
    {
        if (GameManager.Instance != null)
            ppValue = 1 - ((float)GameManager.Instance.NaturalCapital / 1500);
        pp.weight = ppValue;
    }

    private void OnValidate()
    {
        UpdatePP();
    }
}
