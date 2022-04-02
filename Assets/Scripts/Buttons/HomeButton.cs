using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButton : Button
{
    public override void Interact()
    {
        FindObjectOfType<GameState>().ReturnToMenu();
    }
}