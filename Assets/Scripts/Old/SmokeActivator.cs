using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeActivator : MonoBehaviour
{
    public GameObject wasteSmoke1;
    public GameObject wasteSmoke2;
    public GameObject enerSmoke1;




    public void ActivateSmk01Waste()
    {
        wasteSmoke1.gameObject.SetActive(true);
    }

    public void ActivateSmk02Waste()
    {
        wasteSmoke2.gameObject.SetActive(true);
    }

    public void ActivateSmk01Energ()
    {
        enerSmoke1.gameObject.SetActive(true);
    }
}
