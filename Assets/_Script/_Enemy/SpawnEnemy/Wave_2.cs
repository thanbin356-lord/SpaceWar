using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wave_2 : MonoBehaviour, IWave
{
    public GameObject[] enemyPrefabs;
    public int enemyCount = 10;
    public float spawnDelay = 1f;

    private ISpawnPattern spawnPattern;
    private int spawnedCount = 0; // số quái đã spawn

    public event Action<IWave> OnWaveFinished;

    public void ResetWave()
    {
        spawnedCount = 0;
        spawnPattern = new RandomSpawnPattern();
        var positions = spawnPattern.GetSpawnPositionsByRows(Vector2.zero, Vector2.zero, enemyCount);
        StartCoroutine(SpawnEnemies(positions));
    }

    IEnumerator SpawnEnemies(List<List<Vector2>> positions)
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

            Instantiate(chosenPrefab, pos, Quaternion.identity);

            Debug.Log($"[Wave_2] Spawned enemy {i + 1}/{enemyCount}");

            yield return new WaitForSeconds(spawnDelay);
        }

        // Debug.Log("[Wave_2] Finished spawning all enemies!");

        // // báo cha
        // OnWaveFinished?.Invoke(this);

        // // ✨ delay 1 frame rồi mới tắt để coroutine kịp kết thúc
        // yield return null;
        // gameObject.SetActive(false);
    }
}
