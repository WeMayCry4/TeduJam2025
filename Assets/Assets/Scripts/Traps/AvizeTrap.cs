using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvizeTrap : MonoBehaviour
{
    [SerializeField] private string activationTag = "Interactable";
    [SerializeField] private GameObject explosionEffect;

    private bool _isTrapActive = false;  //Only activates the trap one time.
    private Collider _trapCollider;

    private void Start()
    {
        _trapCollider = GetComponent<Collider>();

        if (_trapCollider == null)
        {
            Debug.LogError("OneShotCollisionTrap: No collider found on this GameObject!");
            enabled = false;
            return;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Check if trap has already been activated and the incoming collision has matching tag
        if (!_isTrapActive && other.CompareTag(activationTag))
        {
            ActivateTrap();
        }
    }

    private void ActivateTrap()
    {
        Debug.Log("One-Shot Trap Activated!");
        explosionEffect.gameObject.SetActive(true);
        explosionEffect.transform.SetParent(null);
        Destroy(gameObject);
        // _isTrapActive = true;

        // // Disable the trap's collider to prevent multiple activations or damage.
        // _trapCollider.enabled = false;

        // // Apply Damage to all objects with targetTag inside trigger area.
        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, _trapCollider.bounds.extents.magnitude);
        // foreach (Collider hitCollider in hitColliders)
        // {
        //     if (hitCollider.CompareTag(targetTag))
        //     {
        //         var bossHealth = hitCollider.GetComponent<BossHealth>();
        //         if (bossHealth != null)
        //         {
        //             bossHealth.EnemyTakeDamage(damage);
        //             Debug.Log("Damaged boss!");
        //         }
        //         else
        //         {
        //             Debug.LogWarning("OneShotCollisionTrap: Object with tag 'Boss' has no BossHealth component.");
        //         }
        //     }
        // }

        // // Optional: Instantiate an explosion effect.
        // if (explosionEffect != null)
        // {
        //     Instantiate(explosionEffect, transform.position, transform.rotation);
        // }

        // // Destroy the trap object.
    }
}
