using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First : MonoBehaviour
{
    private async void Start()
    {
        DontDestroyOnLoad(gameObject);
        await SceneLoader.LoadSceneAsyncCustom(SceneLoader.Scene.MenuScene, 2f);
    }
}
