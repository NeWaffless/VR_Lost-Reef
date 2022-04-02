using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : Button
{
    public override void Interact()
    {
        Debug.Log("Quit application");
        Application.Quit();
    }
}
