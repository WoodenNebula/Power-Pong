using System;
using UnityEngine.SceneManagement;


public static class SceneLoader {
    static Action onLoaderCallback;
    public static bool IsInGameWorld {get; set;}

    public enum Scenes {
        Title_Screen = 0,
        Game_World,
        Loading_Screen,
    }

    public static void LoadScene(Scenes scene) {
        // Register the scene to load after loading screen
        onLoaderCallback = () => {
            SceneManager.LoadScene((int)scene);
        };

        if (scene == Scenes.Game_World) {
            IsInGameWorld = true;
        } 
        else {
            IsInGameWorld = false;
        }

        SceneManager.LoadScene((int)Scenes.Loading_Screen);
    }

    public static void LoaderCallback() {
        // If scene callback registered, then load it and reset the callback
        if (onLoaderCallback != null) {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

}