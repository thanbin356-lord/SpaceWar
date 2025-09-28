using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnWave : MonoBehaviour
{
    public GameObject[] waveObjects;
    public int concurrentWaves = 2;   // số wave chạy song song
    public float delayBetweenWaves = 3f; // thời gian chờ khi bật wave mới

    private Queue<IWave> waveQueue = new Queue<IWave>();
    private List<IWave> activeWaves = new List<IWave>();

    private void OnEnable()
    {
        waveQueue.Clear();
        activeWaves.Clear();

        foreach (var obj in waveObjects)
        {
            if (obj != null)
            {
                obj.SetActive(false); // tắt ban đầu
                IWave wave = obj.GetComponent<IWave>();
                if (wave != null)
                {
                    waveQueue.Enqueue(wave);
                }
            }
        }

        // bật N wave đầu tiên
        for (int i = 0; i < concurrentWaves; i++)
        {
            StartCoroutine(StartNextWave(delayBetweenWaves * i));
        }
    }

    private IEnumerator StartNextWave(float delay = 0f)
    {
        if (delay > 0f) yield return new WaitForSeconds(delay);

        if (waveQueue.Count > 0)
        {
            IWave wave = waveQueue.Dequeue();
            var obj = (wave as MonoBehaviour).gameObject;

            obj.SetActive(true);
            wave.ResetWave();
            wave.OnWaveFinished += HandleWaveFinished;

            activeWaves.Add(wave);

            Debug.Log($"[SpawnWave] Started wave: {obj.name}");
        }
    }

    private void HandleWaveFinished(IWave wave)
    {
        wave.OnWaveFinished -= HandleWaveFinished;
        activeWaves.Remove(wave);

        Debug.Log($"[SpawnWave] Wave finished: {(wave as MonoBehaviour).gameObject.name}");

        // thử start thêm 1 wave từ hàng chờ (có delay)
        if (waveQueue.Count > 0)
        {
            StartCoroutine(StartNextWave(delayBetweenWaves));
        }

        // nếu không còn wave nào cả → kết thúc
        if (waveQueue.Count == 0 && activeWaves.Count == 0)
        {
            Debug.Log("[SpawnWave] All waves finished, disabling self.");
            var grandpa = FindObjectOfType<TImeBetweenSpawn>();
            if (grandpa != null)
                grandpa.OnTurnFinished();

            gameObject.SetActive(false);
        }
    }
}
