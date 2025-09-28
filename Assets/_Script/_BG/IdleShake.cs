using UnityEngine;

public class IdleShake : MonoBehaviour
{
    public float speedX = 1f;
    public float speedY = 0.5f;
    public float amountX = 0.05f;
    public float amountY = 0.05f;

    private Vector3 basePos;

    void Start()
    {
        basePos = transform.localPosition; // giữ vị trí gốc từ clip idle
    }

    void LateUpdate()
    {
        // Chồng lắc lư lên position gốc của clip idle
        transform.localPosition = basePos + new Vector3(
            Mathf.Sin(Time.time * speedX) * amountX,
            Mathf.Sin(Time.time * speedY) * amountY,
            0
        );
    }
}
