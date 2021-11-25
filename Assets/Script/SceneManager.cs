using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void Ded()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}
