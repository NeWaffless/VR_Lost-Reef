using UnityEngine;

public class BoatEvent : MonoBehaviour
{
    [SerializeField] GameObject boat;

    void Start()
    {
        boat.SetActive(false);
    }
    
    public void SpawnBoat()
    {
        boat.SetActive(true);
    }

    public void ResetBoat()
    {
        boat.SetActive(false);
    }
}
