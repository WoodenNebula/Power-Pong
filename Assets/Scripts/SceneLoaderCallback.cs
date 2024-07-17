using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour {
    bool isFirstUpdate = true;

    void Update() {
        if (isFirstUpdate) {
            isFirstUpdate = false;
            SceneLoader.LoaderCallback();
        }
    }
}