using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : Singleton<LastBoss>
{
    [SerializeField] GameObject bigBoss;

    public void Activate(Transform copy)
    {
        bigBoss.SetActive(true);
        bigBoss.transform.position = copy.position;
        bigBoss.transform.rotation = copy.rotation;
    }
}
