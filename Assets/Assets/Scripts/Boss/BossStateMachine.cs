using UnityEngine;
using UnityEngine.AI;

public class BossStateMachine : MonoBehaviour
{
    public Transform Player; // The player to chase
    public Transform ShieldPosition; // Where the boss will go when shielding
    public GameObject ShieldObject; // The shield that appears
    public float Health = 100f;

    private IEnemyState currentState;
    private NavMeshAgent agent;

    public IEnemyState chaseState;
    public IEnemyState shieldState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Initialize states
        chaseState = new ChaseState(this, agent);
        shieldState = new ShieldState(this, agent);

        // Start in Chase State
        ChangeState(chaseState);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Boss Health: " + Health);
    }

    public void ActivateShield()
    {
        ShieldObject.SetActive(true);
        agent.isStopped = true; // Stop movement
        Debug.Log("Shield Activated!");
    }
}
