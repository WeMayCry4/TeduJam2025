using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class BossStateMachine : Singleton<BossStateMachine>
{
    public Transform Player; // The player to chase
    public Transform ShieldPosition; // Where the boss will go when shielding
    public GameObject ShieldObject; // The shield that appears
    public float Health = 100f;
    public GameObject ShieldSpots; // Parent object of the Shield Spots

    private IEnemyState currentState;
    public NavMeshAgent agent;

    public IEnemyState chaseState;
    public IEnemyState shieldState;

    private bool isShielded = false; // Track if the shield is active
    private float shieldHealthThreshold1 = 70f;
    private float shieldHealthThreshold2 = 30f;
    private List<GameObject> shieldSpotList = new List<GameObject>();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Initialize states
        chaseState = new ChaseState(this, agent);
        shieldState = new ShieldState(this, agent);

        // Start in Chase State
        ChangeState(chaseState);

        // Populate the list of shield spots
        foreach (Transform child in ShieldSpots.transform)
        {
            shieldSpotList.Add(child.gameObject);
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }

        CheckHealthAndShield();
    }

    public void LeaveShield()
    {
        ChangeState(chaseState);
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
        isShielded = true;
        //Debug.Log("Shield Activated!");
    }

    private void DeactivateShield()
    {
        ShieldObject.SetActive(false);
        agent.isStopped = false; // Resume movement
        isShielded = false;
        //Debug.Log("Shield Deactivated!");
    }

    private void CheckHealthAndShield()
    {
        if (!isShielded && (Health < shieldHealthThreshold1 || Health < shieldHealthThreshold2))
        {
            ChangeState(shieldState);
        }
        

        //Check is Shield Active
        if (isShielded)
        {
            //Check if all shield spots are destoryed, linq makes it easy
            bool allShieldsDown = shieldSpotList.All(spot => spot == null);

            //ShieldsDown, then change state to Chase, and deactivate Shield
            if (allShieldsDown)
            {
                DeactivateShield();
                ChangeState(chaseState);
            }
        }
    }

    //Call this function when spot is destroyed
    public void RemoveShieldSpot(GameObject spot)
    {
        shieldSpotList.Remove(spot);
    }
}