using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    private bool hasHitSomething = false; // Prevents multiple damage instances

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHitSomething) return; // Prevent multiple damage events

        HealthSystem health = collision.gameObject.GetComponent<HealthSystem>();
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasHitSomething = true;
            DamageSelf(20);
        }
        else if (health != null && !collision.gameObject.CompareTag("Interactable"))
        {
            hasHitSomething = true;
            health.TakeDamage(20);
            DamageSelf(20);
        }
    }

    private void DamageSelf(int damage)
    {
        HealthSystem selfHealth = GetComponent<HealthSystem>();
        if (selfHealth != null)
        {
            selfHealth.TakeDamage(damage);
        }

        Destroy(this); // Remove the script after impact
    }
}
