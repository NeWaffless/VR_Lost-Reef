using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLocationGizmo : MonoBehaviour
{
    void OnDrawGizmos() 
    {
        Gizmos.color = new Color(1, 0, 0, 1f);
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}