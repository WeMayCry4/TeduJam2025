using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private string activationTag = "Interactable"; // Tag of the object that triggers the trap
    [SerializeField] private LineRenderer laserLine; // Assign this in the inspector, link the LineRenderer
    [SerializeField] private Transform laserOrigin; // The start point of the laser, assign in inspector
    [SerializeField] private float laserRange = 50f; //How long the laser goes
    [SerializeField] private LayerMask laserTargetMask; //LayerMask to find target to damage
    [SerializeField] private string playerTag = "Player"; // Tag of the player object

    private bool _isTrapActive = true; // Start as active
    private Collider _trapCollider;


    void Start()
    {
        _trapCollider = GetComponent<Collider>();

        if (_trapCollider == null)
        {
            Debug.LogError("RotatingLaserTrap: No collider found on this GameObject!");
            enabled = false;
            return;
        }

        if (laserLine == null)
        {
            Debug.LogError("RotatingLaserTrap: Laser LineRenderer not assigned!");
            enabled = false;
            return;
        }

        if (laserOrigin == null) {
            Debug.LogError("RotatingLaserTrap: Laser Origin Transform not assigned!");
            enabled = false;
            return;
        }
        laserLine.enabled = _isTrapActive; // Initially enabled if active.
    }

    void Update()
    {
        if (_isTrapActive)
        {
            ShootLaser(); // Call the method to shoot laser
        }
        laserLine.enabled = _isTrapActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(activationTag))
        {
            ToggleTrap();
        }
    }

    private void ToggleTrap()
    {
        Debug.Log("Laser Trap Toggled!");
        _isTrapActive = !_isTrapActive; // Toggle state.
    }


    private void ShootLaser()
    {
        RaycastHit hit;
        //Raycast from laserOrigin
        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit, laserRange, laserTargetMask))
        {
            //If we hit an object, draw the laser to that object and damage it
            laserLine.SetPosition(1, laserOrigin.InverseTransformPoint(hit.point)); //Convert world position to local
            //Apply damage to Player (or the object with playerTag)
            if (hit.collider.CompareTag(playerTag))
            {
                 // Get PlayerHealth component from the collided object
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();

                // Check if PlayerHealth component exists
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage * Time.deltaTime); // Call TakeDamage method from PlayerHealth component
                }
                else
                {
                    Debug.LogWarning("RotatingLaserTrap: Object with tag 'Player' has no PlayerHealth component.");
                }
            }
        }
        else
        {
            //If we dont hit anything, shoot the laser at laserRange distance
            laserLine.SetPosition(1, Vector3.forward * laserRange); //Convert world position to local
        }

        // Set the positions of the Line Renderer
        laserLine.SetPosition(0, Vector3.zero);
    }

    public bool GetTrapState()
    {
        return _isTrapActive;
    }

    public void SetTrapState(bool state)
    {
        _isTrapActive = state;
    }
}
