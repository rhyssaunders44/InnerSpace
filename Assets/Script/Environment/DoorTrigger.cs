using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{ 
    Environment owner;

    private void Start()
    {
       owner = GetComponentInParent<Environment>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("bullet"))
        {
            owner.ChangeBool();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("bullet"))
        {
            owner.ChangeBool();
        }
    }
}
