using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpotLightTrap : MonoBehaviour
{
    [SerializeField] private float activeDuration = 3f; // How long the trap stays active
    [SerializeField] GameObject explosionFX;    
    bool activated;


    public void ActivateTrap()
    {
        if(activated) return;
        activated = true;

        explosionFX.SetActive(true);

        DOVirtual.DelayedCall(activeDuration, ()=> 
        {
            activated = false;
            explosionFX.SetActive(false);
        });
    }
}