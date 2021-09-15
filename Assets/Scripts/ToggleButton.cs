using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public bool toggle;
    public GameObject toggleObject;
    public ContentSizeFitter sizeFitter;
    IEnumerator SetDirty()
    {
        sizeFitter.enabled = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        sizeFitter.enabled = true;
    }
    // Start is called before the first frame update
    void Awake()
    {
        sizeFitter = transform.parent.GetComponent<ContentSizeFitter>();
    }

    // Update is called once per frame
    public void ToggleObject()
    {
        toggle = !toggle;
        toggleObject.SetActive(toggle);
        StartCoroutine(SetDirty());
    }
}
