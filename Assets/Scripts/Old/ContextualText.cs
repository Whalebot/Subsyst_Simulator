using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContextualText : MonoBehaviour
{
    public TextMeshProUGUI concept;
    public string originalTxt;
    public string contextualTxt;
    public GameObject newAgent;

    //public void Start()
    //{
    //    concept.text = originalTxt;
    //}

    public void Update()
    {
        if (newAgent.activeInHierarchy == true)
        {
            concept.text = contextualTxt;
        }
        else if (newAgent.activeInHierarchy == false)
        {
            concept.text = originalTxt;

        }
    }
}
