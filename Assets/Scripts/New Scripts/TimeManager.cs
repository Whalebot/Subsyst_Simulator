using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public delegate void TimeEvent();
    public TimeEvent advanceGameEvent;
    public TimeEvent advanceTimeEvent;
    public static bool canSmallScale;
    public float gameUpdateFrequency;
    public int day = 1;
    public int time;
    public int framesPerTime;
    int frameCounter;
    bool day10;
    bool day25;
    bool day50;
    bool day75;
    bool day100;
    bool day150;
    bool day200;

    [FoldoutGroup("Time Slider")] public Slider timeSlider;
    [FoldoutGroup("Time Slider")] public TextMeshProUGUI timeText;

    private void Awake()
    {
        Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        day = 1;
        canSmallScale = true;
        InitializeSlider();
        //GameManager.Instance.updateGameState += AdvanceTime;
    }

    public void InitializeSlider()
    {
        framesPerTime = 60 - (int)timeSlider.value;
        timeSlider.value = 60 - framesPerTime;
        timeText.text = (1 + (float)(timeSlider.value / framesPerTime)) / 1.5F + "x";
    }

    public void SetGameSpeed()
    {

        framesPerTime = 60 - (int)timeSlider.value;
        timeText.text = (1 + (float)(timeSlider.value / framesPerTime)) / 1.5F + "x";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.paused) return;
        //Waits framesPerTime amount of frames before advancing the the game time
        frameCounter++;
        if (frameCounter >= framesPerTime)
        {
            AdvanceTime();
        }
    }

    public void AdvanceTime()
    {
        frameCounter = 0;
        time++;
        //When game time has completed a cycle, the game advances its game state
        if (time >= 7)
        {
            day++;
            time = 0;
            advanceGameEvent?.Invoke();
            SendHeatmapData();
            //print(Time.realtimeSinceStartup);
        }
        canSmallScale = true;
        advanceTimeEvent?.Invoke();

    }

    public void SendHeatmapData()
    {
        switch (day)
        {
            case 200: if (!day200) { day200 = true; StartCoroutine(Telemetry.Instance.HeatMap()); } break;
            case 150: if (!day150) { day150 = true; StartCoroutine(Telemetry.Instance.HeatMap()); } break;
            case 100: if (!day100) { day100 = true; StartCoroutine(Telemetry.Instance.HeatMap()); } break;
            case 75: if (!day75) { day75 = true; StartCoroutine(Telemetry.Instance.HeatMap()); } break;
            case 50: if (!day50) { day50 = true; StartCoroutine(Telemetry.Instance.HeatMap()); } break;
            case 25: if (!day25) { day25 = true; StartCoroutine(Telemetry.Instance.HeatMap()); } break;
            case 10: if (!day10) { day10 = true; StartCoroutine(Telemetry.Instance.HeatMap()); } break;
            default: break;
        }
    }
}
