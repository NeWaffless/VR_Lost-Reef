using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuContent : MonoBehaviour
{
    [SerializeField] float distanceFromPlayer;
    [SerializeField] float yOffset;

    private void OnEnable()
    {
        Transform camTransform = Camera.main.transform;

        Vector3 posFromPlayer = Vector3.Normalize(new Vector3(camTransform.forward.x, 0, camTransform.forward.z)) * distanceFromPlayer;
        transform.position = new Vector3(camTransform.position.x + posFromPlayer.x,
                                         camTransform.position.y + yOffset,
                                         camTransform.position.z + posFromPlayer.z);

        transform.LookAt(new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z));
    }
}
