using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public static PauseMenu Instance { get; set; }

    private static GameObject s_pauseMenuUI;

    private static GameObject s_resumeButton;
    private static GameObject s_exitToMenuButton;

    private void Start() {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        s_pauseMenuUI = GameObject.Find("PauseMenuUI");
        s_pauseMenuUI.transform.Find("Canvas").gameObject.SetActive(true);

        s_resumeButton = GameObject.Find("ResumeButton");
        Button resumeButton = s_resumeButton.GetComponentInChildren<Button>();
        resumeButton.onClick.AddListener(() => {
            Debug.Log("Clicked Resume");
            ButtonFunctions.Resume();
        });

        s_exitToMenuButton = GameObject.Find("ExitToMenuButton");
        Button exitToMenuButton = s_exitToMenuButton.GetComponentInChildren<Button>();
        exitToMenuButton.onClick.AddListener(() => { SceneLoader.LoadScene(SceneLoader.Scenes.Title_Screen); });


        s_pauseMenuUI.SetActive(false);
    }

    public static void Load() {
        EventSystem.current.SetSelectedGameObject(s_resumeButton, new BaseEventData(EventSystem.current));
        s_pauseMenuUI.SetActive(true);
    }
    public static void UnLoad() {
        RemoveListeners();
        s_pauseMenuUI.SetActive(false);
    }

    public static void RemoveListeners() {
        s_resumeButton.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        s_exitToMenuButton.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
    }
}