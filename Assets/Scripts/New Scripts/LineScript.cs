using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public LineRenderer line;
    public List<Transform> targets;
    Vector3[] pos;
    void Start()
    {
        
    }

    public void RenderLine() {
        pos = new Vector3[targets.Count];
        for (int i = 0; i < targets.Count; i++)
        {
            pos[i] = targets[i].position;
        }
        line.positionCount = targets.Count;
        line.SetPositions(pos);
    }

    private void OnValidate()
    {
        RenderLine();
    }
}
