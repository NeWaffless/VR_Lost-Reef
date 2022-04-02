using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioPlayer;
    [SerializeField] AudioClip[] audioTracks;
    [SerializeField] float fadeTime;
    float vol;

    bool exitFade;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        vol = audioPlayer.volume;
    }

    public void ChangeTrack(int newTrack)
    {
        StartCoroutine(FadeTrack(newTrack));
    }

    IEnumerator FadeTrack(int newTrack)
    {
        float elapsedTime = 0;
        while(elapsedTime < fadeTime)
        {
            audioPlayer.volume = Mathf.Lerp(vol, 0, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            if(exitFade)
            {
                exitFade = false;
                break;
            }
            yield return null;
        }

        audioPlayer.Stop();
        if(newTrack < 0 || newTrack > audioTracks.Length)
        {
            newTrack = 0;
            Debug.LogError("audio track failed to play proper track");
        }
        audioPlayer.clip = audioTracks[newTrack];
        audioPlayer.Play();
        audioPlayer.volume = vol;
        
        yield return null;
    }

    public void ChangeVolume(float v)
    {
        if(v > 1f)
        {
            v = 1f;
        }
        else if(v < 0f)
        {
            v = 0f;
        }
        audioPlayer.volume = v;
        vol = v;
        exitFade = true;
    }
}
