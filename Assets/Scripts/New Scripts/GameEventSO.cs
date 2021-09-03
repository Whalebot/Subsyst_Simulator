using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSO : ScriptableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual bool CheckRequirements() {
        return false;
    }

    public virtual void ExecuteEvent() { 
    
    }
}
