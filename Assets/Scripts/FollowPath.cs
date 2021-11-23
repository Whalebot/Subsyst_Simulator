using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class FollowPath : MonoBehaviour
{
    public PathCreator pathCreator;
    public bool reversePath;
    public float speed = 5;
    public float distanceTravelled;
    public float length;
    bool finished;
    // Start is called before the first frame update
    void Start()
    {
        length = pathCreator.path.length;
        if (reversePath) distanceTravelled = length;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.paused) return;
        if (finished) return;
        if (reversePath)
        {
            distanceTravelled -= speed * Time.deltaTime;
            if (distanceTravelled < 0)
            {
                FinishTravel();
            }

        }
        else
        {
            distanceTravelled += speed * Time.deltaTime;
            if (distanceTravelled > length)
            {
                FinishTravel();
            }

        }

        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

    }

    void FinishTravel()
    {
        finished = true;
        Destroy(gameObject);
    }
}
