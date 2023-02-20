using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Enemy enemyScript;

    private float currentHealth;
    
    private void Start()
    {
        enemyScript = this.GetComponent<Enemy>();
        currentHealth = enemyScript.GetEnemyDetails().maxHealth;
    }

    public void TakeDamage(float damageToTake)
    {
        if (enemyScript.GetEnemyIsDead() == false)
        {
            currentHealth -= damageToTake;

            if (currentHealth <= 0)
            {
                enemyScript.Die();
            }
            else
            {
                if (enemyScript.GetIsAwarePlayer() == false)
                {
                    enemyScript.MakeAwareOfPlayer();
                }
            }
        }
    }
}
