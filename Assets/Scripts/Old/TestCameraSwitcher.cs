using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestCameraSwitcher : MonoBehaviour
{

    //THIS SCRIPT MUST BE A COMPONENT OF CLICKABLE OBJECT

    //Camera refs
    public GameObject vcamObj;
    public CinemachineVirtualCamera cmvc1;
    public bool cameraSwitch;

    

    //Ray references
    Vector3 clickPos;
    bool clicked;

    //SidePanel reference
    public SidePanelSwiper sidePanelSwiper;
    public bool sideSwiperSwitch = false;

    public void Start()
    {
        cameraSwitch = true;
        sideSwiperSwitch = false;
    }

    public void OnMouseUp()
    {
        //Debug.Log("Released mouse button on gameobject: " + vcamObj.name);
        cameraSwitch = !cameraSwitch;
        vcamObj.gameObject.SetActive(cameraSwitch);

        //HERE THE TRANSFORM CHANGE HAPPENS.

        sideSwiperSwitch = !sideSwiperSwitch;
        if(sideSwiperSwitch == true)
        {
            sidePanelSwiper.SidePanelInner();
        }
        else if (sideSwiperSwitch == false)
        {
            sidePanelSwiper.SidePanelOuter();
        }
    }

    //public void Update()
    //{
    //    clicked = Input.touchCount > 0;
    //    if (clicked)
    //    {
    //        clickPos = Input.GetTouch(0).position;

    //    }
    //    else if (clicked = Input.GetMouseButtonDown(0))
    //    {
    //        clickPos = Input.mousePosition;

    //    }

    //    RaycastHit hit;
    //    Ray ray = Camera.main.ScreenPointToRay(clickPos);
    //    if (Physics.Raycast(ray, out hit) && clicked)
    //    {
    //        //This is what happens when you click on 3d object button.
    //        Debug.Log("Clicked on gameobject: " + hit.collider.name);
    //        cameraSwitch = !cameraSwitch;
    //        vcamObj.gameObject.SetActive(cameraSwitch);

    //    }
    //}



}
