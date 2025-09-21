using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TImeBetweenSpawn : MonoBehaviour
{
    public GameObject[] spawnTurns;
    private int turn = 0;

    void Start()
    {
        ActivateTurn(turn);
    }

    void ActivateTurn(int index)
    {
        for (int i = 0; i < spawnTurns.Length; i++)
            spawnTurns[i].SetActive(i == index);

        Debug.Log($"Turn {index + 1} started");
    }

    public void OnTurnFinished()
    {
        turn++;
        if (turn < spawnTurns.Length)
        {
            ActivateTurn(turn);
        }
        else
        {
            Debug.Log("All turns finished!");
        }
    }
}
