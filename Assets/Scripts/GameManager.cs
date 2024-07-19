using UnityEngine;

public class GameManager : MonoBehaviour {
    [HideInInspector] public static GameManager Instance { get; set; }

    #region Game Properties
    [HideInInspector] public static bool IsPlaying { get { return SceneLoader.IsInGameWorld; } }
    [HideInInspector] public static bool IsPaused { get; set; }
    [HideInInspector] public static bool IsGameOver { get; set; }

    public enum Players {
        One = 1, Two
    }
    #endregion Game Properties

    void Start() {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsGameOver) {
            if (!IsPaused) {
                Debug.Log("Paused!");
                PauseGameUI();
            }
            else {
                Debug.Log("Unpaused!");
                ResumeGameUI();
            }
        }
    }


    public static void StartGame() {
        IsGameOver = false;
        SceneLoader.LoadScene(SceneLoader.Scenes.Game_World);
        if (Time.timeScale < 1.0f)
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
        IsGameOver = true;
        PauseGame();
        Ball b = FindObjectOfType<Ball>();
        if (b != null)
            b.GetComponent<SpriteRenderer>().color = Color.red;


        Player[] players = FindObjectsOfType<Player>();
        foreach (Player p in players) {
            if (winner == p.PlayerID) {
                p.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }

        EndGame.DeclareWinner(winner);
    }
}