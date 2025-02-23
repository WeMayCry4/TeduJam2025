using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObjectThrow : Singleton<ObjectThrow>
{
    [SerializeField] Transform handPos;
    [SerializeField] Transform playerCam;
    [SerializeField] float throwForce, throwUpwardForce;
    Throwable heldObject;

    public void PickUpObject(Throwable throwable)
    {
        if (heldObject) return;
        heldObject = throwable;

        // Change the layer and set physics to kinematic.
        heldObject.gameObject.layer = 6;
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        // Parent the object to the hand and smoothly move/rotate it into position.
        heldObject.transform.SetParent(handPos);
        heldObject.transform.DOLocalMove(Vector3.zero, 0.3f);
        heldObject.transform.DOLocalRotate(Vector3.zero, 0.3f);

        // If this object has a StateMachine (i.e. it is an enemy), switch its state to HeldState.
        StateMachine sm = heldObject.GetComponent<StateMachine>();
        if (sm != null)
        {
            sm.ChangeState(new HeldState());
        }
    }

    public void Throw()
    {
        if (heldObject == null) return;

        // Re-enable physics and apply the throw force.
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(GetComponent<CharacterController>().velocity + playerCam.forward * throwForce, ForceMode.Impulse);
        }
        heldObject.transform.SetParent(null);
        
        // If the thrown object has a StateMachine (i.e. it is an enemy), switch its state back to PatrolState.
        StateMachine sm = heldObject.GetComponent<StateMachine>();
        if (sm != null)
        {
            // Delay the state change slightly to let the throw physics settle.
            DOVirtual.DelayedCall(0.5f, () =>
            {
                sm.ChangeState(new PatrolState());
            });
        }
        
        heldObject.Throwed();
        heldObject = null;
    }
}
