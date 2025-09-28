using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("----------- Audio Source ---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
      [Header("Common SFX")]
    public AudioClip buttonClick;
    // public AudioClip background;
    public AudioClip death;
    public AudioClip tourch;
    public AudioClip shooting;
    public AudioClip gethit;
    public AudioClip bossgethit;
    public AudioClip bossshooting;
    public AudioClip bossdead;
    [Header("----------- Background Clips ---------")]
    public AudioClip MainMenu;
    public AudioClip Scene1;
    public AudioClip Scene2;
    public AudioClip Scene3;
    public AudioClip LevelSelect;

 private Dictionary<string, AudioClip> bgDict;
    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


        // map scene -> nhạc
        bgDict = new Dictionary<string, AudioClip>()
        {
             { "MainMenu", MainMenu },
            { "LevelSelect", LevelSelect },
            { "Scene1", Scene1 },
            { "Scene2", Scene2 },
            { "Scene3", Scene3 },
            // ...
        };
    }
     private void Start()
    {
        // Chạy nhạc cho scene đầu tiên
        PlayBackground(SceneManager.GetActiveScene().name);

        // Đăng ký sự kiện đổi scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackground(scene.name);
    }

    public void PlayBackground(string sceneName)
    {
        if (bgDict.TryGetValue(sceneName, out AudioClip clip))
        {
            if (musicSource.clip != clip)
            {
                musicSource.clip = clip;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
