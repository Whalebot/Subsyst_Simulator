using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProdButtTutorialer : MonoBehaviour
{
    public int counter;
    public int countLimit;
    //public bool isFood;
    //public bool isEnergy;
    //public bool isWasteM;
    public bool hasFulfilledTutorial;
    //public Tut2 tutManager3;
    //public ArrowActivator tutManag3;
    public Tut2 tutManager4;
    public ArrowActivator tutManag4;
    public ArrowActivator tutManagArrowAct2;

    public bool alsoActivatesArrow;

    public void ZContinueTutorial()
    {
        if (hasFulfilledTutorial == false)
        {
            counter++;

            if (counter >= countLimit)
            {
                tutManager4.Z1SwitchTxt();
                tutManag4.ZDeactivateArrow();

                if(alsoActivatesArrow == true)
                {
                    tutManagArrowAct2.ZActivateArrow();
                }

                hasFulfilledTutorial = true;
            }
        }
        
    }

}
