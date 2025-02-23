using UnityEngine;

public class Flashbang1 : MonoBehaviour
{
    public float flashDuration = 2f;
    public int damageAmount = 0;
    [SerializeField] GameObject fx;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boss")) // Add a "Boss" tag to your boss object
        {
            BossStateMachine boss = collision.gameObject.GetComponent<BossStateMachine>();
            if (boss != null)
            {
                boss.Stun(flashDuration);
                boss.TakeDamage(damageAmount);
                fx.SetActive(true);
                fx.transform.SetParent(null);
                Destroy(gameObject); // Destroy the flashbang
            }
        }
    }
}