using System;
using System.Collections.Generic;
using UnityEngine;

// GameState Manager is just for keeping track of the state of our game
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private GamePlayUI gamePlayUI;

    public int playerScore = 0;
    private int enemiesAlive = 0;

    private bool playerIsReady = false;
    private bool playerWon = false;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gamePlayUI = this.GetComponent<GamePlayUI>();
    }

    public int GetEnemiesAlive()
    {
        return enemiesAlive;
    }

    public void AddToEnemiesAlive(int howManyEnemiesToAdd)
    {
        enemiesAlive = enemiesAlive + howManyEnemiesToAdd;

        CheckIfPlayerWon();
    }

    public void AddToEnemyList(Enemy enemyToAdd)
    {
        enemyList.Add(enemyToAdd);
    }

    public void SetPlayerIsReady(bool value)
    {
        playerIsReady = value;
    }

    public bool GetPlayerIsReady()
    {
        return playerIsReady;
    }

    public bool GetPlayerWon()
    {
        return playerWon;
    }

    private void CheckIfPlayerWon()
    {
        if (enemiesAlive == 0)
        {
            PlayerWon();
        }
    }

    private void PlayerWon()
    {
        playerWon = true;
        gamePlayUI.DisplayPlayerWonScreen();
        musicSource.Play();
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    public void TellAllEnemiesPlayerPos(Enemy enemyMakingTheCall)
    {
        foreach (Enemy enemyVar in enemyList)
        {
            if (enemyVar != enemyMakingTheCall)
            {
                if (enemyVar.GetIsAwarePlayer() == false)
                {
                    enemyVar.MakeAwareOfPlayer();
                }
            }
        }
    }
}
