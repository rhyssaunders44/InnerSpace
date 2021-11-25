using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class contains all Options menu functions
/// </summary>
public class Options : MonoBehaviour
{
    
    private Text killCounter;
    private int killCount;
    [SerializeField] private Dropdown qualityDropdown, aaDropDown;
    [SerializeField] private bool paused;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject Balls;
    public static int ligma;
    
    ///<summary> Sets up the number of enemies, and the standard run quality </summary>
    void Start()
    {
        ligma = 51;
        QualitySettings.SetQualityLevel(1);

        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    /// <summary>
    /// detects player pause input and if the game has ended
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(paused);
        }
        VeryNice();
    }

    /// <summary>
    /// if the monster count is 0, end the game
    /// </summary>
    public void VeryNice()
    {
        if (ligma <= 0)
        {
            Balls.SetActive(true);
        }
    }

    /// <summary>
    /// drop down for the general quality setting
    /// </summary>
    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    
    /// <summary>
    /// drop down for the anti aliasing setting
    /// </summary>
    public void ChangeAASetting()
    {
        QualitySettings.antiAliasing = aaDropDown.value;
    }

    /// <summary>
    /// controls the pause function including timescale
    /// </summary>
    /// <param name="active"></param>
    public void Pause(bool active)
    {
        if (!active)
        {
            paused = true;
            optionsMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            optionsMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Quits the program
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
