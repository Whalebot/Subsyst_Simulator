using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackText : MonoBehaviour
{
    public Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        transform.Translate(dir);
    }
}
