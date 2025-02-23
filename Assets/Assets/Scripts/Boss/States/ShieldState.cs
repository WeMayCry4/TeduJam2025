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
        ShieldManager.Instance.shieldPhase = true;
    }

    public void Update()
    {

    }

    public void Exit()
    {
        
    }
}
