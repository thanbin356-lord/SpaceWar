using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float BackgroundSpeed;
    public float BackgroundSpeed2;
    public Renderer BackgroundRenderer;
    void Update()
    {
        BackgroundRenderer.material.mainTextureOffset += new Vector2(BackgroundSpeed * Time.deltaTime, BackgroundSpeed2 * Time.deltaTime);
    }
}
