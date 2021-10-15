using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotDescription : MonoBehaviour
{
    public GameObject defaultButton;
    public GameObject[] botDescriptions;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    public void SelectDefault() {
        StartCoroutine(PressDefaultBot());
    }

    IEnumerator PressDefaultBot() {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(defaultButton, pointer, ExecuteEvents.pointerEnterHandler);
        ExecuteEvents.Execute(defaultButton, pointer, ExecuteEvents.pointerDownHandler);


        yield return new WaitForSeconds(0.1F);
        ExecuteEvents.Execute(defaultButton, pointer, ExecuteEvents.submitHandler);
        ExecuteEvents.Execute(defaultButton, pointer, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.Execute(defaultButton, pointer, ExecuteEvents.pointerExitHandler);
    }

    public void SelectBot(int i)
    {
        foreach (var item in botDescriptions)
        {
            item.SetActive(false);
        }
        if (i > -1)
            botDescriptions[i].SetActive(true);
    }
}
