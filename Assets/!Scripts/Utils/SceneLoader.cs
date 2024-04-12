using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static Action<int> OnSceneLoaded;
    public static void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        OnSceneLoaded?.Invoke(buildIndex);
    }
}
