using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyState
{
    private BossStateMachine boss;
    private NavMeshAgent agent;

    public ChaseState(BossStateMachine boss, NavMeshAgent agent)
    {
        this.boss = boss;
        this.agent = agent;
    }

    public void Enter()
    {
        Debug.Log("Entering Chase State");
        agent.isStopped = false; // Enable movement
    }

    public void Update()
    {
        if (boss.Health <= 70) // Switch to Shield State when health is low
        {
            boss.ChangeState(boss.shieldState);
            return;
        }

        if (boss.Player != null)
        {
            agent.SetDestination(boss.Player.position);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Chase State");
    }
}
