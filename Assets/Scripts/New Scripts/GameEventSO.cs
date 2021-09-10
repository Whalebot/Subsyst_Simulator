using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEventSO : ScriptableObject
{
    public Sprite image;
    public string title;
    public string description;
    public string buttonText;
    public int thresholdCounter = 0;
    public EventType type;
    public enum EventType { AddToPending, ExecuteInstantly}
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
