using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int levelIndex;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Sprite normalSprite;
    public Sprite completedSprite;
    public Sprite lockedSprite;

    private bool unlocked = false;

    void Start()
    {
        animator.ResetTrigger("Unlock");
        animator.ResetTrigger("Completed");
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

        // Unlock animation
        if (spriteRenderer.sprite == lockedSprite)
        {
            animator.SetTrigger("Unlock");
            spriteRenderer.sprite = normalSprite;
        }

        // Completed animation chỉ khi level thực sự hoàn thành
        if (IsCompleted())
        {
            spriteRenderer.sprite = completedSprite;
            animator.ResetTrigger("Completed"); // đảm bảo không còn trigger cũ
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
        if (unlocked && !IsCompleted())
        {
            animator.SetTrigger("Pressed");
            LoadLevel();
        }
    }

    public void WinLevel()
    {
        PlayerPrefs.SetInt("Level_" + levelIndex + "_Completed", 1);
        PlayerPrefs.Save();

        spriteRenderer.sprite = completedSprite;
        animator.SetTrigger("Completed");
    }

    private bool IsCompleted()
    {
        return PlayerPrefs.HasKey("Level_" + levelIndex + "_Completed") &&
               PlayerPrefs.GetInt("Level_" + levelIndex + "_Completed") == 1;
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("Scene" + levelIndex);
    }
}
