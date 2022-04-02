using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterUpdate : MonoBehaviour
{
    [SerializeField] GameObject waterLowLOD;
    [SerializeField] GameObject waterHighLOD;
    [SerializeField] GameObject player;

    [Rename("Distance from water")]
    [SerializeField] float pDist;

    [Rename("Transition water")]
    [SerializeField] float tDist;

    void Update()
    {
        if(player.transform.position.y > waterLowLOD.transform.position.y - pDist)
        {
            waterLowLOD.SetActive(false);
            waterHighLOD.SetActive(true);
        }
        else if (player.transform.position.y > waterLowLOD.transform.position.y - pDist - tDist)
        {
            waterLowLOD.SetActive(true);
            waterHighLOD.SetActive(true);
        }
        else
        {
            waterLowLOD.SetActive(true);
            waterHighLOD.SetActive(false);
        }
    }


}
