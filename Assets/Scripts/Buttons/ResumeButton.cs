using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : Button
{
    PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = GetComponentInParent<PauseMenu>();
    }

    public override void Interact()
    {
        pauseMenu.PauseUnpause(false);
    }
}

