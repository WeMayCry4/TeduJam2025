using UnityEngine;

public class ShieldSpot : MonoBehaviour
{
    private BossStateMachine boss;

    void Start()
    {
        // Find the BossStateMachine in the scene
        boss = FindObjectOfType<BossStateMachine>();

        if (boss == null)
        {
            Debug.LogError("BossStateMachine not found in the scene!");
        }
    }

    void OnDestroy()
    {
        if (boss != null)
        {
            boss.RemoveShieldSpot(gameObject);
        }
    }
}