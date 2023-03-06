using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyDetails;

    private Rigidbody enemyRb;
    private EnemyMovement enemyMovementScript;

    private Animator enemyAnim;
    
    private bool isAwareOfPlayer = false;
    private bool enemyIsDead = false;

    private void Start()
    {
        enemyRb = this.GetComponent<Rigidbody>();
        enemyMovementScript = this.GetComponent<EnemyMovement>();
        enemyAnim = this.GetComponentInChildren<Animator>();
        
        enemyIsDead = false;
        OnEnemySpawn();
    }

    private void OnEnemySpawn()
    {
        GameStateManager.Instance.AddToEnemiesAlive(1);
        GameStateManager.Instance.AddToEnemyList(this);
    }
    
    public void Die()
    {
        GameStateManager.Instance.playerScore += 1;
        
        GameStateManager.Instance.AddToEnemiesAlive(-1);
        // Add in sound effect
        // Start a particle effect
        // Spawn in a loot chest

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
            //Destroy(this.gameObject);
            enemyMovementScript.DisableNavMeshAgent();
            this.AddComponent<Rigidbody>();
            
        }

        enemyIsDead = true;
    }

    public EnemyScriptableObject GetEnemyDetails()
    {
        return enemyDetails;
    }

    public EnemyMovement GetEnemyMovementComponent()
    {
        return enemyMovementScript;
    }

    public bool GetIsAwarePlayer()
    {
        return isAwareOfPlayer;
    }

    public bool GetEnemyIsDead()
    {
        return enemyIsDead;
    }

    public void MakeAwareOfPlayer()
    {
        if (enemyIsDead == false)
        {
            isAwareOfPlayer = true;
            enemyMovementScript.TargetPlayer(GameStateManager.Instance.GetPlayerTransform());
            GameStateManager.Instance.TellAllEnemiesPlayerPos(this);
        }
    }

    public Animator GetEnemyAnimator()
    {
        return enemyAnim;
    }
}
