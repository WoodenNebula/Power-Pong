using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [HideInInspector] public static GameManager Instance { get; private set; }

    [HideInInspector] public static bool IsPlaying { get { return SceneLoader.IsInGameWorld; } }
    [HideInInspector] public static bool IsPaused { get; set; }


    public enum Players {
        One = 1, Two
    }


    private void Start() {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!IsPaused) {
                Debug.Log("Paused!");
                PauseGameUI();
            }
            else {
                ResumeGameUI();
            }
        }
    }


    public static void StartGame() {
        SceneLoader.LoadScene(SceneLoader.Scenes.Game_World);
        if(Time.timeScale < 1.0f)
            Time.timeScale = 1.0f;
        IsPaused = false;
    }

    public static void PauseGameUI() {
        PauseGame();
        PauseMenu.Load();
    }

    public static void ResumeGameUI() {
        ResumeGame();
        PauseMenu.UnLoad();
    }

    public static void PauseGame() {
        IsPaused = true;
        Time.timeScale = 0.0f;
    }

    public static void ResumeGame() {
        IsPaused = false;
        Time.timeScale = 1.0f;
    }

    public static void ExitToMenu() {
        SceneLoader.LoadScene(SceneLoader.Scenes.Title_Screen);
    }

    public static void QuitGame() {
        Debug.Log("Quiting Game!");
        Application.Quit();
    }

    public static void FinishGame(Players winner) {
        EndGame.DeclareWinner(winner);
    }
}