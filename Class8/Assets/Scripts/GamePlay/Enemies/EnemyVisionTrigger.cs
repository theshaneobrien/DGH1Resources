using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionTrigger : MonoBehaviour
{
    [SerializeField] private EnemyVision enemyVision;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyVision.ChangePlayerInVisualRange(true);
            enemyVision.SetPlayerTransform(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyVision.ChangePlayerInVisualRange(false);
            enemyVision.SetPlayerTransform(null);
        }
    }
}
