using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    //[HideInInspector] public PlayerObjectHolder playerObjectHolder;
    [HideInInspector] public PlayerInteraction playerInteraction;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public InputManager playerInput;


    void Awake()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<InputManager>();
    }
}
