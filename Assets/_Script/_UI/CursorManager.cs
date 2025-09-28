using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [Header("Cursor Settings")]
    public Texture2D cursorTexture;   // Kéo ảnh chuột vào đây
    public Vector2 hotspot = Vector2.zero; // Điểm click
    public CursorMode cursorMode = CursorMode.Auto;

    void Awake()
    {
        // Nếu chưa có instance → giữ lại
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetCursor(cursorTexture, hotspot);
        }
        else
        {
            // Nếu đã tồn tại instance khác → phá hủy cái mới
            Destroy(gameObject);
        }
    }

    public void SetCursor(Texture2D texture, Vector2 hotspot)
    {
        Cursor.SetCursor(texture, hotspot, cursorMode);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
