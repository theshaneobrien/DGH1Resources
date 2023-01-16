using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyDetails;
    
    public void Die()
    {
        GameStateManager.Instance.playerScore += 1;
        
        GameStateManager.Instance.AddToEnemiesAlive(-1);
        // Add in sound effect
        // Start a particle effect
        // Spawn in a loot chest
        TellEnemyStory();
        
        Destroy(this.gameObject);
    }

    private void TellEnemyStory()
    {
        Debug.Log(enemyDetails.enemyName + " was a " + enemyDetails.enemyType + " and "  + enemyDetails.enemyDescription);
    }

    public EnemyScriptableObject GetEnemyDetails()
    {
        return enemyDetails;
    }
}
