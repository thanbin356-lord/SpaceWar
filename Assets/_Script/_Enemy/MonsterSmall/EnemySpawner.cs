using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float timeBetweenWaves = 150f;
    public int enemiesPerWave = 5;
    public float fallSpeed = 2f;

    private ISpawnPattern spawnPattern;

    void Start()
    {

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        
        while (true)
        {
            SpawnRandomShape();
            var rowsPositions = spawnPattern.GetSpawnPositionsByRows(min, max, enemiesPerWave);

            float baseStopY = min.y + 8f;    // ví dụ vị trí dừng của hàng dưới cùng (thấp nhất)
            float rowSpacing = 2f;           // khoảng cách dừng giữa các hàng

            int rowIndex = 0;
            foreach (var rowPositions in rowsPositions)
            {
                float stopYForRow = baseStopY + rowIndex * rowSpacing; // hàng sau dừng cao hơn hàng trước 1 đơn vị

                foreach (Vector2 pos in rowPositions)
                {
                    GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

                    EnemyControl control = enemy.GetComponent<EnemyControl>();
                    if (control != null)
                    {
                        control.speed = fallSpeed;
                        control.stopY = stopYForRow;
                        control.stopDuration = 5f;  // gán vị trí dừng cho hàng
                    }
                }
                yield return new WaitForSeconds(1.5f);
                rowIndex++;
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
    void SpawnRandomShape()
    {

        int shapeType = Random.Range(0, 2); // 0: vuông, 1: tròn, 2: tam giác

        switch (shapeType)
        {

            case 0:
                Debug.Log("Vuong");
                spawnPattern = new RectangleSpawnPattern();
                break;
            case 1:
                Debug.Log("Tamgiac");
                spawnPattern = new TriangleSpawnPattern();
                break;
        }
    }
}
