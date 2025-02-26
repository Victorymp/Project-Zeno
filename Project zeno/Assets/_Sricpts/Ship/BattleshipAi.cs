using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleshipAi : MonoBehaviour
{
    public enum AiState
    {
        Idle,
        Moving,
        Attacking
    }

    public AiState aiState;
    // Stores the  waypoint system
    [SerializeField] private Waypoint waypoint;

    [SerializeField] private float speed = 50f;

    [SerializeField] private float distance = 0.1f;

    [SerializeField] private float rotationSpeed = 5f;

    // Stores the current waypoint
    private Transform currentWaypoint;
    private int currentWaypointIndex;
    private NavMeshAgent agent;
    private Animator animator;
    private int speedHashId;

    // Stores the BattleShip object
    private GameObject BattleShip;

    // Stores the fire point
    [SerializeField] private Transform firePoint;

    // Stores the bullet prefab
    [SerializeField] private GameObject bulletPrefab;

    // Stores the bullet force
    [SerializeField] private float bulletForce = 100f;

    [SerializeField] private float raycastDistance = 100f;

    private Vector3 raycastDirection;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        speedHashId = Animator.StringToHash("Speed");

        // Set the current waypoint to the first waypoint
        currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        // set the current waypoint to the next waypoint
        currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);

        // Find the BattleShip object
        raycastDirection = transform.forward;

    }


    // Update is called once per frame
    void Update()
    {
        if (aiState == AiState.Idle)
        {
            Idle();
        }
        else if (aiState == AiState.Moving)
        {
            Move();
        }
        else if (aiState == AiState.Attacking)
        {
            Attack();
        }
    }

    void Idle()
    {
        // Calculate the direction to the waypoint
        Vector3 direction = (currentWaypoint.position - transform.position).normalized;

        // Create a rotation to face the waypoint
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the waypoint
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);

        // Check if the transform is close enough to the waypoint to move to the next one
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distance)
        {
            currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
        }
    }

    void Move()
    {
        // Define the raycast direction
        raycastDirection = transform.forward;

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastDistance))
        {
            // Check if the raycast hit an object with the "BattleShip" tag
            if (hit.collider.CompareTag("BattleShip"))
            {
                // Set the BattleShip's position as the destination for the agent
                agent.SetDestination(hit.transform.position);
            }
        }
        else
        {
            // If no BattleShip is detected, continue with the current waypoint
            agent.SetDestination(currentWaypoint.position);
        }
    }

    void Attack()
    {
        // Define the raycast direction
        Vector3 raycastDirection = transform.forward;

        // Define the raycast distance
        float raycastDistance = 100.0f; // Adjust this value as needed

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastDistance))
        {
            // Check if the raycast hit an object with the "BattleShip" tag
            if (hit.collider.CompareTag("BattleShip"))
            {
                // Call the Shoot method
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // Play the shoot animation
        //animator.SetTrigger("Shoot");

        // Play the shoot sound
        //audioSource.PlayOneShot(shootSound);

        // Create a bullet object
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the bullet component
        Bullet bulletComponent = bullet.GetComponent<Bullet>();

        // Set the bullet's damage
        //bulletComponent.damage = damage;

        // Apply a force to the bullet
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }


}
