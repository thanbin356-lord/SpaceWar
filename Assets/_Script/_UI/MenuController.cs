using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
     AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Gọi khi nhấn nú
    // t Start
    public void OnStartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    audioManager.PlaySFX(audioManager.buttonClick);
    }

    // Gọi khi nhấn nút Settings
    public void OnSettings()
    {
        Debug.Log("Open Settings Menu");
        audioManager.PlaySFX(audioManager.buttonClick);
        // settingsPanel.SetActive(true);
    }
    public void OnQuitGame()
    {
        Debug.Log("Quit Game!");
        audioManager.PlaySFX(audioManager.buttonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        Application.Quit();
#endif
    }
}
