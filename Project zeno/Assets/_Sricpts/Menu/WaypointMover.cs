using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    // Stores the  waypoint system
    [SerializeField] private Waypoint waypoint;

    [SerializeField] private float speed = 50f;

    [SerializeField] private float distance = 30f;

    [SerializeField] private float rotationSpeed = 5f;

    // Stores the rotation to the waypoint
    private Quaternion roationToWaypoint;

    // Stores the direction to the waypoint
    private Vector3 direction;

    // Stores the current waypoint
    private Transform currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        // Set the current waypoint to the first waypoint
        currentWaypoint = waypoint.GetNextWaypoint(null);
        transform.position = currentWaypoint.position;

        // set the current waypoint to the next waypoint
        currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        // // Calculate the direction to the waypoint
        // Vector3 direction = (currentWaypoint.position - transform.position).normalized;

        // // Create a rotation to face the waypoint
        // Quaternion lookRotation = Quaternion.LookRotation(direction);

        // // Smoothly rotate towards the waypoint
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);
        // Check if the transform is close enough to the waypoint to move to the next one
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distance)
        {
            currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
        }
        RoationToWaypoint();
    }

    public void Activate()
    {
        if(!enabled) enabled = true;
    }

    public void Deactivate()
    {
        enabled = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, currentWaypoint.position);
    }

    void RoationToWaypoint()
    {
        // Calculate the direction to the waypoint

        direction = (currentWaypoint.position - transform.position).normalized;

        roationToWaypoint = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the waypoint
        transform.rotation = Quaternion.Slerp(transform.rotation, roationToWaypoint, Time.deltaTime * rotationSpeed);
    }
}
