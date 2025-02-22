using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ShieldState : MonoBehaviour
{
    public Transform player; // Oyuncu
    public Transform escapePoint; // Kaçış noktası
    public GameObject shield; // Kalkan objesi
    public float health = 100f;
    public float escapeThreshold = 30f; // Kaçmaya başlayacağı can seviyesi
    private NavMeshAgent agent;
    private bool isEscaping = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        shield.SetActive(false); // Başlangıçta kalkan kapalı olsun
    }

    void Update()
    {
        if (isEscaping)
        {
            agent.SetDestination(escapePoint.position);
            if (Vector3.Distance(transform.position, escapePoint.position) < 1f)
            {
                ActivateShield();
            }
        }
        else if (player != null && health > escapeThreshold)
        {
            agent.SetDestination(player.position);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= escapeThreshold && !isEscaping)
        {
            isEscaping = true;
        }
    }

    void ActivateShield()
    {
        shield.SetActive(true);
        agent.isStopped = true; // Düşman hareket etmeyi bıraksın
        // Burada düşmanın saldırı yapmasını da durdurabilirsin
    }
}

