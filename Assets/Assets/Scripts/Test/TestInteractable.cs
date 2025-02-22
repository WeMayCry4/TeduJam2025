using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestInteractable : MonoBehaviour, Interactable
{
    [Space]
    [Header(" Settings : ")]
    [SerializeField] Outline outline;
    [SerializeField] string interactionMsessage;
    public bool canBeInteractive = true;
    public bool canBeHold = true; // Changed to true.  Now this script manages holdable state.
    public Transform holdArea; // Where the object is held
    public float pickupRange = 2f; // added pickup range
    public float smoothTime = 0.2f; // Smoothing time for the pickup animation
    private Camera mainCam;

    [HideInInspector] public bool interactable { get => canBeInteractive; }
    [HideInInspector] public bool holdable { get => canBeHold; }

    [Space]
    [Header(" Event : ")]
    [SerializeField] UnityEvent Event;

    private ThrowableCube throwableCube; // Reference to the ThrowableCube script
    private Rigidbody rb;
    private Collider coll;

    private bool isHolding = false;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        throwableCube = GetComponent<ThrowableCube>(); // Get the ThrowableCube component
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        mainCam = Camera.main;

        if (throwableCube == null)
        {
            Debug.LogError("ThrowableCube script not found on this GameObject!");
        }

        // Disable ThrowableCube script at the start
        DisableThrowable();
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    public void DisableCollider()
    {
        GetComponent<Collider>().enabled = false;
    }

    public string GetUIMessage()
    {
        return interactionMsessage;
    }

    public List<Vector3> GetHandPosRot()
    {
        return null;
    }

    public void Interact()
    {
        // Pickup and drop logic
        if (isHolding)
        {
            Drop();
        }
        else
        {
            Pickup();
        }

        Event.Invoke(); // Keep invoking the original event (if any)
    }

    void Pickup()
    {
        // Check if the player is in range (you might need to get the player's position from somewhere)
        if (Vector3.Distance(transform.position, mainCam.transform.position) <= pickupRange)
        {
            isHolding = true;
            DisableThrowable(); // Disable the ThrowableCube script when holding
            StartCoroutine(MoveToHoldArea()); // Start smooth transition
        }
    }

    void Drop()
    {
        isHolding = false;
        StartCoroutine(EnableThrowableAfterDelay(0.1f)); // Enable the ThrowableCube script after dropping

        transform.SetParent(null); // Unparent from the hold area
        rb.useGravity = true;
        coll.enabled = true;
        rb.velocity = Vector3.zero; // Stop any residual velocity
        rb.angularVelocity = Vector3.zero;
    }

    // Enable and Disable ThrowableCube script
    public void EnableThrowable()
    {
        throwableCube.enabled = true;
    }

    public void DisableThrowable()
    {
        throwableCube.enabled = false;
    }

    private IEnumerator MoveToHoldArea()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        coll.enabled = false;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        float timeElapsed = 0;

        while (timeElapsed < smoothTime)
        {
            transform.position = Vector3.SmoothDamp(transform.position, holdArea.position, ref velocity, smoothTime);
            transform.rotation = Quaternion.Slerp(startRotation, holdArea.rotation, timeElapsed / smoothTime); // Rotate to the HoldArea's rotation
            transform.SetParent(holdArea); // Parent the cube to the Hold Area During Transition
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and rotation are exact
        transform.position = holdArea.position;
        transform.rotation = holdArea.rotation;
        transform.SetParent(holdArea);
    }

    private IEnumerator EnableThrowableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableThrowable();
    }
}