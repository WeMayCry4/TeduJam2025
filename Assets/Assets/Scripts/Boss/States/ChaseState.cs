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
        
    }

    public void Update()
    {
        agent.SetDestination(boss.Player.position);
    }

    public void Exit()
    {
        
    }
}