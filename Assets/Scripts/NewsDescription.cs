using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsDescription : MonoBehaviour
{
    public TextMeshProUGUI headline;
    public TextMeshProUGUI description;
    GameEventSO item;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetValues(GameEventSO so) {
        headline.text = so.title + " Day " + TimeManager.Instance.day;
        description.text = so.description;
        //  image.color = Color.red;
        image.color = Color.white;
        item = so;
    }
    public void OnClick()
    {

        image.color = Color.white;
        EventManager.Instance.ShowEvent(item);
    }
}
