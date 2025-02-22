using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    private bool hasHitGround = false; // Prevents multiple damage instances

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasHitGround) 
        {
            hasHitGround = true; // Ensure it only takes damage once

            HealthSystem health = GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(20); // Apply 20 damage on ground impact
            }

            Destroy(this); // Remove this script after impact
        }
    }
}
