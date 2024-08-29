using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        GameScene
    }

    private static Scene scene;

    public static void Load(Scene scene)
    {

        Loader.scene = scene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());

        Debug.Log("da kich hoat Load");
    }

    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(scene.ToString());
        Debug.Log("da kich hoat LoadrCallBack ben Loadre");
    }
}
