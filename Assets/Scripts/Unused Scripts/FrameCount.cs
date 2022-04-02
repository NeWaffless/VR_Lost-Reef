using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameCount : MonoBehaviour
{
    
    float fps;
    [SerializeField] Text fcText;
    
    void Start()
    {
        StartCoroutine(UpdateFramecount());
    }

    IEnumerator UpdateFramecount()
    {
        while(true)
        {
            fps = Mathf.Floor(1.0f / Time.deltaTime);
            fcText.text = fps.ToString();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
