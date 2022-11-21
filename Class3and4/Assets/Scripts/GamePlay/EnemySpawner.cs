using System.Collections.Generic;
using UnityEngine;

// You can see a fully commented version of this file at the following location: https://gist.github.com/theshaneobrien/3a240d854d773e9975c01c57ee138f2c
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;

    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private int numberOfEnemiesToSpawn = 5;

    [SerializeField] private float heightAboveToSpawn = 5;

    [SerializeField] private Vector3 rotationToSpawnAt = new Vector3 (0,0,90);
    
    private void Start()
    {
        SpawnRandomAmountOfEnemies();
    }
    
    private void SpawnEnemy()
    {
        Instantiate(enemyToSpawn, spawnPoints[0].position, Quaternion.identity);
    }
    
    private void SpawnEnemies()
    {
        for (int index = 0; index < spawnPoints.Count; index++)
        {
            Instantiate(enemyToSpawn, spawnPoints[index].position, Quaternion.identity);
        }
    }
    
    private void SpawnRandomAmountOfEnemies()
    {
        int numberOfSpawnPoints = spawnPoints.Count - 1;

        for(int index = 0; index < numberOfEnemiesToSpawn; index++)
        {
            Debug.Log("There are currently " + numberOfSpawnPoints + " spawnPoints available");
            
            int randomSpawnPoint = Random.Range(0, numberOfSpawnPoints);

            //We are taking the randomly selected spawn position
            //We are adding 5 meters to the Y axis

            heightAboveToSpawn = Random.Range(2.0f, 10.0f);

            Vector3 spawnPosition = spawnPoints[randomSpawnPoint].position + new Vector3(0f, heightAboveToSpawn, 0f);

            Instantiate(enemyToSpawn, spawnPosition, Quaternion.Euler(rotationToSpawnAt));
            
            spawnPoints.RemoveAt(randomSpawnPoint);
            
            numberOfSpawnPoints = numberOfSpawnPoints - 1;

            Debug.Log("There are currently " + numberOfSpawnPoints + " spawnPoints available");
        }
    }
}