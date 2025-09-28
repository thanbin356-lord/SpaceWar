using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_2 : MonoBehaviour, IWave
{
    public GameObject[] enemyPrefabs;
    public int enemyCount = 100;
    public float spawnDelay = 0.2f;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool finishedSpawning = false;

    public event Action<IWave> OnWaveFinished;

    public void ResetWave()
    {
        StopAllCoroutines();
        spawnedEnemies.Clear();
        finishedSpawning = false;

        var spawnPattern = new RandomSpawnPattern();
        var positions = spawnPattern.GetSpawnPositionsByRows(Vector2.zero, Vector2.zero, enemyCount);

        StartCoroutine(SpawnEnemies(positions));
    }

    private IEnumerator SpawnEnemies(List<List<Vector2>> positions)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            GameObject chosenPrefab = enemyPrefabs[randomIndex];

            Vector2 pos = Vector2.zero;
            if (positions.Count > 0)
            {
                var row = positions[UnityEngine.Random.Range(0, positions.Count)];
                pos = row[UnityEngine.Random.Range(0, row.Count)];
            }

            GameObject enemy = Instantiate(chosenPrefab, pos, Quaternion.identity);
            spawnedEnemies.Add(enemy);

            Debug.Log($"[Wave_2] Spawned enemy {i + 1}/{enemyCount} at {pos}");

            yield return new WaitForSeconds(spawnDelay);
        }

        finishedSpawning = true;
        Debug.Log("[Wave_2] Finished spawning, waiting for all enemies to die...");
    }

    private void Update()
    {
        spawnedEnemies.RemoveAll(e => e == null);

        if (spawnedEnemies.Count == 0 && finishedSpawning)
        {
            Debug.Log("[Wave_2] All enemies dead → Wave finished!");
            OnWaveFinished?.Invoke(this);
            enabled = false; // tránh gọi nhiều lần
        }
    }
}
