using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Casing : MonoBehaviour
{

    /// <summary>
    /// this class covers the casing
    /// </summary>
    private int deathTime = 40, physicsTime = 3;
    /// <summary>
    /// aquire the rigidbody and eject it sideways, like an automatic weapon would
    /// this also starts the timer to the casing disappearing
    /// this could be tied to quality settings but it is not
    /// </summary>
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(2,0,0),ForceMode.Impulse);
        StartCoroutine(Death());
    }

    /// <summary>
    /// on collision with anything, stop the physics interactions of the casing
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(routine: NoMorePhysics());
    }

    /// <summary>
    /// the casing's death timer
    /// </summary>
    /// <returns></returns>
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    /// <summary>
    /// the removal of the casing's rigidbody
    /// </summary>
    /// <returns></returns>
    private IEnumerator NoMorePhysics()
    {
        yield return new WaitForSeconds(physicsTime);
        Destroy(GetComponent<Rigidbody>());
    }

}
