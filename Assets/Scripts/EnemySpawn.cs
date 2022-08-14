using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> enemies;
    public float spawnRate;
    private float spawnTime;
    public int minimumEnemies = 3;
    public int currentEnemies;
    private Transform player;
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if(spawnTime < 0 || GameObject.FindGameObjectsWithTag("Enemy").Length < minimumEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float mapWidth = 8f;
        float mapHeight = 8f;
        Vector2 spawnPos;
        do
        {
            spawnPos = new Vector2(Random.Range(-mapWidth, mapWidth), Random.Range(-mapHeight, mapHeight));
        } while (Vector2.Distance(player.position, spawnPos) < 5f);
    }
}
