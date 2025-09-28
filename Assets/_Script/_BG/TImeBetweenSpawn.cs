using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TImeBetweenSpawn : MonoBehaviour
{
    public GameObject[] spawnTurns;
    private int turn = 0;
    public GameObject gameplayObjects;
    public GameObject Player;

    void Start()
    {
        ActivateTurn(turn);
        gameplayObjects.SetActive(false);
    }

    void ActivateTurn(int index)
    {
        for (int i = 0; i < spawnTurns.Length; i++)
        {
            spawnTurns[i].SetActive(i == index);
        }
        Debug.Log($"Turn {index + 1} started");
    }
    private IEnumerator ActivateTurnWithDelay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivateTurn(index);
    }
    public void OnTurnFinished()
    {
        turn++;
        if (turn < spawnTurns.Length)
        {
            StartCoroutine(ActivateTurnWithDelay(turn, 5f));
        }
        else
        {
            Debug.Log("All turns finished!");
            int levelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
            PlayerPrefs.SetInt("Level_" + levelIndex + "_Completed", 1);
            Debug.Log(">>> Saved completion: Level_" + levelIndex);
            PlayerPrefs.Save();
            StartCoroutine(EnableTimeline(1f));
            StartCoroutine(LoadMainMenuAfterDelay(9f));
        }
    }

    private IEnumerator EnableTimeline(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameplayObjects.SetActive(true);
    }
    private IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(Player);
        SceneManager.LoadScene("LevelSelect");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
