using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public AICursor cursorScript;
    public GameObject foodButton;
    public GameObject energyButton;
    public GameObject wasteButton;
    public GameObject lines;
    public Interactable activeInteractable;
    public Interactable[] currentInteractables;
    public float positionLimitX;
    public float positionLimitY;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    [Button]
    public void ResetInteractables()
    {

        currentInteractables = new Interactable[0];
    }

    [Button]
    public void FindInteractablesInObject(GameObject GO)
    {

        currentInteractables = new Interactable[0];
        currentInteractables = GO.GetComponentsInChildren<Interactable>();
        activeInteractable = currentInteractables[0];
    }

    [Button]
    void FindNearestObject()
    {
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        foreach (var item in currentInteractables)
        {
            if (item != activeInteractable)
            {

                float tempDist = Vector2.Distance(item.transform.position, activeInteractable.transform.position);
                if (closestDistance == 0 || tempDist < closestDistance)
                {
                    closestDistance = tempDist;
                    closestInteractable = item;
                }
            }
        }
        activeInteractable = closestInteractable;
    }

    [Button]
    void FindNearestObjectUp()
    {
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        float closestDistanceX = 0;
        foreach (var item in currentInteractables)
        {
            if (item != activeInteractable)
            {

                float tempDist = item.transform.position.y - activeInteractable.transform.position.y;
                float tempDistX = item.transform.position.x - activeInteractable.transform.position.x;
                if (tempDist > 0)
                {
                    if (closestDistance == 0 || tempDist < closestDistance && Mathf.Abs(tempDistX) < positionLimitX || tempDist > 0 && Mathf.Abs(tempDistX) < closestDistanceX)
                    {
                        closestDistance = tempDist;
                        closestDistanceX = Mathf.Abs(tempDistX);
                        closestInteractable = item;
                    }
                }
            }
        }
        activeInteractable = closestInteractable;
        SelectActive();
    }
    [Button]
    void FindNearestObjectDown()
    {
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        foreach (var item in currentInteractables)
        {
            if (item != activeInteractable)
            {

                float tempDistY = item.transform.position.y - activeInteractable.transform.position.y;
                float tempDistX = item.transform.position.x - activeInteractable.transform.position.x;
                if (tempDistY < 0 && Mathf.Abs(tempDistX) < positionLimitX)
                    if (closestDistance == 0 || Mathf.Abs(tempDistY) < closestDistance)
                    {
                        closestDistance = tempDistY;
                        closestInteractable = item;
                    }
            }
        }
        activeInteractable = closestInteractable;
        SelectActive();
    }

    [Button]
    void FindNearestObjectRight()
    {
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        foreach (var item in currentInteractables)
        {
            if (item != activeInteractable)
            {

                float tempDistX = item.transform.position.x - activeInteractable.transform.position.x;
                float tempDistY = item.transform.position.y - activeInteractable.transform.position.y;
                if (tempDistX > 0 && Mathf.Abs(tempDistY) < positionLimitY)
                {
                    if (closestDistance == 0 || Mathf.Abs(tempDistX) < closestDistance)
                    {
                        closestDistance = Mathf.Abs(tempDistX);
                        closestInteractable = item;
                    }
                }
            }
        }
        activeInteractable = closestInteractable;
        SelectActive();
    }


    [Button]
    void FindNearestObjectLeft()
    {
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        foreach (var item in currentInteractables)
        {
            if (item != activeInteractable)
            {

                float tempDistX = item.transform.position.x - activeInteractable.transform.position.x;
                float tempDistY = item.transform.position.y - activeInteractable.transform.position.y;
                if (tempDistX < 0 && Mathf.Abs(tempDistY) < positionLimitY)
                {
                    if (closestDistance == 0 || Mathf.Abs(tempDistX) < closestDistance)
                    {
                        closestDistance = Mathf.Abs(tempDistX);
                        closestInteractable = item;
                    }
                }
            }
        }
        activeInteractable = closestInteractable;
        SelectActive();

    }

    void SelectActive()
    {
        AI.Instance.MoveCursorToNextObject(activeInteractable);
    }

    void ClickActive()
    {
        AI.Instance.ClickObject(activeInteractable);
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
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                lines.SetActive(!lines.gameObject.activeInHierarchy);
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

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                FindNearestObjectUp();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                FindNearestObjectRight();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                FindNearestObjectLeft();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                FindNearestObjectDown();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClickActive();
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

        yield return new WaitForSeconds(0.1F);
        SelectActive();
    }
}
