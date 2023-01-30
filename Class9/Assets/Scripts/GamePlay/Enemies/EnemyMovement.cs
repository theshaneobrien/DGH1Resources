using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;

    [SerializeField] private int currentWaypointIndex;
    [SerializeField] private Vector3 currentWaypoint;

    [SerializeField] private float distanceToWaypoint;
    [SerializeField] private float acceptableDistanceToWaypoint = 3.0f;

    [SerializeField] private bool enemyAtWaypoint = false;

    private Rigidbody enemyRb;
    private float walkSpeed = 5.0f;
    [SerializeField] private float turnSpeed = 1.0f;

    private Transform playerTransform;
    private bool isChasingPlayer = false;

    private Enemy enemyScript;

    [SerializeField] private float idleAtWaypointTime;
    [SerializeField] private float timeSpentIdling = 0;
    
    // Start is called before the first frame update
    private void Start()
    {
        this.enemyRb = this.GetComponent<Rigidbody>();
        this.enemyScript = this.GetComponent<Enemy>();

        currentWaypointIndex = 0;
        currentWaypoint = waypoints[currentWaypointIndex].position;

        walkSpeed = enemyScript.GetEnemyDetails().walkSpeed;
        idleAtWaypointTime = enemyScript.GetEnemyDetails().timeSpentIdle;
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
        if (isChasingPlayer == false)
        {
            distanceToWaypoint = Vector3.Distance(this.transform.position, currentWaypoint);
            if (distanceToWaypoint <= acceptableDistanceToWaypoint)
            {
                //We've hit our waypoint. start a timer
                enemyAtWaypoint = true;
                timeSpentIdling = timeSpentIdling + Time.deltaTime;
                if (timeSpentIdling > idleAtWaypointTime)
                {
                    Debug.Log("Idling");
                    //When our Timer hits zero, change the waypoint
                    currentWaypointIndex++;

                    if (currentWaypointIndex == waypoints.Count)
                    {
                        currentWaypointIndex = 0;
                    }

                    currentWaypoint = waypoints[currentWaypointIndex].position;
                    timeSpentIdling = 0;
                    enemyAtWaypoint = false;
                }
            }
        }
    }

    private void MoveToWaypoint()
    {
        Debug.Log("Is at waypoint");
        if (enemyAtWaypoint == false)
        {
            Debug.Log("Not at waypoint");
            Vector3 directionOfTarget = (currentWaypoint - this.transform.position).normalized;

            Quaternion calculatedLookRotation = Quaternion.LookRotation(directionOfTarget);

            Quaternion lerpRotation = Quaternion.Lerp(this.transform.rotation, calculatedLookRotation,
                Time.deltaTime * turnSpeed);

            Vector3 filteredLookRotation = new Vector3(0.0f, lerpRotation.eulerAngles.y, 0.0f);

            this.transform.rotation = Quaternion.Euler(filteredLookRotation);

            enemyRb.velocity = transform.forward * walkSpeed;
        }
    }

    public void TargetPlayer(Transform target)
    {
        playerTransform = target;
        currentWaypoint = playerTransform.position;
        isChasingPlayer = true;
    }
}
