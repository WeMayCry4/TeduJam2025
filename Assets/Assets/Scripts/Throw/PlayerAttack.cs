using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackDamage = 20f; // Adjustable damage value
    public string BossTag = "Boss"; // Tag of the Boss GameObject

    // Example 1: Trigger-based attack (e.g., melee)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(BossTag))
        {
            // Get the BossStateMachine from the collided GameObject
            BossStateMachine boss = other.GetComponent<BossStateMachine>();

            // If a BossStateMachine is found, apply damage
            if (boss != null)
            {
                boss.TakeDamage(AttackDamage);
            }
        }
    }
}