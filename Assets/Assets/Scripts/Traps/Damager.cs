using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] Collider collider;
    [SerializeField] float duration;
    bool completed;



    void OnEnable()
    {
        completed = false;
        collider.enabled = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Boss"))
        {
            if(completed) return;
            completed = true;

            col.gameObject.GetComponent<BossStateMachine>().TakeDamage(damage);
            DOVirtual.DelayedCall(duration, ()=> collider.enabled = false);
        }
    }
}
