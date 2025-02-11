using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{
    public static void LoadScene(Scenes scene)
    {
        SceneManager.LoadSceneAsync((int)scene);
    }
}

public enum Scenes
{
    Menu,
    Gameplay,
}
