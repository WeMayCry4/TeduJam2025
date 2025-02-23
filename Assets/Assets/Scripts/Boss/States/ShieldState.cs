using UnityEngine;
using UnityEngine.AI;

public class ShieldState : IEnemyState
{
    private BossStateMachine boss;
    private NavMeshAgent agent;

    public ShieldState(BossStateMachine boss, NavMeshAgent agent)
    {
        this.boss = boss;
        this.agent = agent;
    }

    public void Enter()
    {
        agent.SetDestination(boss.ShieldPosition.position);
        ShieldManager.Instance.ActivateShield();
    }

    public void Update()
    {
        float distance = Vector3.Distance(boss.transform.position, boss.ShieldPosition.position);
        if (distance < 1f) // Once boss reaches the shield zone
        {
            boss.ActivateShield();
        }
    }

    public void Exit()
    {
        
    }
}
