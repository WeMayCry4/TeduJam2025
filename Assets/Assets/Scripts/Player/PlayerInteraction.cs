using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float playerReach = 3f;

    RaycastHit hit;
    Interactable currentInteractable;
    bool lockInteraction;


    void Update()
    {
        CheckInteractions();
    }

    void CheckInteractions()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if(Physics.Raycast(ray, out hit, playerReach))
        {
            if(hit.collider.tag == "Interactable" && hit.collider.GetComponent<Interactable>().interactable)
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                if(currentInteractable != null && newInteractable != currentInteractable) currentInteractable.DisableOutline();
                SetCurrentInteractable(newInteractable);
            }
            else DisableCurrentInteractable();
        }
        else DisableCurrentInteractable();
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------

    void SetCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();

        UIManager.Instance.EnableInteractionTest(currentInteractable.GetUIMessage());
    }

    void DisableCurrentInteractable()
    {
        if(currentInteractable != null)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
        UIManager.Instance.DisableInteractionText();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public bool CheckIsThereAInteractable()
    {
        if(currentInteractable != null) return true;
        else return false;
    }

    public void Interaction()
    {
        if(lockInteraction) return;
        currentInteractable.Interact();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void LockInteraction()
    {
        lockInteraction = true;
    }

    public void UnlockInteraction()
    {
        lockInteraction = false;
    }
}
