using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTanker : MonoBehaviour
{
    
    Animation anim;

    AudioSource horn;
    [SerializeField] AudioSource metalCrash;
    [SerializeField] AudioSource rockCrash;

    [SerializeField] GameObject rockToCollide;
    Animation rockAnim;

    [SerializeField] GameObject oilSpill;
    Animation oilAnim;

    private void Awake()
    {
        horn = GetComponent<AudioSource>();

        anim = GetComponent<Animation>();
        rockAnim = rockToCollide.GetComponent<Animation>();
        oilAnim = oilSpill.GetComponent<Animation>();
    }

    private void OnEnable()
    {
        anim.Play();
        horn.Play();
    }

    private void Honk()
    {
        horn.Stop();
        horn.Play();
    }

    private void OnDisable()
    {
        anim.Stop();
        anim.Rewind();
        if(rockAnim != null)
        {
            rockAnim.Stop();
            rockAnim.Rewind();
        }

        horn.Stop();
        metalCrash.Stop();
        rockCrash.Stop();
        
        oilAnim.Stop();
        oilAnim.Rewind();
        oilSpill.SetActive(false);
    }
    
    private void InitialCrash()
    {
        metalCrash.Play();
        rockAnim.Play();
    }

    private void SecondaryCrash()
    {
        rockCrash.Play();
        oilSpill.SetActive(true);
        oilSpill.GetComponent<Animation>().Play();
        FindObjectOfType<EnvironmentUpdater>().SetOilSpillTrue();
    }
    
}
