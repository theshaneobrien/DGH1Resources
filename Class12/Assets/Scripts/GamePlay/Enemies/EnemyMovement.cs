using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

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

    // Chase player variables here
    private bool isChasingPlayer = false;
    private bool lostSightOfPlayer = false;

    private float distanceToPlayer = 0.0f;
    [SerializeField] private float engagePlayerDistance = 5.0f;
    [FormerlySerializedAs("repositionToEngagePlayer")] [SerializeField] private float repositionToEngagePlayerDistance = 10.0f;

    private Vector3 lastKnownPlayerPostition = new Vector3();
    private float distanceToLastKnownPost;
    
    //This is rotation vars
    [SerializeField] private GameObject enemyRigSpine;
    private Vector3 playerDirection = new Vector3();
    
    
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
            if (isChasingPlayer == false)
            {
                CheckWaypoint();
            }
            else
            {
                if (lostSightOfPlayer == false)
                {
                    ChasePlayer();
                }
                else
                {
                    MoveToLastKnownPlayerPosition();
                }
            }
        }
    }

    private void CheckWaypoint()
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

                // Tell our NavMeshAgent where to go
                enemyNavAgent.destination = currentWaypoint;

                timeSpentIdling = 0;
                enemyAtWaypoint = false;
            }
        }
    }

    private void ChasePlayer()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, playerTransform.position);

        enemyNavAgent.destination = playerTransform.position;
        // This is going to tell the navmesh the player is the enemies destination
        if (distanceToPlayer > engagePlayerDistance)
        {
        }

        // When we get close enough to the player, stop (and then we will shoot / whatever)
        if (distanceToPlayer < engagePlayerDistance)
        {
            enemyScript.GetEnemyAnimator().SetBool("IsWalking", false);
            enemyNavAgent.isStopped = true;
            
            // Turn to face the player
            // Get the angle of the player relative to the forward vector of the enemy
            // If the angle is within a small bound, rotate from the animation (spine)
                //enemyRigSpine.transform.rotation = tempRotation;
            // If the angle is greater than the small bound, rotate the entire enemy (play feet shuffle animation)
                //this.transform.rotation = tempRotation;

            playerDirection = playerTransform.position - this.transform.position;
            Quaternion tempRotation = Quaternion.LookRotation(playerDirection);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, tempRotation, Time.deltaTime * turnSpeed);
        }
        
        //When the player gets too far away, move closer / chase
        if (distanceToPlayer >= repositionToEngagePlayerDistance)
        {
            enemyScript.GetEnemyAnimator().SetTrigger("WalkTrigger");
            enemyScript.GetEnemyAnimator().SetBool("IsWalking", true);
            enemyNavAgent.isStopped = false;
        }

    }

    private void MoveToLastKnownPlayerPosition()
    {
        distanceToLastKnownPost = Vector3.Distance(this.transform.position, lastKnownPlayerPostition);

        enemyNavAgent.destination = lastKnownPlayerPostition;
        if (distanceToLastKnownPost < 0.25f)
        {
            enemyScript.GetEnemyAnimator().SetBool("IsWalking", false);
        }
        
    }

    public void TargetPlayer(Transform target)
    {
        playerTransform = target;
        isChasingPlayer = true;
        lostSightOfPlayer = false;
    }

    public void LostSightOfPlayer()
    {
        lostSightOfPlayer = true;
        lastKnownPlayerPostition = playerTransform.position;
    }

    public void DisableNavMeshAgent()
    {
        enemyNavAgent.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying == true)
        {
            if (isChasingPlayer)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(enemyNavAgent.destination, 1.25f);   
            }
            else
            {
                //Gizmos.color = Color.green;
                Gizmos.DrawSphere(enemyNavAgent.destination, 1.25f);
            }

            for (int i = 0; i < enemyNavAgent.path.corners.Length; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(enemyNavAgent.path.corners[i], 1.25f);
            }
        }
    }
}
