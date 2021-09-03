using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkAnimator : MonoBehaviour
{
    public GameObject sparkParent;
    public GameObject sparkObject;
    public GameObject sparkObject2;
    public bool switcher;
    public float waiter;
    public int loopLimit;

    //LeanTween references.
    public float leanTime;
    public Vector3 scaleStart = new Vector3(0f, 0f, 0f);
    public Vector3 scaleBig = new Vector3 (1f, 1f, 1f);
    public AnimationCurve curve;

    public void Start()
    {
        //LeanTween lines.
        LeanTween.scale(sparkParent, scaleStart, 0f);

        switcher = true;
        sparkObject.gameObject.SetActive(true);
        sparkObject2.gameObject.SetActive(false);
    }

    //public void LvlUpSparker()
    //{
    //    LeanTween.scale(sparkParent, scaleBig, leanTime).setEase(curve);
    //    StartCoroutine(SwitchSparkCoroutine());
    //}

    public void Update()
    {
        if (switcher == true)
        {
            sparkObject.gameObject.SetActive(switcher);
            sparkObject2.gameObject.SetActive(!switcher);
        }
        else if (switcher == false)
        {
            sparkObject.gameObject.SetActive(switcher);
            sparkObject2.gameObject.SetActive(!switcher);
        }
    }

    //IEnumerator SwitchSparkCoroutine()
    //{
    //    for (int i = 0; i < loopLimit; i++)
    //    {
    //        switcher = !switcher;
    //        yield return new WaitForSeconds(waiter);
    //    }

    //    LeanTween.scale(sparkParent, scaleStart, leanTime).setEase(curve);


    //    //sparkParent.gameObject.SetActive(false);
    //}


}
