using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_HitSplat : MonoBehaviour
{
    /// <summary>
    /// this class covers the ricochets 
    /// </summary>
    private float deathtime;
    
    void Start()
    {
        deathtime = Time.time + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > deathtime)
        {
            Destroy(gameObject);
        }
    }
}
