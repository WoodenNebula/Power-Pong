using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [HideInInspector] public static bool IsPlaying { get; set; }
    [HideInInspector] public static bool IsPaused { get; set; }


    public enum Players {
        One = 1, Two
    }


    private void Start() {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        StartGame();
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsPlaying) { PauseGame(); }
            else if (!IsPlaying && IsPaused) { ResumeGame(); }
        }
    }


    public static void StartGame() {
        IsPlaying = true;
        UILoader.Initialize();


        SceneLoader.LoadScene(SceneLoader.Scenes.Game_World);

        //UILoader.LoadUI(UILoader.UI.Player_HUD);
        UILoader.LoadUI(UILoader.UI.Pause_Menu, false);
        UILoader.LoadUI(UILoader.UI.Title_Screen, false);
        UILoader.LoadUI(UILoader.UI.End_Game, false);
    }


    public static void PauseGame() {
        IsPlaying = false;
        IsPaused = true;

        Ball.Pause();

        UILoader.LoadUI(UILoader.UI.Pause_Menu);
    }


    public static void ResumeGame() {
        IsPlaying = true;
        IsPaused = false;

        Ball.Resume();
        UILoader.LoadUI(UILoader.UI.Pause_Menu, false);
    }


    public void ExitToMenu() {
        IsPlaying = false;
        IsPaused = false;

        UILoader.ResetUIElements();

        SceneLoader.LoadScene(SceneLoader.Scenes.Title_Screen);
    }

    public static void QuitGame() {
        Application.Quit();
    }

    public static void EndGame(Players winner) {
        IsPlaying = false;
        IsPaused = false;

        UILoader.DeclareWinner(winner.ToString());
        UILoader.LoadUI(UILoader.UI.End_Game);
    }
}