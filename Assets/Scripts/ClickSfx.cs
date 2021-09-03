using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSfx : MonoBehaviour
{
    public AudioSource click;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click.Play();
        }
    }
}
