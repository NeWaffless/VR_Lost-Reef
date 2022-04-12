using System.Collections;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // spawn locations
    [Header("Spawn locations")]
    [SerializeField] Transform menuSpawn;
    [SerializeField] Transform experienceSpawn;
    
    // experience state
    public bool isRunning { get; private set; }
    public enum Stages 
    {
        CLEAN = 35,
        OVERFISHING = 60,
        PLASTICS = 99,
        END = 100
    }
    Stages pollStage, prevPollStage;

    // reset
    [Header("")]
    [Rename("Wait time before reset")]
    [SerializeField] float timeUntilReset;

    // necessary components
    [Header("Component references")]
    [SerializeField] GameObject player;
    PollutionMeter polMeter;
    EnvironmentUpdater envUpdater;
    [SerializeField] GameObject musicAudio;
    [SerializeField] GameObject boatEvent;
    [SerializeField] GameObject menuPointer;
    
    // fish
    [SerializeField] Transform fishSet1;
    [SerializeField] Transform fishSet2;
    // garbage
    [SerializeField] Transform garbageSet1;
    [SerializeField] Transform garbageSet2;



    void InitialiseVariables()
    {
        isRunning = false;
        pollStage = Stages.CLEAN;
        prevPollStage = Stages.END;
    }

    void Awake()
    {
        polMeter = GetComponent<PollutionMeter>();
        envUpdater = GetComponent<EnvironmentUpdater>();
        InitialiseVariables();
    }

    void Update()
    {
        //if(!isRunning) return;
        CheckPollStage();
        if(pollStage != prevPollStage)
        {
            UpdateStage();
            prevPollStage = pollStage;
        }
    }
    
    void CheckPollStage()
    {
        float pLevel = polMeter.GetPolLevel();
        if(pLevel < (float) Stages.CLEAN)
        {
            pollStage = Stages.CLEAN;
        }
        else if(pLevel < (float) Stages.OVERFISHING)
        {
            pollStage = Stages.OVERFISHING;
        }
        else if(pLevel < (float) Stages.END)
        {
            pollStage = Stages.PLASTICS;
        }
        else
        {
            pollStage = Stages.END;
        }
    }

    void UpdateStage()
    {
        switch(pollStage)
        {
            case Stages.CLEAN:
                musicAudio.GetComponent<AudioManager>().ChangeTrack(0);
                fishSet1.gameObject.SetActive(true);
                fishSet2.gameObject.SetActive(true);
                garbageSet1.gameObject.SetActive(false);
                garbageSet2.gameObject.SetActive(false);
                break;
            case Stages.OVERFISHING:
                musicAudio.GetComponent<AudioManager>().ChangeTrack(1);
                fishSet1.gameObject.SetActive(false);
                garbageSet1.gameObject.SetActive(true);
                break;
            case Stages.PLASTICS:
                garbageSet2.gameObject.SetActive(true);
                break;
            case Stages.END:
                musicAudio.GetComponent<AudioManager>().ChangeTrack(2);
                boatEvent.GetComponent<BoatEvent>().SpawnBoat();
                StartCoroutine(ResetExperience());
                fishSet2.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void StartExperience()
    {
        player.transform.position = experienceSpawn.position;
        player.transform.rotation = experienceSpawn.rotation;

        polMeter.gameObject.SetActive(true);
        envUpdater.gameObject.SetActive(true);

        isRunning = true;
        menuPointer.SetActive(false);
    }

    IEnumerator ResetExperience()
    {
        yield return new WaitForSeconds(timeUntilReset);
        ReturnToMenu();
    }

    public void ReturnToMenu()
    {
        player.transform.position = menuSpawn.position;
        musicAudio.GetComponent<AudioManager>().ChangeTrack(0);
        boatEvent.GetComponent<BoatEvent>().ResetBoat();

        player.GetComponent<PlayerInput>().GetRightController().GetComponentInChildren<Animator>().enabled = false;
        
        polMeter.gameObject.SetActive(false);
        envUpdater.gameObject.SetActive(false);
        fishSet1.gameObject.SetActive(true);
        fishSet2.gameObject.SetActive(true);
        garbageSet1.gameObject.SetActive(false);
        garbageSet2.gameObject.SetActive(false);
        FindObjectOfType<PauseMenu>().Reset();
        menuPointer.SetActive(true);
        InitialiseVariables();
    }

    public float GetResetTime()
    {
        return timeUntilReset;
    }

}
