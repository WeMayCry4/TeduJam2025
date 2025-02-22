using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    GameObject gameObject { get; }
    bool interactable { get ; }
    bool holdable { get ; }

    List<Vector3> GetHandPosRot();
    void EnableCollider();
    void DisableCollider();
    string GetUIMessage();
    void EnableOutline();
    void DisableOutline();
    void Interact();
}
