using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;

    [SerializeField] private int currentWaypointIndex;
    [SerializeField] private Vector3 currentWaypoint;

    [SerializeField] private float distanceToWaypoint;
    [SerializeField] private float acceptableDistanceToWaypoint = 3.0f;

    [SerializeField] private bool enemyAtWaypoint = false;

    private Rigidbody enemyRb;
    private NavMeshAgent enemyNavAgent;
    private float walkSpeed = 5.0f;
    [SerializeField] private float turnSpeed = 1.0f;

    private Transform playerTransform;

    private Enemy enemyScript;

    [SerializeField] private float idleAtWaypointTime;
    [SerializeField] private float timeSpentIdling = 0;
    
    // Start is called before the first frame update
    private void Start()
    {
        this.enemyRb = this.GetComponent<Rigidbody>();
        this.enemyScript = this.GetComponent<Enemy>();
        this.enemyNavAgent = this.GetComponent<NavMeshAgent>();

        currentWaypointIndex = 0;
        currentWaypoint = waypoints[currentWaypointIndex].position;
        enemyNavAgent.destination = currentWaypoint;

        walkSpeed = enemyScript.GetEnemyDetails().walkSpeed;
        idleAtWaypointTime = enemyScript.GetEnemyDetails().timeSpentIdle;
        enemyNavAgent.speed = walkSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (enemyScript.GetEnemyIsDead() == false)
        {
            CheckWaypoint();
            MoveToWaypoint();
        }
    }

    private void CheckWaypoint()
    {
        // Only patrol if we are not chasing the player
        if (enemyScript.GetEnemyIsDead() == false)
        {
            if (enemyScript.GetIsAwarePlayer() == false)
            {
                distanceToWaypoint = Vector3.Distance(this.transform.position, currentWaypoint);
                if (distanceToWaypoint <= acceptableDistanceToWaypoint)
                {
                    //We've hit our waypoint. start a timer
                    enemyScript.GetEnemyAnimator().SetBool("IsWalking", false);

                    enemyAtWaypoint = true;
                    timeSpentIdling = timeSpentIdling + Time.deltaTime;
                    if (timeSpentIdling > idleAtWaypointTime)
                    {
                        //When our Timer hits zero, change the waypoint
                        enemyScript.GetEnemyAnimator().SetTrigger("WalkTrigger");
                        enemyScript.GetEnemyAnimator().SetBool("IsWalking", true);
                        currentWaypointIndex++;

                        if (currentWaypointIndex == waypoints.Count)
                        {
                            currentWaypointIndex = 0;
                        }

                        currentWaypoint = waypoints[currentWaypointIndex].position;

                        //Tell our NavMeshAgent where to go
                        enemyNavAgent.destination = currentWaypoint;

                        timeSpentIdling = 0;
                        enemyAtWaypoint = false;
                    }
                }
            }
        }
    }

    private void MoveToWaypoint()
    {
        if (enemyAtWaypoint == false)
        {
            
        }
    }

    public void TargetPlayer(Transform target)
    {
        playerTransform = target;
        currentWaypoint = playerTransform.position;
        enemyNavAgent.destination = currentWaypoint;
    }

    public void DisableNavMeshAgent()
    {
        enemyNavAgent.enabled = false;
    }
}
