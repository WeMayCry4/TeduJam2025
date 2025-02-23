using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;
using System;

public class BossStateMachine : Singleton<BossStateMachine>
{
    public Transform Player; // The player to chase
    public Transform ShieldPosition; // Where the boss will go when shielding
    public GameObject ShieldObject; // The shield that appears
    public float Health = 100f;
    public GameObject ShieldSpots; // Parent object of the Shield Spots

    // Added: prefab for creating new shield spot objects
    public GameObject shieldSpotPrefab; 

    private IEnemyState currentState;
    public NavMeshAgent agent;

    public IEnemyState chaseState;
    public IEnemyState shieldState;

    private bool isShielded = false; // Track if the shield is active
    private float shieldHealthThreshold1 = 20f;

    private List<GameObject> shieldSpotList = new List<GameObject>();

    private bool isStunned = false;
    private float stunTimer = 0f;

    public void Stun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
        agent.isStopped = true;
        GetComponent<Animator>().SetTrigger("stun");
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Initialize states
        chaseState = new ChaseState(this, agent);
        shieldState = new ShieldState(this, agent);

        // Start in Chase State
        ChangeState(chaseState);

        // (Optional) Populate the list of shield spots from existing children under ShieldSpots
        foreach (Transform child in ShieldSpots.transform)
        {
            shieldSpotList.Add(child.gameObject);
        }
    }

    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                agent.isStopped = false;
            }
            return;
        }
        
        if (currentState != null)
        {
            currentState.Update();
        }

        CheckHealthAndShield();
    }

    public void LeaveShield()
    {
        isShielded = false;
        ChangeState(chaseState);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
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
        Stun(2);
    }

    public void ActivateShield()
    {
        ShieldObject.SetActive(true);
        agent.isStopped = true; // Stop movement
        isShielded = true;
        
        // Instantiate 4 shield spot objects relative to ShieldPosition.
        // Clear any existing shield spots so we start fresh.
        shieldSpotList.Clear();
        
        // Define four offset positions (you can adjust these offsets)
        Vector3[] offsets = new Vector3[] {
            new Vector3(1f, 0f, 0f),
            new Vector3(-1f, 0f, 0f),
            new Vector3(0f, 0f, 1f),
            new Vector3(0f, 0f, -1f)
        };

        // Instantiate each shield spot at the offset from the ShieldPosition
        foreach (Vector3 offset in offsets)
        {
            Vector3 spawnPos = ShieldPosition.position + offset;
            GameObject shieldSpot = Instantiate(shieldSpotPrefab, spawnPos, Quaternion.identity, ShieldSpots.transform);
            shieldSpotList.Add(shieldSpot);
        }
    }

    private void DeactivateShield()
    {
        ShieldObject.SetActive(false);
        agent.isStopped = false; // Resume movement
        isShielded = false;
    }

    private void CheckHealthAndShield()
    {
        if (!isShielded && (Health <= shieldHealthThreshold1))
        {
            isShielded = true;
            ChangeState(shieldState);
        }
        
        // Check if the shield is active and if all shield spots are destroyed
        bool allShieldsDown = shieldSpotList.All(spot => spot == null);
        if (isShielded && allShieldsDown)
        {
            DeactivateShield();
            ChangeState(chaseState);
        }
    }

    // Call this function when a shield spot is destroyed
    public void RemoveShieldSpot(GameObject spot)
    {
        shieldSpotList.Remove(spot);
    }
}
