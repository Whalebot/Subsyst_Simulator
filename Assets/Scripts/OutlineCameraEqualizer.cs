using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineCameraEqualizer : MonoBehaviour
{
    public Camera duplicatedCamera;
    public Camera mainCamera;
    public GameObject mainCamObj;
    public GameObject dupCamObj;
    public bool dupCamCaptured;

    public void Start()
    {
        StartCoroutine(SearchCamCoroutine());

        //dupCamObj = mainCamObj.transform.GetChild(0).gameObject;
        //dupCamObj = mainCamObj.transform.Find("Camera_CameraNormalsTexture").gameObject;

        //duplicatedCamera = dupCamObj.gameObject.GetComponent<Camera>();


    }

    IEnumerator SearchCamCoroutine()
    {
        
        yield return new WaitForSeconds(1);

        dupCamObj = mainCamObj.transform.GetChild(0).gameObject;
        duplicatedCamera = dupCamObj.gameObject.GetComponent<Camera>();
        dupCamCaptured = true;


    }

    public void LateUpdate()
    {
        if(dupCamCaptured == true)
        {
            duplicatedCamera.orthographicSize = mainCamera.orthographicSize;
        }
    }

}
