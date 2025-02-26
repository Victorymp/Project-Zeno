using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] public float waypointRadius = 1f;
    private void OnDrawGizmos()
    {
        // Draw a sphere at each waypoint
        foreach (Transform child in transform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(child.position, waypointRadius);
        }
        Gizmos.color = Color.green;
        // Draw a line between each waypoint
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        // Draw a line between the last and first waypoint
        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            // If there is no current waypoint, return the first waypoint
            return transform.GetChild(0);
        }
        // Get the index of the current waypoint
        int currentIndex = currentWaypoint.GetSiblingIndex();
        // Get the next waypoint
        if (currentIndex < transform.childCount - 1)
        {
            // If the current waypoint is the last waypoint, return the first waypoint
            return transform.GetChild(currentIndex + 1);
        }
        else
        {
            return transform.GetChild(0);
        }
    }
}
