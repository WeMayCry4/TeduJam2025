using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableEvent : MonoBehaviour, Interactable
{
    [Space] [Header(" Settings : ")]
    [SerializeField] Outline outline;
    [SerializeField] string interactionMsessage;
    public bool canBeInteractive = true;
    public bool canBeHold = false;

    [HideInInspector] public bool interactable {get => canBeInteractive;}
    [HideInInspector] public bool holdable {get => canBeHold;}

    [Space] [Header(" Event : ")]
    [SerializeField] UnityEvent Event;
    


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
        Event.Invoke();
    }
}
