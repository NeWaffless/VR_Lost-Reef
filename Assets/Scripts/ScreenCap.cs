using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCap : MonoBehaviour
{
    public void Capture()
    {
        int i = Mathf.RoundToInt(Random.Range(0, 100f) * 10000);
        ScreenCapture.CaptureScreenshot(string.Format("Screenshots/{0}.png", i));

        Debug.Log(i);
    }
}
