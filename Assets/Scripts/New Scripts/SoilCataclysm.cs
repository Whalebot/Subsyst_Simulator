using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilCataclysm : MonoBehaviour
{
    public GameEventSO cataclysm;
    public Color col;
    Renderer rend;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        mat = rend.material;
        EventManager.Instance.cataclysmTrigger += TriggerEvent;
    }

    void TriggerEvent(GameEventSO p)
    {
        if (p == cataclysm)
            mat.color = col;
    }
}
