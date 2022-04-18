using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public bool makeyMakeyMode = false;
    public AICursor cursorScript;
    public GameObject foodButton;
    public GameObject energyButton;
    public GameObject wasteButton;
    public GameObject lines;
    public Interactable activeInteractable;
    public List<Interactable> interactableList;

    public GameObject lastGO;

    public float restartTime;
    public float timer;

    public float positionLimitX;
    public float positionLimitY;
    private void Awake()
    {
        Instance = this;
        timer = restartTime;
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

        if (activeInteractable.upItem != null && activeInteractable.upItem.gameObject.activeInHierarchy)
        {
            activeInteractable = activeInteractable.upItem;
            SelectActive();
            return;
        }
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

        if (activeInteractable.downItem != null && activeInteractable.downItem.gameObject.activeInHierarchy)
        {
            activeInteractable = activeInteractable.downItem;
            SelectActive();
            return;
        }

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
        if (activeInteractable.rightItem != null && activeInteractable.rightItem.gameObject.activeInHierarchy)
        {
            activeInteractable = activeInteractable.rightItem;
            SelectActive();
            return;
        }
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        float closestDistance2 = 0;
        foreach (var item in interactableList)
        {
            if (item != activeInteractable)
            {

                float tempDistX = item.transform.position.x - activeInteractable.transform.position.x;
                float tempDistY = item.transform.position.y - activeInteractable.transform.position.y;
                if (tempDistX > 0 && Mathf.Abs(tempDistY) < positionLimitY && Mathf.Abs(tempDistX) > 1)
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

        if (activeInteractable.leftItem != null && activeInteractable.leftItem.gameObject.activeInHierarchy)
        {
            activeInteractable = activeInteractable.leftItem;
            SelectActive();
            return;
        }
        Interactable closestInteractable = activeInteractable;
        float closestDistance = 0;
        float closestDistance2 = 0;
        foreach (var item in interactableList)
        {
            if (item != activeInteractable)
            {

                float tempDistX = item.transform.position.x - activeInteractable.transform.position.x;
                float tempDistY = item.transform.position.y - activeInteractable.transform.position.y;
                print(item + " " + tempDistX);
                if (tempDistX < 0 && Mathf.Abs(tempDistY) < positionLimitY && Mathf.Abs(tempDistX) > 1)
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
        if (!makeyMakeyMode) return;



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
            timer -= Time.deltaTime;
            if (timer <= 0 && makeyMakeyMode && !GameManager.attractMode)
            {
                GameManager.Instance.AttractMode();
            }

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
                timer = restartTime;
                GameManager.Instance.EndAttractMode();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(SimulateClick(energyButton));
                timer = restartTime;
                GameManager.Instance.EndAttractMode();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(SimulateClick(wasteButton));
                timer = restartTime;
                GameManager.Instance.EndAttractMode();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                FindNearestObjectUp();
                timer = restartTime;
                GameManager.Instance.EndAttractMode();

            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                FindNearestObjectRight();
                timer = restartTime;
                GameManager.Instance.EndAttractMode();

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                FindNearestObjectLeft();
                timer = restartTime;
                GameManager.Instance.EndAttractMode();

            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                FindNearestObjectDown();
                timer = restartTime;
                GameManager.Instance.EndAttractMode();

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClickActive(); 
                timer = restartTime;
                GameManager.Instance.EndAttractMode();

            }
            if (makeyMakeyMode)
                if (Input.GetKeyDown(KeyCode.D))
                {
                   // GameManager.Instance.ReloadScene();
                }
            if (Input.GetKeyDown(KeyCode.Tab))
            {

                makeyMakeyMode = !makeyMakeyMode;
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
