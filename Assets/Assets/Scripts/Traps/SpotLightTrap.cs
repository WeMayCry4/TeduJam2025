using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float activeDuration = 3f; // How long the trap stays active
    [SerializeField] private string interactionKey = "E"; // Key to trigger the trap
    [SerializeField] private string targetTag = "Boss"; // Tag of the object to damage.

    private bool _isTrapActive = false;
    private bool _canActivate = true; // Prevents activation during cooldown.
    private Collider _trapCollider; // Reference to the trap's collider.
    private float _activationEndTime; //Store when trap should deactivate.

    private void Start()
    {
        // Get the collider component.  Assumes it's on the same game object.
        _trapCollider = GetComponent<Collider>();

        if (_trapCollider == null)
        {
            Debug.LogError("SpotLightTrap: No collider found on this GameObject!");
            enabled = false; // Disable script if no collider is present.
            return;
        }

        //Initially, deactivate the trap.
        DeactivateTrap();
    }

    private void Update()
    {
        if (_isTrapActive)
        {
            //Check if active duration has passed
            if (Time.time > _activationEndTime)
            {
                DeactivateTrap();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Only checks when player presses interaction button and can activate the trap.
        if (other.CompareTag("Player") && Input.GetKeyDown(interactionKey) && _canActivate)
        {
            ActivateTrap();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //This is where the damage actually happens.  Only applies damage when trap is active.
        if (_isTrapActive && other.CompareTag(targetTag))
        {
            var bossHealth = other.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.EnemyTakeDamage(damage);
                Debug.Log("Damaged boss!");
            }
            else
            {
                Debug.LogWarning("SpotLightTrap: Object with tag 'Boss' has no BossHealth component.");
            }
        }
    }

    private void ActivateTrap()
    {
        Debug.Log("Trap Activated!");
        _isTrapActive = true;
        _canActivate = false;
        _trapCollider.enabled = true; //Enable collider so OnTriggerEnter will be called.
        _activationEndTime = Time.time + activeDuration;

        // Optionally, you could also change the material or particle effects here to indicate activation.
        StartCoroutine(ActivationCooldown());
    }

    private void DeactivateTrap()
    {
        Debug.Log("Trap Deactivated!");
        _isTrapActive = false;
        _trapCollider.enabled = false; //Disable collider to prevent damaging enemies
        // Optionally, change the material or particle effects back to indicate deactivation.
    }


    //Prevents activating multiple times in short period.
    private IEnumerator ActivationCooldown()
    {
        // Wait for the active duration to end PLUS any additional cooldown you want.
        yield return new WaitForSeconds(activeDuration);
        _canActivate = true;
    }
}