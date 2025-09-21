    using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour
{
    public float speed = 2f;
    public float stopY = -999f;
    public float stopDuration = 5f;

    private bool isStopped = false;
    private bool isWaitingCoroutineRunning = false;
    private bool hasStoppedOnce = false;  // thêm biến này

    void Update()
    {
        Vector2 position = transform.position;

        if (!isStopped)
        {
            // Nếu chưa dừng lần nào hoặc stopY chưa đạt
            if (stopY == -999f || hasStoppedOnce || position.y > stopY)
            {
                position.y -= speed * Time.deltaTime;
                transform.position = position;
            }
            else
            {
                if (!isWaitingCoroutineRunning)
                {
                    isStopped = true;
                    hasStoppedOnce = true;
                    StartCoroutine(ResumeFallAfterDelay());
                }
            }
        }

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ResumeFallAfterDelay()
    {
        isWaitingCoroutineRunning = true;
        yield return new WaitForSeconds(stopDuration);
        isStopped = false;
        isWaitingCoroutineRunning = false;
    }
}
