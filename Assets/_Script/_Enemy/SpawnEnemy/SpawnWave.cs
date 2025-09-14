using UnityEngine;

public class SpawnWave : MonoBehaviour
{
    public GameObject[] spawnTurns;
    private int turn = 0;

    void Start()
    {
        ActivateTurn(turn);
    }

    void ActivateTurn(int index)
    {
        // Tắt hết turn trước đó
        foreach (var obj in spawnTurns)
            obj.SetActive(false);

        for (int i = index; i < index + 3 && i < spawnTurns.Length; i++)
        {
            spawnTurns[i].SetActive(true);
        }

        Debug.Log($"Turn {index + 1} → bật 3 turn");
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
            gameObject.SetActive(false);
        }
    }
}
