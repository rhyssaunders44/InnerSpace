using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private GameObject levelDoor, doorStart, doorEnd;
    [SerializeField] private float cutOffDist = 0.1f;
    [SerializeField] private Vector3 openPos;
    [SerializeField] private Vector3 closedPos;
    [SerializeField] private bool open;
    [SerializeField] private float coolDown;

    private void Start()
    {
        openPos = doorEnd.transform.position;
        closedPos = doorStart.transform.position;
    }

    private void FixedUpdate()
    {
        DoorMoving(open);
    }

    private void DoorMoving(bool closed)
    {
        Vector3 currentPos = levelDoor.transform.position;
        Vector3 targetPos = closed ? openPos : closedPos;

        float distanceToTarget = Vector3.Distance(currentPos, targetPos);

        if (distanceToTarget > cutOffDist)
            levelDoor.transform.position = Vector3.Lerp(currentPos, targetPos, Time.fixedDeltaTime);
    }

    public void ChangeBool()
    {
        if (Time.time > coolDown)
        {
            coolDown = Time.time + 0.3f;
            open = !open; 
        }
    }
}
