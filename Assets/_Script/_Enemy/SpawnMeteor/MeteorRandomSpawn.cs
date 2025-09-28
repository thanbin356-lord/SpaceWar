using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRandomSpawn : MonoBehaviour, IWave
{
    public GameObject[] enemyPrefabs;
    public int enemyCount = 10;
    public float spawnDelay = 1f;

    private ISpawnPattern spawnPattern;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // IWave event
    public event Action<IWave> OnWaveFinished;

    public void ResetWave()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        spawnPattern = new RandomSpawnPattern();
        var positions = spawnPattern.GetSpawnPositionsByRows(Vector2.zero, Vector2.zero, enemyCount);
        spawnedEnemies.Clear();
        yield return StartCoroutine(SpawnEnemies(positions));

        while (spawnedEnemies.Exists(e => e != null))
        {
            yield return null;
        }
        Debug.Log("[MeteorRandomSpawn] Finished spawning all meteors!");
        OnWaveFinished?.Invoke(this);
    }

    IEnumerator SpawnEnemies(List<List<Vector2>> positions)
    {
        int spawned = 0;

        foreach (var row in positions)
        {
            foreach (var pos in row)
            {
                if (spawned >= enemyCount) yield break;

                int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
                GameObject chosenPrefab = enemyPrefabs[randomIndex];

                float angle = (UnityEngine.Random.value > 0.5f) ? 0f : 180f;
                GameObject enemy = Instantiate(chosenPrefab, pos, Quaternion.Euler(0, angle, UnityEngine.Random.Range(-90f, 0f)));


                float randomScale = UnityEngine.Random.Range(0.06f, 0.1f);
                enemy.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                EnemyControl ec = enemy.GetComponent<EnemyControl>();
                if (ec != null)
                {
                    ec.speed = UnityEngine.Random.Range(5f, 10f);
                }
                spawned++;
                spawnedEnemies.Add(enemy);
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
