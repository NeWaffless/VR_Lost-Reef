using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInput : MonoBehaviour
{
    XRRig rig;
    public XRNode lInputSource, rInputSource;
    InputDevice lDevice, rDevice;

    // used to prevent interaction during button animation / level transition
    public bool canInteract { get; set; }
    public bool paused { get; set; }
    bool pauseHeld;

    ContinuousMovement playerMovement;

    [SerializeField] GameObject rightController;
    MenuPointer menuPointer;
    bool interactHeld;

    [Rename("Game State Manager")]
    GameState gameState;

    void Start()
    {
        rig = GetComponent<XRRig>();
        playerMovement = GetComponent<ContinuousMovement>();
        gameState = FindObjectOfType<GameState>();
        menuPointer = FindObjectOfType<MenuPointer>();

        canInteract = true;
    }

    void Update()
    {
        rDevice = InputDevices.GetDeviceAtXRNode(rInputSource);
        if(gameState.isRunning)
        {
            CheckPause();
            if(!paused)
            {
                PlayerMovement();
            }
            else
            {
                MenuInteract();
            }
        }
        else
        {
            MenuInteract();
        }
    }

    void CheckPause()
    {
        // lDevice = InputDevices.GetDeviceAtXRNode(lInputSource);
        // lDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool pausePressed);
        rDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool pausePressed);
        
        if(pausePressed && !pauseHeld)
        {
            if(paused)
            {
                FindObjectOfType<PauseMenu>().Reset();
            }
            else
            {
                FindObjectOfType<PauseMenu>().PauseUnpause(true);
            }
            pauseHeld = true;
        }
        else if(!pausePressed && pauseHeld)
        {
            pauseHeld = false;
        }
    }

    void PlayerMovement()
    {
        rDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool moving);
        rDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotateVal);
        if(moving)
        {
            rightController.GetComponentInChildren<Animator>().enabled = true;
            playerMovement.MovePlayer(1f, rotateVal);
        }
        else
        {
            rightController.GetComponentInChildren<Animator>().enabled = false;
        }
    }

    void MenuInteract()
    {
        rDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);
        if(canInteract && triggerPressed && menuPointer.currButton && menuPointer.currButton.isActiveAndEnabled && !interactHeld)
        {
            menuPointer.currButton.Interact();
            interactHeld = true;
        }
        else if(interactHeld && !triggerPressed)
        {
            interactHeld = false;
        }
    }

    public GameObject GetRightController()
    {
        return rightController;
    }
}
