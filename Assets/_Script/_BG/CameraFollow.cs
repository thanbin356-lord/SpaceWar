using UnityEngine;

[DisallowMultipleComponent]
public class CameraFollow : MonoBehaviour
{
  [Header("Movement")]
    public float moveSpeed = 5f;      // tốc độ di chuyển
    public float borderSize = 30f;    // vùng rìa màn hình (px) để bắt đầu kéo camera

    [Header("Bounds (optional)")]
    public bool useBounds = false;
    public float minX = -10f;
    public float maxX = 10f;

    void Update()
    {
        float moveX = 0f;

        // Nếu chuột ở gần rìa trái
        if (Input.mousePosition.x <= borderSize)
        {
            moveX = -1f;
        }
        // Nếu chuột ở gần rìa phải
        else if (Input.mousePosition.x >= Screen.width - borderSize)
        {
            moveX = 1f;
        }

        if (moveX != 0f)
        {
            float newX = transform.position.x + moveX * moveSpeed * Time.deltaTime;

            if (useBounds)
                newX = Mathf.Clamp(newX, minX, maxX);

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}
