using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShieldTrap : MonoBehaviour
{
    [SerializeField] GameObject explosionFX;    
    
    [SerializeField] float damage;
    [SerializeField] Collider collider;
    [SerializeField] float duration;
    [SerializeField] private string deactivationTag = "Interactable";
    private Collider _trapCollider;

    private bool _isTrapActive = true;

    bool completed;
    bool activated = true;

        void OnEnable()
    {
        completed = false;
        collider.enabled = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

                if (!_isTrapActive && col.CompareTag(deactivationTag))
        {
            DeactivateTrap();
        }
    }

    public void DeactivateTrap()
    {
        if(activated == false) return;
        activated = false;

        explosionFX.SetActive(false);
    }

    public void ActivateTrap()
    {
        if(activated) return;
        activated = true;

        explosionFX.SetActive(true);
    }
}