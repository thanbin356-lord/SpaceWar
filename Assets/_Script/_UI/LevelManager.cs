using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int levelIndex;

    private void Awake()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.StartsWith("Scene"))
        {
            string numberPart = sceneName.Substring(5);
            int.TryParse(numberPart, out levelIndex);
        }
    }

    public void WinLevel()
    {
        PlayerPrefs.SetInt("Level_" + levelIndex + "_Completed", 1);
        PlayerPrefs.Save();
        Debug.Log(">>> Saved completion: Level_" + levelIndex);
    }
}
