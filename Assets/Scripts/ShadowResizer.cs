using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowResizer : MonoBehaviour
{
    public RectTransform targetRt;
    public RectTransform shadowRt;

    public void Update()
    {
        shadowRt.sizeDelta = new Vector2(targetRt.sizeDelta.x, targetRt.sizeDelta.y);
    }
}
