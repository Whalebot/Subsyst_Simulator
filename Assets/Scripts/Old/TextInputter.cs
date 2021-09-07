using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputter : MonoBehaviour
{
    public bool enableTxt;
    public GameObject txt, inputfield;

    public void Start()
    {
        inputfield.SetActive(false);
    }

    public void OnGUI()
    {
        if (GUILayout.Button("write text"))
        {
            enableTxt = !enableTxt;
        }

        if (enableTxt)
        {
            inputfield.SetActive(true);

            InputField inputFieldCo = inputfield.GetComponent<InputField>();
            Debug.Log(inputFieldCo.text);
            txt.GetComponent<Text>().text = inputFieldCo.text;
        }
    }
}
