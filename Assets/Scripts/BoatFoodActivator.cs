using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFoodActivator : MonoBehaviour
{
    public Animator boatAnimator;

    public void StartBoatAnim()
    {
        boatAnimator.SetBool("productionBegan", true);
    }
}
