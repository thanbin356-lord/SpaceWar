using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRandomSpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int enemyCount = 10;
    public float spawnDelay = 1f; 

    private ISpawnPattern spawnPattern;

    void Start()
    {
        spawnPattern = new RandomSpawnPattern();
        var positions = spawnPattern.GetSpawnPositionsByRows(Vector2.zero, Vector2.zero, enemyCount);
        StartCoroutine(SpawnEnemies(positions));
    }

    IEnumerator SpawnEnemies(List<List<Vector2>> positions)
    {
        foreach (var row in positions)
        {
            foreach (var pos in row)
            {
                int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
                GameObject chosenPrefab = enemyPrefabs[randomIndex];

                float angle = (UnityEngine.Random.value > 0.5f) ? 0f : 180f;
                GameObject enemy = Instantiate(chosenPrefab, pos, Quaternion.Euler(0,angle, UnityEngine.Random.Range(-90f, 0f)));

                float randomScale = UnityEngine.Random.Range(0.06f, 0.1f);
                enemy.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                EnemyControl ec = enemy.GetComponent<EnemyControl>();
                if (ec != null)
                {
                    ec.speed = UnityEngine.Random.Range(5f, 10f);
                }

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
