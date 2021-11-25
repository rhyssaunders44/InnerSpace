using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using static UnityEngine.Debug;
public class Lighting : MonoBehaviour
{
    [SerializeField] private Light[] lamps;
    [SerializeField] private float minBrightness = 0, maxBrightness = 2, defaultBrightness = 0;
    [SerializeField] private bool dimming;
    
    private void FixedUpdate()
    {
        if (lamps[0].intensity <= minBrightness || lamps[0].intensity >= maxBrightness) 
        {
            dimming = !dimming;
        }
        
        if (dimming)
        {
            foreach (Light lamp in lamps)
            {
                lamp.intensity -= 0.02f;
            }
        }
        else
        {
            foreach (Light lamp in lamps)
            {
                lamp.intensity += 0.02f;
            }
        }
    }
}
