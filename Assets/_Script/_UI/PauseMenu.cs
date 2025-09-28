using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
     AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public static bool GameIsPause = false;
    public GameObject pauseMenuUI;
    public GameObject objectToDestroy;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
    audioManager.PlaySFX(audioManager.buttonClick);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }
    void Pause()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }
    public void Map()
    {
          GameIsPause = false;
    Time.timeScale = 1f;
        Time.timeScale = 1f;
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            
            if (obj.hideFlags == HideFlags.None && obj.scene.IsValid())
            {
                    if (obj.transform.root.CompareTag("Audio")) continue;
                Destroy(obj);
            }
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("LevelSelect");
    }
    public void LoadMenu()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
            GameIsPause = false;
        Time.timeScale = 1f;
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (obj.hideFlags == HideFlags.None && obj.scene.IsValid())
            {
                if (obj.transform.root.CompareTag("Audio")) continue;
                Destroy(obj);
            }
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }
}
