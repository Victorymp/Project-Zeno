using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirpersonCamera : MonoBehaviour
{
    // the target that the camera will follow
    public Transform target;
    // the distance the camera will stay from the target
    public float distanceFromTarget = 5;
    // boolean to toggle following on and off
    public bool follow = true;

    float displacmentY = 1.5f;
    /*
    Update the cameras position to follow the target
    */
    void Update ()
    {
        // if allowed to follow the target
        if (follow)
        {
            // move within "distanceFromTarget" metres of the target
            // but retain the same camera rotation at all times
            Vector3 newPos = target.position - (Vector3.forward * distanceFromTarget);
            newPos.y = target.position.y + displacmentY;
            transform.position += (newPos - transform.position) * 0.1f;
            transform.position.Set(transform.position.x, transform.position.y + displacmentY, transform.position.z);
            // match the rotation of the target so we look directly at it
            transform.rotation = target.rotation;
            transform.LookAt(target);
        }
        else // otherwise just look at the target
        {
            // use LookAt(...) to point towards the target
            transform.LookAt(target);
        }
    }

}
