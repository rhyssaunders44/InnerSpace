using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// this class Manages the player health.
/// </summary>
public class HealthManager : MonoBehaviour
{
	public float currentHP, maxHP;
	public UnityEvent onDeathEvent;
	[SerializeField] private Image healthBar;

	/// <summary>
	/// sets the starting player hp and updates the display
	/// </summary>
    void Start()
    {
		currentHP = maxHP;
		UpdateDisplay();
    }

    /// <summary>
    /// runs when the player takes damage, by the selected hit point amount.
    /// </summary>
    /// <param name="changeBy"></param>
    public void ChangeHP(float changeBy)
    {
	    currentHP += changeBy;
		UpdateDisplay();
		if (currentHP > maxHP)
			currentHP = maxHP;
		if (currentHP <= 0)
			onDeathEvent.Invoke();
	}
    

    /// <summary>
    /// When the player takes damage the display is updated
    /// </summary>
    void UpdateDisplay()
	{
		if (healthBar == null)
			return;
		healthBar.fillAmount = currentHP/maxHP;
	}
}
