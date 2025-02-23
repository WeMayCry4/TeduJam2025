using UnityEngine;
using DG.Tweening;

public class HeldState : BaseState
{
    private Rigidbody rb;

    public override void Enter()
    {
        // Stop movement by halting the NavMeshAgent and clearing its path
        enemy.Agent.isStopped = true;
        enemy.Agent.ResetPath();

        // Set Rigidbody to kinematic so physics won’t interfere
        rb = enemy.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public override void Perform()
    {
        // Enemy remains stationary while held.
    }

    public override void Exit()
    {
        // Allow movement again by reactivating the NavMeshAgent
        enemy.Agent.isStopped = false;

        // Restore physics by disabling the kinematic setting
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
