using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CutsceneController : MonoBehaviour
{
    public GameObject gameplayObjects;
    public float TimetoStart;

    void Start()
    {
        gameplayObjects.SetActive(false); // ẩn gameplay lúc đầu
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        // chạy cutscene ở đây (chờ 3 giây làm ví dụ)
        yield return new WaitForSeconds(TimetoStart);

        // hết cutscene -> bật gameplay
        gameplayObjects.SetActive(true);
    }
}
