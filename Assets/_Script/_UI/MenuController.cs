using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Gọi khi nhấn nút Start
    public void OnStartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    // Gọi khi nhấn nút Settings
    public void OnSettings()
    {
        Debug.Log("Open Settings Menu");
        // settingsPanel.SetActive(true);
    }
    public void OnQuitGame()
    {
        Debug.Log("Quit Game!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        Application.Quit();
#endif
    }
}
