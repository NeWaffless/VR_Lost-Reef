using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeIncButton : Button
{
    public override void Interact()
    {
        GetComponentInParent<VolumeBar>().UpdateVolume(transform.GetSiblingIndex());
    }
}