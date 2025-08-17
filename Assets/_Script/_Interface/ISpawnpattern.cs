using System.Collections.Generic;
using UnityEngine;

public interface ISpawnPattern
{
    List<List<Vector2>> GetSpawnPositionsByRows(Vector2 min, Vector2 max, int enemyCount);
}
