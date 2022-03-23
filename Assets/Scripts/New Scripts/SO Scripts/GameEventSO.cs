using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameEventSO : ScriptableObject
{
    public bool showInPaper;
    [PreviewField]
    public Sprite image;
    public string title;
    public string description;
    public string buttonText;

    public CameraTargets cameraTarget;

    public int thresholdCounter = 0;
    
    public EventType type;
    public enum EventType { AddToPending, ExecuteInstantly, DoNotShow}
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
