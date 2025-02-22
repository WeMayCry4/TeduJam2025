using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : MonoBehaviour
{
     private Transform playerTransform;
    private NavMeshAgent nav;

    [SerializeField] private float attackCooldown = 2f;  // Time between attacks in seconds
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    [SerializeField] private int damage;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        boxCollider.isTrigger = true; // Ensure that the boxCollider is set as a trigger
    }

    void Update()
    {
        nav.destination = playerTransform.position;

        cooldownTimer += Time.deltaTime;
    }

    private bool PlayerInSight()
    {
        Vector3 boxCenter = boxCollider.bounds.center + transform.forward * range * colliderDistance;
        Vector3 boxSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);

        Collider[] hits = Physics.OverlapBox(boxCenter, boxSize / 2, transform.rotation, playerLayer);

        foreach (Collider hit in hits)
        {
            playerHealth = hit.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 boxCenter = boxCollider.bounds.center + transform.forward * range * colliderDistance;
        Vector3 boxSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }

    // This method will be called when the enemy collides with the player
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player is within the trigger area
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && cooldownTimer >= attackCooldown) // Ensure attack cooldown
            {
                DamagePlayer();
                cooldownTimer = 0f; // Reset the cooldown timer after dealing damage
            }
        }
    }

    // Method to deal damage to the player
    private void DamagePlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Player damaged by enemy!");
        }
    }
}
