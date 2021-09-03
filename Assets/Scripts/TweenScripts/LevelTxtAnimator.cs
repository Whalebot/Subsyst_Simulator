using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTxtAnimator : MonoBehaviour
{
    public GameObject lvlTxtObject;
    public bool switcher;
    public float waiter;
    public int loopLimit;


    public void Start()
    {

        switcher = true;
        lvlTxtObject.gameObject.SetActive(true);
    }

    public void LvlUpBlinker()
    {
        StartCoroutine(SwitchSparkCoroutine());
    }

    public void Update()
    {
        if (switcher == true)
        {
            lvlTxtObject.gameObject.SetActive(switcher);
        }
        else if (switcher == false)
        {
            lvlTxtObject.gameObject.SetActive(switcher);
        }
    }

    IEnumerator SwitchSparkCoroutine()
    {
        for (int i = 0; i < loopLimit; i++)
        {
            switcher = !switcher;
            yield return new WaitForSeconds(waiter);
        }


    }


}
