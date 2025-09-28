using UnityEngine;

public class AnimationBG : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speedX = 1f;   // tốc độ lắc theo trục X
    public float speedY = 0.5f; // tốc độ lắc theo trục Y
    public float amplitudeX = 0.05f; // biên độ lắc X
    public float amplitudeY = 0.05f; // biên độ lắc Y
    public Renderer BackgroundRenderer;
    public SpriteRenderer ButtonRender;

    private Vector2 offset;

    void Update()
    {
        offset.x = Mathf.Sin(Time.time * speedX) * amplitudeX;
        offset.y = Mathf.Cos(Time.time * speedY) * amplitudeY;
        if (BackgroundRenderer != null)
        {
            BackgroundRenderer.material.mainTextureOffset = offset;
        }
    }
}
