using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventDescription : MonoBehaviour
{
    public GameEventSO eventSO;
    public Image image;              
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI buttonText;



    public void DisplayEvent(GameEventSO SO) {
        eventSO = SO;
        image.sprite = SO.image;
        titleText.text = SO.title;
        descriptionText.text = SO.description;
        buttonText.text = SO.buttonText;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CloseEventDescription();
        }
    }

    public void CloseEventDescription() {
        GameManager.Instance.ResumeGame();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(AutoCloseDescription());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator AutoCloseDescription() {
        yield return new WaitForSeconds(10);
        CloseEventDescription();
    }
}
