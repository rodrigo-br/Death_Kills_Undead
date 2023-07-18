using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagment : Singleton<ScenesManagment>
{
    public static string SceneTransitionName { get; private set; }
    static int GetActiveSceneIndex() => SceneManager.GetActiveScene().buildIndex;
    public static void SetTransitionName(string name) => SceneTransitionName = name;

    public static void LoadNextScene()
    {
        int nextSceneIndex = GetActiveSceneIndex() + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public static void LoadSceneByName(string name) => SceneManager.LoadScene(name);
}
