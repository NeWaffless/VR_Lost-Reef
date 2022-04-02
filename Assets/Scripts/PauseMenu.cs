using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour 
{
    MenuPointer menuPointer;
    GameObject menuItems;

    private void Start()
    {
        menuPointer = FindObjectOfType<MenuPointer>();
        menuItems = transform.GetChild(0).gameObject;
    }
    
    public void PauseUnpause(bool pause) 
    {
        FindObjectOfType<PlayerInput>().paused = pause;
        menuPointer.gameObject.SetActive(pause);
        menuItems.SetActive(pause);
    }

    public void Reset()
    {
        PauseUnpause(false);
        menuItems.transform.GetChild(0).gameObject.SetActive(true);
        menuItems.transform.GetChild(1).gameObject.SetActive(false);
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = new Color(1, 1, 0, 1f);
        Gizmos.DrawWireCube(transform.position, new Vector3(3.5f, 1, 1));
    }
}
