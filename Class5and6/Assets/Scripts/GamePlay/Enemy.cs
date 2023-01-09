using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public void Die()
    {
        GameStateManager.Instance.playerScore += 1;
        
        GameStateManager.Instance.AddToEnemiesAlive(-1);
        // Add in sound effect
        // Start a particle effect
        // Spawn in a loot chest
        Destroy(this.gameObject);
    }
}
