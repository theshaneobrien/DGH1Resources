using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemySO", menuName = "Scriptable Objects/New Enemy Scriptable Object", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public string enemyName;
    public string enemyType;
    public string enemyDescription;
    public Sprite enemyConversationSprite;
    public int walkSpeed;
    public int timeSpentIdle;
    public int maxHealth;
    public AudioClip enemyVoice;
    
}
