using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : Button
{
    PlayerInput pInput;
    GameState gameState;
    Animator startAnim;

    void Awake()
    {
        pInput = FindObjectOfType<PlayerInput>();
        gameState = FindObjectOfType<GameState>();
        startAnim = GetComponent<Animator>();
    }
    
    public override void Interact()
    {
        StartCoroutine(ButtonPressed());
    }

    IEnumerator ButtonPressed()
    {
        pInput.canInteract = false;
        startAnim.Play("Playhook", -1, 0f);
        // waits for the length of the play button animation clip before continuing
            // this code works because there is only one clip in the play button animation
        yield return new WaitForSeconds(startAnim.runtimeAnimatorController.animationClips[0].length);
        gameState.StartExperience();

        startAnim.Play("Playidle", -1, 0f);
        pInput.canInteract = true;
    }
}
