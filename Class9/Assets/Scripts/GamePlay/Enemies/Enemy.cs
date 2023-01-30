using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyDetails;

    private Rigidbody enemyRb;
    private EnemyMovement enemyMovementScript;

    private bool enemyIsDead = false;

    private void Start()
    {
        enemyRb = this.GetComponent<Rigidbody>();
        enemyMovementScript = this.GetComponent<EnemyMovement>();
        enemyIsDead = false;
    }
    
    public void Die()
    {
        GameStateManager.Instance.playerScore += 1;
        
        GameStateManager.Instance.AddToEnemiesAlive(-1);
        // Add in sound effect
        // Start a particle effect
        // Spawn in a loot chest

        if (enemyDetails.enemyType != "target")
        {
            TellEnemyStory();
        }

        // We are checking if the rigidbody is null
        if (enemyRb != null)
        {
            // If it is not, we are disabling the rotational constraints
            // This will cause our enemies to fall over when they die
            enemyRb.constraints = RigidbodyConstraints.None;
        }
        else
        {
            // If it is null, the don't have physics, so delete them
            Destroy(this.gameObject);
        }

        enemyIsDead = true;
    }

    private void TellEnemyStory()
    {
        
        Debug.Log(enemyDetails.enemyName + " was a " + enemyDetails.enemyType + " and "  + enemyDetails.enemyDescription);
    }

    public EnemyScriptableObject GetEnemyDetails()
    {
        return enemyDetails;
    }

    public EnemyMovement GetEnemyMovementComponent()
    {
        return enemyMovementScript;
    }

    public bool GetEnemyIsDead()
    {
        return enemyIsDead;
    }
}
