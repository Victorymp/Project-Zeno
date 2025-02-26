using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiShip : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public LayerMask whatIsShip;

    // attack
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // states
    public float sightRange, attackRange;
    public bool inSightRange, inAttackRange;
    public WaypointMover waypointMover;

    public GunsVertical gunsVertical;

    public GameObject bullet;

    [SerializeField]public float health;
    


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // set waypoint mover
        waypointMover = GetComponent<WaypointMover>();
        waypointMover.Activate();

        gunsVertical = GetComponent<GunsVertical>();

    }

    private void Update()
    {
        // check for sight and attack range
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsShip);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsShip);

        if (!inSightRange && !inAttackRange) Patrol();
        if (inSightRange && !inAttackRange) Chase();
        if (inSightRange && inAttackRange) Attack();
        // if (health <= 0) Destroy(gameObject);
    }


    void Patrol()
    {
        waypointMover.Activate();
    }

    void Chase()
    {
        waypointMover.Deactivate();
        agent.SetDestination(target.position);
    }

    void Attack()
    {
        waypointMover.Deactivate();
        agent.SetDestination(transform.position);

        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            // attack code here
            // gunsVertical.SetEnemy("Player");
            gunsVertical.setBullet(bullet);
            gunsVertical.Fire();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }

    public void TakeDamage(float damage)
    {
        // take damage
        health -= damage;
        Debug.Log("Enemy Health: " + health);
    }

}
