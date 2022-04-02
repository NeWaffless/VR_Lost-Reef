using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : Button
{
    public override void Interact()
    {
        transform.parent.GetComponent<VolumeBar>().MuteUnmute();
    }
}
