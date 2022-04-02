using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Button : MonoBehaviour
{
    public bool Colliding { get; set; }

    public abstract void Interact();
}
