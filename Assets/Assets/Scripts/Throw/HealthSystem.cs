using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage! Remaining HP: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log(gameObject.name + " was destroyed!");
        }
    }
}
