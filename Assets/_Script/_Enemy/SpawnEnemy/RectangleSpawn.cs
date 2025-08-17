using System.Collections.Generic;
using UnityEngine;

public class RectangleSpawnPattern : ISpawnPattern
{
    public List<List<Vector2>> GetSpawnPositionsByRows(Vector2 min, Vector2 max, int enemyCount)
    {
        List<List<Vector2>> rowsPositions = new List<List<Vector2>>();

        int cols = Mathf.CeilToInt(Mathf.Sqrt(enemyCount));
        int rows = Mathf.CeilToInt((float)enemyCount / cols);

        float fullXSpacing = (cols > 1) ? (max.x - min.x) / (cols - 1) : 0f;
        float fullYSpacing = (rows > 1) ? (max.y - min.y) / (rows - 1) : 0f;

        float shrinkFactor = 0.8f;

        float xSpacing = fullXSpacing * shrinkFactor;
        float ySpacing = fullYSpacing * shrinkFactor;

        float gridWidth = (cols - 1) * xSpacing;
        float offsetX = ((max.x - min.x) - gridWidth) / 2f;
        float spawnTopY = max.y -1f;

        int spawned = 0;
        for (int row = 0; row < rows; row++)
        {
            List<Vector2> rowPositions = new List<Vector2>();

            float yPos = spawnTopY ;

            for (int col = 0; col < cols; col++)
            {
                if (spawned >= enemyCount)
                    break;

                float xPos = min.x + offsetX + col * xSpacing;

                rowPositions.Add(new Vector2(xPos, yPos));
                spawned++;
            }

            rowsPositions.Add(rowPositions);
        }

        return rowsPositions;
    }
}
