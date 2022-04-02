using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuButton : Button
{
    [SerializeField] GameObject currentMenu;
    [SerializeField] GameObject newMenu;

    public override void Interact()
    {
        currentMenu.SetActive(false);
        newMenu.SetActive(true);
    }
}