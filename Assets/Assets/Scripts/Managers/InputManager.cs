using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    bool mouseScrollLock;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // MOVEMENT INPUTS
        playerMovement.SetInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")));
        PlayerInteraction interaction = Player.Instance.playerInteraction;

        // SPRINT
        if(Input.GetKeyDown(KeyCode.LeftShift)) playerMovement.SprintBegin();
        if(Input.GetKeyUp(KeyCode.LeftShift)) playerMovement.SprintEnd();

        // JUMP
        if(Input.GetButtonDown("Jump")) playerMovement.TryJump();

        // CAMERA
        if(Input.GetMouseButtonDown(0)) playerMovement.SetInitial();

        // LMB
        if(Input.GetButtonDown("Fire1"))
        {
            ObjectThrow.Instance.Throw();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(interaction.CheckIsThereAInteractable()) interaction.Interaction();
        }
    }
}
