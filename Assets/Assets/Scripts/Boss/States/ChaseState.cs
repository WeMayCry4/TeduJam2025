using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyState
{
    private BossStateMachine boss;
    private NavMeshAgent agent;
    bool sfx;

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

        if(!sfx)
        {
            sfx = true;
            BossStateMachine.Instance.PlaySfx();
        }

    }

    public void Exit()
    {
        
    }
}