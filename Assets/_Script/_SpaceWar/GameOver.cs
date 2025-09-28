using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen; // Panel Game Over (kéo thả vào Inspector)
    [SerializeField] private float delayBeforeDisplay = 1f; // Trễ trước khi hiện panel
    [SerializeField] private GameObject objectToDestroy;


    public void ShowGameOverScreen()
    {
        StartCoroutine(DisplayGameOver());
    }

    private IEnumerator DisplayGameOver()
    {
        if (delayBeforeDisplay > 0)
            yield return new WaitForSecondsRealtime(delayBeforeDisplay);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;             
    }
    public void MainMenu()
    {
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (obj.hideFlags == HideFlags.None && obj.scene.IsValid())
            {
                Destroy(obj);
            }
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }
}
