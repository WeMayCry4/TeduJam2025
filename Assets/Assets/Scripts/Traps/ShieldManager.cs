using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : Singleton<ShieldManager>
{
    int targetCount;
    [SerializeField] GameObject shieldParent;
    [SerializeField] GameObject shieldLights;
    bool activated;
    public bool shieldPhase;


    void Update()
    {
        if(!shieldPhase) return;

        float distance = Vector3.Distance(BossStateMachine.Instance.transform.position, transform.position);

        print(distance);

        if (distance <= 1f)
        {
            ActivateShield();
            LastBoss.Instance.Activate(BossStateMachine.Instance.transform);
            BossStateMachine.Instance.Disable();
        }
    }


    public void ActivateShield()
    {
        if(activated) return;
        activated = true;

        shieldParent.SetActive(true);
        shieldLights.SetActive(true);
        targetCount = 4;
        
    }

    public void DecraseTarget()
    {
        targetCount --;

        print(targetCount);

        if(targetCount <= 0)
        {
            shieldParent.SetActive(false);
            shieldLights.SetActive(false);

            //BossStateMachine.Instance.LeaveShield();
        }
    }
}
