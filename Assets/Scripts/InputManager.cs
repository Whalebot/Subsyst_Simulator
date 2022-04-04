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
    public List<Interactable> interactableList;

    public GameObject lastGO;

    public float positionLimitX;
    public float positionLimitY;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        interactableList = new List<Interactable>();
        UpgradeManager.Instance.upgradeEvent += SearchObjects;
    }

    void SearchObjects(ActionSO so)
    {
        if (lastGO != null)
            FindInteractablesInObject();
    }

    [Button]
    public void ResetInteractables()
    {

        interactableList.Clear();
    }

    [Button]
    public void FindInteractablesInObject()
    {

        ResetInteractables();

        lastGO.GetComponentsInChildren(interactableList);
        List<Interactable> removeList = new List<Interactable>();
        foreach (var item in interactableList)
        {
            if (item.requiresUpgrade) removeList.Add(item);
            //  interactableList.Remove(item);
        }
        foreach (var item in removeList)
        {
            interactableList.Remove(item);
        }

        if (activeInteractable != null)
        {
            FindNearestObject();
        }
        else if (interactableList.Count > 0)
            activeInteractable = interactableList[0];
    }

    [Button]
    public void FindInteractablesInObjectTab(GameObject GO)
    {

        ResetInteractables();
        //currentInteractables = 
        lastGO = GO;
        GO.GetComponentsInChildren(interactableList);
        List<Interactable> removeList = new List<Interactable>();
        foreach (var item in interactableList)
        {
            if (item.requiresUpgrade) removeList.Add(item);
            //  interactableList.Remove(item);
        }
        foreach (var item in removeList)
        {
            interactableList.Remove(item);
        }


        if (interactableList.Count > 0)
            activeInteractable = interactableList[0];
    }

    [Button]
    void FindNearestObject()
    {
        DeselectActive();
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        foreach (var item in interactableList)
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
        SelectActive();
    }

    [Button]
    void FindNearestObjectUp()
    {
        DeselectActive();
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        float closestDistanceX = 0;
        foreach (var item in interactableList)
        {
            if (item != activeInteractable)
            {

                float tempDistY = item.transform.position.y - activeInteractable.transform.position.y;
                float tempDistX = item.transform.position.x - activeInteractable.transform.position.x;
                if (tempDistY > 0 && Mathf.Abs(tempDistX) < positionLimitX)
                {
                    if (closestDistance == 0 || Mathf.Abs(tempDistY) <= closestDistance)
                    {
                        closestDistance = tempDistY;
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
        DeselectActive();
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        float closestDistance2 = 0;
        foreach (var item in interactableList)
        {
            if (item != activeInteractable)
            {

                float tempDistY = item.transform.position.y - activeInteractable.transform.position.y;
                float tempDistX = item.transform.position.x - activeInteractable.transform.position.x;
                if (tempDistY < 0 && Mathf.Abs(tempDistX) < positionLimitX)
                    if (closestDistance == 0 || Mathf.Abs(tempDistY) <= closestDistance)
                    {
                        //  if (Mathf.Abs(tempDistY) <= closestDistance2 || closestDistance2 == 0)
                        {
                            closestDistance = tempDistY;
                            closestDistance2 = tempDistX;
                            closestInteractable = item;
                        }
                    }
            }
        }
        activeInteractable = closestInteractable;
        SelectActive();
    }

    [Button]
    void FindNearestObjectRight()
    {
        DeselectActive();
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        float closestDistance2 = 0;
        foreach (var item in interactableList)
        {
            if (item != activeInteractable)
            {

                float tempDistX = item.transform.position.x - activeInteractable.transform.position.x;
                float tempDistY = item.transform.position.y - activeInteractable.transform.position.y;
                if (tempDistX > 0 && Mathf.Abs(tempDistY) < positionLimitY)
                {
                    if (closestDistance == 0 && closestDistance2 == 0 || Mathf.Abs(tempDistX) < closestDistance)
                    {
                        closestDistance = Mathf.Abs(tempDistX);
                        closestDistance2 = Mathf.Abs(tempDistY);
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
        DeselectActive();
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        float closestDistance2 = 0;
        foreach (var item in interactableList)
        {
            if (item != activeInteractable)
            {

                float tempDistX = item.transform.position.x - activeInteractable.transform.position.x;
                float tempDistY = item.transform.position.y - activeInteractable.transform.position.y;
                if (tempDistX < 0 && Mathf.Abs(tempDistY) < positionLimitY)
                {
                    if (closestDistance == 0 && closestDistance2 == 0 || Mathf.Abs(tempDistX) < closestDistance)
                    {
                        closestDistance = Mathf.Abs(tempDistX);
                        closestDistance2 = Mathf.Abs(tempDistY);
                        closestInteractable = item;
                    }
                }
            }
        }
        activeInteractable = closestInteractable;
        SelectActive();

    }

    void DeselectActive()
    {
        AI.Instance.MoveCursorToNextObject(activeInteractable);
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(activeInteractable.gameObject, pointer, ExecuteEvents.pointerExitHandler);

    }

    void SelectActive()
    {
        AI.Instance.MoveCursorToNextObject(activeInteractable);
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(activeInteractable.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

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
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                AI.Instance.showCursor = !AI.Instance.showCursor;
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
