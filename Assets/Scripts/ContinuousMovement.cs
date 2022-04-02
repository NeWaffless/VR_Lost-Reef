using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousMovement : MonoBehaviour
{
    CharacterController character;
    [SerializeField] float speed;

    float previousInput = 0;
    float deadZone = 0.0001f;

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    public void MovePlayer(float inputAxis, Quaternion rotateVal)
    {
        Vector3 direction = Vector3.zero;
        if(inputAxis > deadZone && inputAxis >= previousInput)
        {
            direction = rotateVal * Vector3.forward;
        }
        character.Move(direction * Time.deltaTime * speed);

        previousInput = inputAxis;
    }    
}
