using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public bool toggle;
    public GameObject toggleObject;
    public ContentSizeFitter sizeFitter;
    public ContentSizeFitter sizeFitter2;
    public LayoutGroup layoutGroup;
    IEnumerator SetDirty()
    {
        sizeFitter.enabled = false;

        yield return new WaitForEndOfFrame();

        sizeFitter.enabled = true;
        layoutGroup.CalculateLayoutInputVertical();
        sizeFitter2.enabled = false;

        yield return new WaitForEndOfFrame();
        sizeFitter2.enabled = true;
        layoutGroup.CalculateLayoutInputVertical();
    }
    // Start is called before the first frame update
    void Awake()
    {
        sizeFitter = transform.parent.GetComponent<ContentSizeFitter>();
        sizeFitter2 = transform.GetComponent<ContentSizeFitter>();
        layoutGroup.GetComponent<LayoutGroup>();
    }

    private void OnEnable()
    {
        StartCoroutine(SetDirty());
    }

    // Update is called once per frame
    public void ToggleObject()
    {
        toggle = !toggle;
        toggleObject.SetActive(toggle);
        StartCoroutine(SetDirty());
    }
}
