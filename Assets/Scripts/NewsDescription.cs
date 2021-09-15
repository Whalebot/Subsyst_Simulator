using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsDescription : MonoBehaviour
{
    public TextMeshProUGUI headline;
    public TextMeshProUGUI description;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetValues(GameEventSO so) {
        headline.text = so.title;
        description.text = so.description;
        image.color = Color.red;
    }
    public void OnClick()
    {

        image.color = Color.white;
    }
}
