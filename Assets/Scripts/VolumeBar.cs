using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeBar : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    [SerializeField] private GameObject muteIcon;
    private float storedVolume;
    [SerializeField] private Transform volumeBar;

    [SerializeField] private Material lightMat;
    [SerializeField] private Material darkMat;

    private void OnEnable()
    {
        storedVolume = source.volume;
        AdjustVolumeBarMaterials();
    }

    public void MuteUnmute()
    {
        float newInc = -1;
        if(source.volume == 0f)
        {
            newInc = Mathf.Ceil(storedVolume * 10f - 1);
        }
        else
        {
            storedVolume = source.volume;
        }

        // incremented in UpdateVolume()
        UpdateVolume(newInc);
    }

    public void UpdateVolume(float inc)
    {
        float newVol = (inc + 1) / 10f;

        if(source.gameObject.GetComponent<AudioManager>())
        {
            source.gameObject.GetComponent<AudioManager>().ChangeVolume(newVol);
        }
        else
        {
            source.volume = newVol;
        }

        AdjustVolumeBarMaterials();
    }
    
    public void AdjustVolumeBarMaterials()
    {
        float inc = Mathf.Ceil(source.volume * 10f);

        for(int i = 0; i < volumeBar.childCount; i++)
        {
            if(i < inc)
            {
                volumeBar.GetChild(i).GetComponent<Renderer>().material = lightMat;
            }
            else
            {
                volumeBar.GetChild(i).GetComponent<Renderer>().material = darkMat;
            }
        }

        if(source.volume <= 0f)
        {
            muteIcon.SetActive(true);
        }
        else
        {
            muteIcon.SetActive(false);
        }
    }
}
