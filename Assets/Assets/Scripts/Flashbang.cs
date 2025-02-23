using UnityEngine;
using System.Collections;

public class Flashbang : MonoBehaviour
{
    public float flashDuration = 2f; // Duration of the flash effect
    public string bossTag = "Boss"; // Tag of the boss GameObject


void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag(bossTag)) // Use collision.gameObject
    {
        // Get the BossStateMachine component from the boss
        BossStateMachine boss = collision.gameObject.GetComponent<BossStateMachine>(); //GetComponent on gameObject

        if (boss != null)
        {
            StartCoroutine(FlashBoss(boss)); // Flash the boss
        }

        // Optionally destroy the flashbang after it hits the boss
        Destroy(gameObject);
    }
}


    private IEnumerator FlashBoss(BossStateMachine boss)
    {
        // Stop boss movement and disable AI temporarily
        boss.agent.isStopped = true; // Assuming BossStateMachine has a public NavMeshAgent
        boss.enabled = false; // Disable the BossStateMachine script itself

        // Flash Effect (Implementation depends on how you're handling flash effects)
        // Example:  If using a material, you could change the material's color temporarily
        // Or, if using a white screen effect, you could activate that effect here


        yield return new WaitForSeconds(flashDuration);


        // Restore boss movement and AI after flash duration
        boss.agent.isStopped = false;
        boss.enabled = true;
    }
}