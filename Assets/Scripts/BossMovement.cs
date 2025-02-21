using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Transform playerTransform;

    void chaseState()
    {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void stayState()
    {
        
    }


}
