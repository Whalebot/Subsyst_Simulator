using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecenterer : MonoBehaviour
{
    public Transform cinemachinePos;

    public Transform camCenter;
    public float speed;
    public AnimationCurve curve;

    public void Start()
    {
        camCenter.transform.position = cinemachinePos.position;
    }

    public void Recenter()
    {
        Vector3 camCenterVector = new Vector3(camCenter.position.x, camCenter.position.y, camCenter.position.z);
        LeanTween.moveLocal(gameObject, camCenterVector, speed).setEase(curve);
    }
}
