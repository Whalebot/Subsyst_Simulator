using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementLinker : MonoBehaviour
{
    public GameObject prodLockIcon;
    public GameObject personalOffButton;

    public void Update()
    {
        if (prodLockIcon.activeInHierarchy == true)
        {
            personalOffButton.gameObject.SetActive(true);
        }
        else if (prodLockIcon.activeInHierarchy == false)
        {
            personalOffButton.gameObject.SetActive(false);

        }
    }

}
