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
        if(heldObject) return;
        heldObject = throwable;

        heldObject.gameObject.layer = 6;
        heldObject.GetComponent<Rigidbody>().isKinematic = true;

        heldObject.transform.SetParent(handPos);
        heldObject.transform.DOLocalMove(Vector3.zero, 0.3f);
        heldObject.transform.DOLocalRotate(Vector3.zero, 0.3f);
    }

    public void Throw()
    {
        if(heldObject == null) return;

        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.GetComponent<Rigidbody>().AddForce(GetComponent<CharacterController>().velocity + playerCam.forward * throwForce, ForceMode.Impulse);
        heldObject.transform.SetParent(null);
        heldObject.Throwed();
        heldObject = null;
    }
}
