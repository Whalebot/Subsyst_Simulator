using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class BoatFoodActivator : MonoBehaviour
{
    public Animator boatAnimator;
    [Button]
    public void StartBoatAnim()
    {
        boatAnimator.SetBool("productionBegan", true);
    }
}
