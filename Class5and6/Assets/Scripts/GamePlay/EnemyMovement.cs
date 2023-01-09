using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;

    [SerializeField] private int currentWaypointIndex;
    [SerializeField] private Vector3 currentWaypoint;

    [SerializeField] private float distanceToWaypoint;
    [SerializeField] private float acceptableDistanceToWaypoint = 3.0f;

    private Rigidbody enemyRb;
    [SerializeField] private float walkSpeed = 5.0f;

    private Transform playerTransform;
    private bool isChasingPlayer = false;
    
    // Start is called before the first frame update
    private void Start()
    {
        this.enemyRb = this.GetComponent<Rigidbody>();

        currentWaypointIndex = 0;
        currentWaypoint = waypoints[currentWaypointIndex].position;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckWaypoint();
        MoveToWaypoint();
    }

    private void CheckWaypoint()
    {
        // Only patrol if we are not chasing the player
        if (isChasingPlayer == false)
        {
            distanceToWaypoint = Vector3.Distance(this.transform.position, currentWaypoint);
            if (distanceToWaypoint <= acceptableDistanceToWaypoint)
            {
                currentWaypointIndex++;

                if (currentWaypointIndex == waypoints.Count)
                {
                    currentWaypointIndex = 0;
                }

                currentWaypoint = waypoints[currentWaypointIndex].position;
            }
        }
    }

    private void MoveToWaypoint()
    {
        Vector3 directionOfTarget = (currentWaypoint - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(directionOfTarget);
        
        enemyRb.velocity = transform.forward * walkSpeed;
    }

    public void TargetPlayer(Transform target)
    {
        playerTransform = target;
        currentWaypoint = playerTransform.position;
        isChasingPlayer = true;
    }
}
