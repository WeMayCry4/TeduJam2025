using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    static public float enemyDamage = 10f;
    static public float playerDamage = 20f;
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransfrom = collision.transform;
        if (hitTransfrom.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            hitTransfrom.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
        }
        else if (hitTransfrom.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            hitTransfrom.GetComponent<EnemyHealth>().EnemyTakeDamage(playerDamage);
        }
        Destroy(gameObject);
    }
}
