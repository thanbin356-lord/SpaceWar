using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float BackgroundSpeed;
    public Renderer BackgroundRenderer;
    void Update()
    {
        BackgroundRenderer.material.mainTextureOffset += new Vector2(0f, BackgroundSpeed * Time.deltaTime);
    }
}
