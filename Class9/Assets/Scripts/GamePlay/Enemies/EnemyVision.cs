using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float visionRadius = 2;
    [SerializeField] private float visionDistance = 15;

    private EnemyMovement enemyMovement;

    private bool playerInVisualRange = false;

    private Transform playerTransform;

    // Start is called before the first frame update
    private void Start()
    {
        this.enemyMovement = this.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerInVisualRange == true)
        {
            VisionCheck();
        }
    }
    
    public void ChangePlayerInVisualRange(bool isInRange)
    {
        playerInVisualRange = isInRange;
    }

    public void SetPlayerTransform(Transform target)
    {
        playerTransform = target;
    }

    //I got this from user X on this stackoverflow page: https://stackoverflow.com/questions/32897250/movement-script-in-unity-c-sharp
    private void VisionCheck()
    {
        Vector3 targetDirection = playerTransform.position - this.transform.position;
        
        RaycastHit objectOurRayHit;
        Debug.DrawRay(this.transform.position, targetDirection * 1000, Color.red);
        if (Physics.Raycast(this.transform.position, targetDirection * 1000, out objectOurRayHit))
        {
            if (objectOurRayHit.collider)
            {
                // We need to set the player gameobjects tag to "Player" in the Unity Inspector
                if (objectOurRayHit.collider.CompareTag("Player"))
                {
                    enemyMovement.TargetPlayer(objectOurRayHit.transform);
                }
            }
        }
    }
}
