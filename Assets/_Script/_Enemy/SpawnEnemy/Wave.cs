using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]

public class Wave : MonoBehaviour, IWave
{

    #region FIELDS
    [Tooltip("Enemy's prefab")]
    public GameObject enemy;

    [Tooltip("a number of enemies in the wave")]
    public int count;

    [Tooltip("path passage speed")]
    public float speed;

    [Tooltip("time between emerging of the enemies in the wave")]
    public float timeBetween;

    [Tooltip("points of the path. delete or add elements to the list if you want to change the number of the points")]
    public Transform[] pathPoints;

    [Tooltip("whether 'Enemy' rotates in path passage direction")]
    public bool rotationByPath;

    [Tooltip("if loop is activated, after completing the path 'Enemy' will return to the starting point")]
    public bool Loop;

    [Tooltip("color of the path in the Editor")]
    public Color pathColor = Color.yellow;

    [Tooltip("if testMode is marked the wave will be re-generated after 3 sec")]
    public bool testMode;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool finishedSpawning = false;
    public event Action<IWave> OnWaveFinished;

    #endregion

    private void OnEnable()
    {
        ResetWave();
        StartCoroutine(CreateEnemyWave());
    }

    private void Update()
    {
        spawnedEnemies.RemoveAll(e => e == null);

        if (spawnedEnemies.Count == 0 && finishedSpawning)
        {
            // báo cha (SpawnWave) biết là wave này xong
            OnWaveFinished?.Invoke((IWave)this);
        }
    }

    IEnumerator CreateEnemyWave()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newEnemy = Instantiate(enemy, enemy.transform.position, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);

            var follow = newEnemy.GetComponent<FollowThePath>();
            follow.path = pathPoints;
            follow.speed = speed;
            follow.rotationByPath = rotationByPath;
            follow.loop = Loop;
            follow.SetPath();

            newEnemy.SetActive(true);
            yield return new WaitForSeconds(timeBetween);
        }

        if (testMode)
        {
            yield return new WaitForSeconds(3);
            StartCoroutine(CreateEnemyWave());
        }
        finishedSpawning = true;
    }

    public void ResetWave()
    {
        StopAllCoroutines();
        spawnedEnemies.Clear();
        finishedSpawning = false;
    }

    void OnDrawGizmos()
    {
        DrawPath(pathPoints);
    }

    void DrawPath(Transform[] path) //drawing the path in the Editor
    {
        Vector3[] pathPositions = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            pathPositions[i] = path[i].position;
        }
        Vector3[] newPathPositions = CreatePoints(pathPositions);
        Vector3 previosPositions = Interpolate(newPathPositions, 0);
        Gizmos.color = pathColor;
        int SmoothAmount = path.Length * 20;
        for (int i = 1; i <= SmoothAmount; i++)
        {
            float t = (float)i / SmoothAmount;
            Vector3 currentPositions = Interpolate(newPathPositions, t);
            Gizmos.DrawLine(currentPositions, previosPositions);
            previosPositions = currentPositions;
        }
    }

    Vector3 Interpolate(Vector3[] path, float t)
    {
        int numSections = path.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * numSections), numSections - 1);
        float u = t * numSections - currPt;
        Vector3 a = path[currPt];
        Vector3 b = path[currPt + 1];
        Vector3 c = path[currPt + 2];
        Vector3 d = path[currPt + 3];
        return 0.5f * ((-a + 3f * b - 3f * c + d) * (u * u * u) + (2f * a - 5f * b + 4f * c - d) * (u * u) + (-a + c) * u + 2f * b);
    }

    Vector3[] CreatePoints(Vector3[] path)  //using interpolation method calculating the path along the path points
    {
        Vector3[] pathPositions;
        Vector3[] newPathPos;
        int dist = 2;
        pathPositions = path;
        newPathPos = new Vector3[pathPositions.Length + dist];
        Array.Copy(pathPositions, 0, newPathPos, 1, pathPositions.Length);
        newPathPos[0] = newPathPos[1] + (newPathPos[1] - newPathPos[2]);
        newPathPos[newPathPos.Length - 1] = newPathPos[newPathPos.Length - 2] + (newPathPos[newPathPos.Length - 2] - newPathPos[newPathPos.Length - 3]);
        if (newPathPos[1] == newPathPos[newPathPos.Length - 2])
        {
            Vector3[] LoopSpline = new Vector3[newPathPos.Length];
            Array.Copy(newPathPos, LoopSpline, newPathPos.Length);
            LoopSpline[0] = LoopSpline[LoopSpline.Length - 3];
            LoopSpline[LoopSpline.Length - 1] = LoopSpline[2];
            newPathPos = new Vector3[LoopSpline.Length];
            Array.Copy(LoopSpline, newPathPos, LoopSpline.Length);
        }
        return (newPathPos);
    }
}
