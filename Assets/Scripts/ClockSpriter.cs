using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSpriter : MonoBehaviour
{
    TimeManager timeManager;
    public GameObject[] clockSprite;
    public GameObject happyClock;
    public GameObject sadClock;

    private void Start()
    {
        timeManager = TimeManager.Instance;
        timeManager.advanceTimeEvent += UpdateClock;
        EventManager.Instance.fedPopulation += HappyClock;
        EventManager.Instance.starvedPopulation+= SadClock;

    }

    void UpdateClock()
    {
        for (int i = 0; i < clockSprite.Length; i++)
        {
            clockSprite[i].SetActive(timeManager.time == i);

        }
    }

    void SadClock() {
        StartCoroutine("ActivateClockFace", sadClock);
    }

    void HappyClock() {
        StartCoroutine("ActivateClockFace", happyClock);
    }

    IEnumerator ActivateClockFace(GameObject g) {
        g.SetActive(true);
        yield return new WaitForSeconds(TimeManager.Instance.framesPerTime / 30);
        g.SetActive(false);
    }
}
