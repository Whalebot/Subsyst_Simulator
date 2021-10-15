using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatacylsmGO : MonoBehaviour
{
    public GameEventSO cataclysm;
    public GameObject GO;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.cataclysmTrigger += TriggerEvent;
        GO.SetActive(false);
    }

    void TriggerEvent(GameEventSO p)
    {
        if (p == cataclysm) GO.SetActive(true);
    }
}
