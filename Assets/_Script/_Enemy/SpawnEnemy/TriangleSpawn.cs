using System.Collections.Generic;
using UnityEngine;

public class TriangleSpawnPattern : ISpawnPattern
{
    public List<List<Vector2>> GetSpawnPositionsByRows(Vector2 min, Vector2 max, int enemyCount)
    {
        List<List<Vector2>> rows = new List<List<Vector2>>();

        float startY = max.y + 1f; // Spawn phía trên màn hình
        float rowSpacing = 1.5f;   // Khoảng cách giữa các hàng
        float horizontalSpacing = 3f; // Khoảng cách giữa các enemy trong cùng hàng

        int spawned = 0;
        int enemiesInRow = 1; // Hàng đầu tiên có 1 enemy

        while (spawned < enemyCount)
        {
            List<Vector2> rowPositions = new List<Vector2>();

            // Tính tổng chiều rộng của hàng này
            float rowWidth = (enemiesInRow - 1) * horizontalSpacing;
            // Điểm bắt đầu X để căn giữa
            float startX = (min.x + max.x) / 2 - rowWidth / 2;

            for (int i = 0; i < enemiesInRow && spawned < enemyCount; i++)
            {
                float x = startX + i * horizontalSpacing;
                float y = startY - (rows.Count * rowSpacing);
                rowPositions.Add(new Vector2(x, y));
                spawned++;
            }

            rows.Add(rowPositions);
            enemiesInRow++;
        }

        return rows;
    }
}
