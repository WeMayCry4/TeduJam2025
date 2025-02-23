using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTarget : MonoBehaviour
{
    bool breaked;

    private void OnTriggerEnter(Collider other)
    {
        //Check if trap has already been activated and the incoming collision has matching tag
        if (other.CompareTag("Interactable"))
        {
            Break();
        }
    }

    void Break()
    {
        if(breaked) return;
        breaked = true;

        ShieldManager.Instance.DecraseTarget();
        gameObject.SetActive(false);

    }
}
