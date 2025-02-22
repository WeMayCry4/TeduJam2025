using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Transform guide; // Where the object will be held
    public float throwForce = 10f; // How strong the throw is

    private GameObject heldObject;
    private Rigidbody heldRb;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null) // Try to pick up
                TryPickup();
            else // Drop it if already holding
                DropObject();
        }

        if (Input.GetMouseButtonDown(0) && heldObject != null)
        {
            ThrowObject();
        }

        if (heldObject != null)
        {
            heldObject.transform.position = guide.position; // Keep object in hand
        }
    }

    private void TryPickup()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f); // Detect nearby objects
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Interactable"))
            {
                heldObject = col.gameObject;
                heldRb = heldObject.GetComponent<Rigidbody>();

                if (heldRb != null)
                {
                    heldRb.useGravity = false;
                    heldRb.velocity = Vector3.zero;
                    heldObject.transform.SetParent(guide);
                    heldObject.transform.position = guide.position;
                }

                break; // Stop after finding one object
            }
        }
    }

    private void DropObject()
    {
        if (heldObject == null) return;

        heldObject.transform.SetParent(null);
        if (heldRb != null)
        {
            heldRb.useGravity = true;
        }

        heldObject = null;
        heldRb = null;
    }

    private void ThrowObject()
    {
        if (heldObject == null) return;

        heldObject.transform.SetParent(null);
        if (heldRb != null)
        {
            heldRb.useGravity = true;
            heldRb.velocity = transform.forward * throwForce;
        }

        // Attach the thrown object script if not already present
        if (!heldObject.GetComponent<ThrownObject>())
        {
            heldObject.AddComponent<ThrownObject>();
        }

        heldObject = null;
        heldRb = null;
    }
}
