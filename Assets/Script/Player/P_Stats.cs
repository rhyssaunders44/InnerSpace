using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// the player health manager is managed by this class
/// </summary>
public class P_Stats : MonoBehaviour
{
    private float maxHP;
    private float currentHP;
    private Image[] hpBars;
    private bool healing;
    
    /// <summary>
    /// Set player stats
    /// </summary>
    void Start()
    {
        currentHP = maxHP;
        HPChange(0);
    }

    /// <summary>
    /// an un implamented healing function that healed every tick
    /// </summary>
    void Update()
    {
        if (currentHP > maxHP)
        {
            StopCoroutine(Healing());
            currentHP = maxHP;
        }
    }

    /// <summary>
    /// registers the damage taken and impacts the player health
    /// </summary>
    /// <param name="damageTaken"></param>
    void HPChange(float damageTaken)
    {
        foreach (Image _hpBar in hpBars)
        {
            _hpBar.fillAmount = (currentHP - damageTaken)  / maxHP;
        }
    }

    /// <summary>
    /// an unImplemented healing function
    /// </summary>
    /// <returns></returns>
    private IEnumerator Healing()
    {
        while (healing && currentHP < maxHP)
        {
            HPChange(-0.1f);
            yield return new WaitForSeconds(0.1f);
        }

    }

    /// <summary>
    /// Unimplemented healing trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            StartCoroutine(Healing());
        }
    }

    /// <summary>
    /// unimplemented cessation of healing
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            StopCoroutine(Healing());
        }
    }
}
