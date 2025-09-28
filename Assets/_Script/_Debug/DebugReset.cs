using UnityEngine;

public class DebugReset : MonoBehaviour
{
    public bool resetOnStart = true; // bật/tắt dễ dàng

    void Start()
    {
        if (resetOnStart)
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs reset at Start!");
        }
    }
}
