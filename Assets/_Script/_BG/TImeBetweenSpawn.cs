using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TImeBetweenSpawn : MonoBehaviour
{
     public GameObject[] spawnTurns;

    private int turn = 0;

    void Start()
    {
        InvokeRepeating(nameof(TimeToSpawnEnemy), 5f, 45f);
    }

    void TimeToSpawnEnemy()
    {
        foreach (var obj in spawnTurns)
            obj.SetActive(false);

        spawnTurns[turn % spawnTurns.Length].SetActive(true);

        Debug.Log($"Turn {turn % spawnTurns.Length + 1}");

        turn++;
    }
}