using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundQuadMovement : MonoBehaviour
{
    float distanceFromPlayer;

    private void Awake()
    {
        distanceFromPlayer = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    private void Update()
    {
        Transform camTransform = Camera.main.transform;

        Vector3 newPos = Vector3.Normalize(new Vector3(camTransform.forward.x, 0, camTransform.forward.z)) * distanceFromPlayer;
        transform.position = new Vector3(newPos.x, transform.parent.position.y, newPos.z);

        Vector3 targetPos = new Vector3(transform.parent.position.x, transform.position.y, transform.parent.position.z);
        transform.LookAt(transform.position - (targetPos - transform.position));
    }
    
}
