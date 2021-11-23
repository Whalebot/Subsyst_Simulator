using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public AICursor cursorScript;
    public GameObject foodButton;
    public GameObject energyButton;
    public GameObject wasteButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStart)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (GameManager.Instance.disableGraphics)
                    GameManager.Instance.EnableGraphics();
                else GameManager.Instance.DisableGraphics();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UIManager.Instance.ToggleUI();
                //if (UIManager.Instance.uiEnabled)
                //{
                //    UIManager.Instance.DisableUI();
                //}
                //else UIManager.Instance.EnableUI();
            }


            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(SimulateClick(foodButton));
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(SimulateClick(energyButton));
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(SimulateClick(wasteButton));
            }
            if (Input.GetMouseButtonDown(0))
            {
                // print("Click");
            }
        }

    }


    IEnumerator SimulateClick(GameObject g)
    {
        if (cursorScript.gameObject.activeInHierarchy)
            cursorScript.PerformClick();

        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(g, pointer, ExecuteEvents.pointerEnterHandler);
        ExecuteEvents.Execute(g, pointer, ExecuteEvents.pointerDownHandler);


        yield return new WaitForSeconds(0.1F);

        //print(g); g.GetComponent<Interactable>().ExecuteAction();
        ExecuteEvents.Execute(g, pointer, ExecuteEvents.submitHandler);
        ExecuteEvents.Execute(g, pointer, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.Execute(g, pointer, ExecuteEvents.pointerExitHandler);

    }
}
