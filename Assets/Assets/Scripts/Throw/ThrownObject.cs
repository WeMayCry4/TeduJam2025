using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    private bool hasHitSomething = false; // Prevents multiple damage instances
    public int damageAmount = 20;
    public float destroyDelay = 0.1f;  // Delay before destroying (for visual effect)

    private void OnCollisionEnter(Collision collision)
    {

        GameObject target = collision.gameObject;  // The object we collided with

        if (target.CompareTag("Boss")) // Check for the "Boss" tag.
        {
            hasHitSomething = true;
            DealDamageToBoss(target);
        }
        else if (target.GetComponent<HealthSystem>() != null && !target.CompareTag("Interactable"))
        {
            hasHitSomething = true;
            DealDamageToOther(target);
        }
    }

    //For non boss HealthSystem
    private void DealDamageToOther(GameObject target)
    {
        HealthSystem health = target.GetComponent<HealthSystem>();
        health.TakeDamage(damageAmount);
    }

    //Deals damage to boss and triggers TakeDamage state
    private void DealDamageToBoss(GameObject target)
    {
        BossStateMachine boss = target.GetComponent<BossStateMachine>();  // Assuming "BossController"
                                                                  // is your main boss script.
        if (boss != null)
        {
            boss.TakeDamage(damageAmount);  // Call a method on the BossController
        }
    }
}