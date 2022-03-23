using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public bool mouseButtonHeld;
    public CinemachineVirtualCamera overviewCam;
    public Camera cam1;
    public Camera cam2;
    public Vector3 mouseDelta;
    Vector3 relativeMovement;
    public float cameraSpeedX = 1;
    public float cameraSpeedY = 1;

    public float zoomSpeed = 10;
    public float camSize = 120F;
    public float minZoom, maxZoom;
    public GameObject startCam;
    public GameObject introObject;

    [TabGroup("Cinematic Camera")] public CinemachineVirtualCamera cinematicCamera;
    [TabGroup("Cinematic Camera")] public float cinematicDuration;
    [TabGroup("Cinematic Camera")] public Transform cinematicTarget;
    [TabGroup("Cinematic Camera")] public Transform foodTarget;
    [TabGroup("Cinematic Camera")] public Transform energyTarget;
    [TabGroup("Cinematic Camera")] public Transform wasteTarget;
    [TabGroup("Cinematic Camera")] public Transform populationTarget;
    [TabGroup("Cinematic Camera")] public Transform townTarget;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        GameManager.Instance.gameStartEvent += StartGameCamera;
    }

    public void StartGameCamera()
    {
        introObject.SetActive(true);
        startCam.SetActive(false);
    }

    void Update()
    {
        if (!GameManager.gameStart) return;
        if (Input.GetMouseButtonDown(0)) { mouseButtonHeld = !CursorScript.Instance.foundInteractable; }
        if (Input.GetKeyDown(KeyCode.Space)) { SetCinematicCamera(cinematicTarget); }
        if (Input.GetMouseButtonUp(0)) mouseButtonHeld = false;

        if (mouseButtonHeld)
        {
            mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            Vector3 tempForward = overviewCam.transform.forward;
            tempForward.y = 0;
            tempForward = tempForward.normalized;
            relativeMovement = overviewCam.transform.right * mouseDelta.x * -cameraSpeedX + tempForward * mouseDelta.y * -cameraSpeedY;
            overviewCam.transform.position = overviewCam.transform.position + relativeMovement;
        }

        camSize += -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        camSize = Mathf.Clamp(camSize, minZoom, maxZoom);
        overviewCam.m_Lens.OrthographicSize = camSize;
        cam2.orthographicSize = cam1.orthographicSize;

    }

    public void SetCinematicCamera(Transform t)
    {
        cinematicTarget = t;
        StartCoroutine(TurnOffCinematicCamera());
    }

    public void SetCinematicCamera(CameraTargets t)
    {
        SetCameraTarget(t);
        StartCoroutine(TurnOffCinematicCamera());
    }

    public void SetCinematicCamera()
    {

        if (cinematicTarget != null)
            StartCoroutine(TurnOffCinematicCamera());
    }

    IEnumerator TurnOffCinematicCamera()
    {
        GameManager.Instance.PauseGame();
        GameManager.pauseAnimations = false;
        cinematicCamera.m_Follow = cinematicTarget;
        cinematicCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5F);
        GameManager.pauseAnimations = true;
        yield return new WaitForSeconds(cinematicDuration - 0.5F);
        GameManager.pauseAnimations = false;
        GameManager.Instance.Unpause();
        cinematicCamera.gameObject.SetActive(false);
        cinematicTarget = null;


    }

    public void SetCameraTarget(CameraTargets t)
    {
        switch (t)
        {
            case CameraTargets.None:
                cinematicCamera.m_Lens.OrthographicSize = 50;
                cinematicTarget = null;
                break;
            case CameraTargets.Food:
                cinematicCamera.m_Lens.OrthographicSize = 50;
                cinematicTarget = foodTarget;
                break;
            case CameraTargets.Energy:
                cinematicCamera.m_Lens.OrthographicSize = 50;
                cinematicTarget = energyTarget;
                break;
            case CameraTargets.Waste:
                cinematicCamera.m_Lens.OrthographicSize = 50;
                cinematicTarget = wasteTarget;
                break;
            case CameraTargets.Population:
                cinematicCamera.m_Lens.OrthographicSize = 50;
                cinematicTarget = populationTarget;
                break;
            case CameraTargets.Town:
                cinematicCamera.m_Lens.OrthographicSize = 35;
                cinematicTarget = townTarget;
                break;
            default:
                break;
        }
    }
}

public enum CameraTargets { None, Food, Energy, Waste, Population, Town }
