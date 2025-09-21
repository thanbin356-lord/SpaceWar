using UnityEngine;
using System.Collections.Generic;

public class SpawnWave : MonoBehaviour
{
    public GameObject[] waveObjects;
    private int finishedWaves = 0;
    private List<IWave> activeWaves = new List<IWave>();

    private void OnEnable()
    {
        finishedWaves = 0;
        activeWaves.Clear();

        foreach (var obj in waveObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);

                IWave wave = obj.GetComponent<IWave>();
                if (wave != null)
                {
                    wave.ResetWave();
                    wave.OnWaveFinished += HandleWaveFinished;
                    activeWaves.Add(wave);
                }
            }
        }
    }

    private void OnDisable()
    {
        foreach (var wave in activeWaves)
        {
            wave.OnWaveFinished -= HandleWaveFinished;
        }
        activeWaves.Clear();

        foreach (var obj in waveObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    private void HandleWaveFinished(IWave wave)
    {
        finishedWaves++;
        if (finishedWaves == activeWaves.Count)
        {
            var grandpa = FindObjectOfType<TImeBetweenSpawn>();
            if (grandpa != null)
                grandpa.OnTurnFinished();

            gameObject.SetActive(false);
        }
    }
}
