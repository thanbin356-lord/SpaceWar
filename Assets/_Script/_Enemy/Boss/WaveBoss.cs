using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBoss : MonoBehaviour, IWave
{
    [Header("Bosses có sẵn trong scene (drag vào Inspector)")]
    public GameObject[] bosses;

    // IWave event
    public event Action<IWave> OnWaveFinished;

    public void ResetWave()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        // Bật boss lên
        foreach (var boss in bosses)
        {
            if (boss != null)
                boss.SetActive(true);
        }

        // 🔑 Chờ đến khi tất cả boss bị Destroy
        while (Array.Exists(bosses, b => b != null))
        {
            yield return null;
        }

        Debug.Log("[WaveBoss] All bosses defeated!");
        OnWaveFinished?.Invoke(this);
    }
}
