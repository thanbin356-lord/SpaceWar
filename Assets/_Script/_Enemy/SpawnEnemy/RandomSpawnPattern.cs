using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPattern : ISpawnPattern
{
    private System.Random rng = new System.Random();

    public List<List<Vector2>> GetSpawnPositionsByRows(Vector2 min, Vector2 max, int enemyCount)
{
    List<List<Vector2>> result = new List<List<Vector2>>();
    List<Vector2> row = new List<Vector2>();

    // Lấy mép trên của camera (theo world position)
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    Vector2 topLeft  = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));

    float minX = topLeft.x;
    float maxX = topRight.x;
    float y    = topRight.y;

    for (int i = 0; i < enemyCount; i++)
    {
        float x = Mathf.Lerp(minX, maxX, (float)rng.NextDouble());
        row.Add(new Vector2(x, y));
    }
    result.Add(row);
    return result;
}

}
