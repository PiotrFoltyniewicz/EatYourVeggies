using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> enemies;
    public float spawnRate;
    private float spawnTime;
    public int minimumEnemies = 3;
    private Transform player;
    public float scaleRate;
    private float scaleTime;
    public float healthScale;
    public float speedScale;

    void Start()
    {
        scaleTime = scaleRate;
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        scaleTime -= Time.deltaTime;
        if(scaleTime < 0){
            healthScale += 5;
            speedScale += 0.1f;
            scaleTime = scaleRate;
        }
        spawnTime -= Time.deltaTime;
        if(spawnTime < 0 || GameObject.FindGameObjectsWithTag("Enemy").Length < minimumEnemies)
        {
            spawnTime = spawnRate;
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
        GameObject choosenEnemy = enemies[Random.Range(0, enemies.Count)];
        GameObject temp = Instantiate(choosenEnemy, spawnPos, Quaternion.identity, null);
        temp.GetComponent<Enemy>().health += healthScale;
        temp.GetComponent<Enemy>().movementSpeed += speedScale;
    }
}
