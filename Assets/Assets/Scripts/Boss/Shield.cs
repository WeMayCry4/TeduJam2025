using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Oyuncunun içeri girmesini engelle
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushBack = other.transform.position - transform.position;
                rb.AddForce(pushBack.normalized * 500f);
            }
        }
    }
}

