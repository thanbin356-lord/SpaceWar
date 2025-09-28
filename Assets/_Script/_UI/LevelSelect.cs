using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Sprite normalSprite;
    public Sprite completedSprite;
    public Sprite lockedSprite;

    private bool unlocked = false;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        CheckUnlock();
    }

    void CheckUnlock()
    {
        unlocked = (levelIndex == 1) ||
                   PlayerPrefs.GetInt("Level_" + (levelIndex - 1) + "_Completed", 0) == 1;

        if (!unlocked)
        {
            spriteRenderer.sprite = lockedSprite;
            animator.enabled = false;
            return;
        }

        animator.enabled = true;

        bool firstTimeUnlock = PlayerPrefs.GetInt("Level_" + levelIndex + "_Unlocked", 0) == 0;
        if (firstTimeUnlock && !IsCompleted())
        {
            animator.SetTrigger("Unlock");
            spriteRenderer.sprite = normalSprite;

            PlayerPrefs.SetInt("Level_" + levelIndex + "_Unlocked", 1);
            PlayerPrefs.Save();
        }


        if (IsCompleted())
        {
            spriteRenderer.sprite = completedSprite;
            animator.ResetTrigger("Completed");
            animator.SetTrigger("Completed");
        }
    }

    void OnMouseEnter()
    {
        if (unlocked && !IsCompleted())
            animator.SetBool("Highlighted", true);
    }

    void OnMouseExit()
    {
        if (unlocked && !IsCompleted())
            animator.SetBool("Highlighted", false);
    }

    void OnMouseDown()
    {
        if (unlocked)
        {
            animator.SetTrigger("Pressed");
            audioManager.PlaySFX(audioManager.buttonClick);
            PlayerPrefs.SetInt("CurrentLevelIndex", levelIndex);
            PlayerPrefs.Save();
            Debug.Log(">>> CurrentLevelIndex set to " + levelIndex);
            SceneManager.LoadScene("Scene" + levelIndex);
        }
    }

    private bool IsCompleted()
    {
        return PlayerPrefs.GetInt("Level_" + levelIndex + "_Completed", 0) == 1;
    }
}
