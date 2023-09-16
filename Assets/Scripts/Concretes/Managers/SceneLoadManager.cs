using MoreMountains.Tools;
using UnityEngine;


public class SceneLoadManager : MMSingleton<SceneLoadManager>
{
    public async void LoadGame()
    {
        await SceneLoader.LoadSceneAsyncCustom(SceneLoader.Scene.GameScene, 2f);
    }
    public async void LoadMenu()
    {
        await SceneLoader.LoadSceneAsyncCustom(SceneLoader.Scene.MenuScene, 2f);
    }
    public void ExitGame()
    {
        Application.Quit(0);
    }
    public async void LoadGameOnDead()
    {
        await SceneLoader.LoadSceneAsyncCustom(SceneLoader.Scene.GameScene, 1f);
    }
}
