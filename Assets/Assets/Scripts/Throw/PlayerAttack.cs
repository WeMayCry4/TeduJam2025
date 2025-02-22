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

    // Example 2: Raycast-based attack (e.g., shooting)
    public void PerformRaycastAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f)) // Adjust range as needed
        {
            if (hit.collider.gameObject.CompareTag(BossTag))
            {
                BossStateMachine boss = hit.collider.GetComponent<BossStateMachine>();

                if (boss != null)
                {
                    boss.TakeDamage(AttackDamage);
                }
            }
        }
    }

    //Example 3: Using Input and Button
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Replace "Fire1" with your desired input button
        {
            PerformRaycastAttack();  // Call the raycast attack function
        }
    }
}