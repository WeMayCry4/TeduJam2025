using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : Singleton<ShieldManager>
{
    int targetCount;
    [SerializeField] GameObject shieldParent;
    [SerializeField] GameObject shieldLights;
    bool activated;


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

            BossStateMachine.Instance.LeaveShield();
        }
    }
}
