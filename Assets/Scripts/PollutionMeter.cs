using UnityEngine;

public class PollutionMeter : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 playerPos, prevPlayerPos;

    [Rename("Pollution level")]
    [SerializeField] float polLevel;
    [Rename("Pollution max")]
    [SerializeField] float polMax;
    [Rename("Rate of decay")]
    [SerializeField] float rateOfDecay;

    void OnEnable()
    {
        polLevel = 0;
        playerPos = player.transform.position;
        prevPlayerPos = player.transform.position;
    }

    void Update()
    {
        Vector3 tempPos = playerPos;
        playerPos = player.transform.position;

        // update pollution level between 0 and pollution max based on player distance travelled
        float dist = Mathf.Abs( Vector3.Distance(playerPos, prevPlayerPos) );
        // prevents calculation after teleporting
        if(dist < polMax/2)
        {
            polLevel = Mathf.Clamp( polLevel + (dist * rateOfDecay), 0, polMax );
        }
        prevPlayerPos = tempPos;
    }


    // note, I recognise these could use properties
        // I wanted to initialise them in the inspector
    public float GetPolLevel()
    {
        return polLevel;
    }

    public float GetPolMax()
    {
        return polMax;
    }
}
