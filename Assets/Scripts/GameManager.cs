using UnityEngine;

public interface IResetAble {
    public void Reset();
}

public class GameManager : MonoBehaviour {
    [HideInInspector] public static GameManager Instance { get; set; }
    static AudioSource s_victorySound;

    static public readonly int ROUNDS_PER_GAME = 3;
    static public readonly int NUM_PLAYERS = 2;

    IResetAble m_ball;
    IResetAble[] m_players;


    #region Game Properties
    //[HideInInspector] public static int CurrentRound { get; set; } = 0;
    [HideInInspector] public static bool IsPaused { get; set; }
    [HideInInspector] public static bool IsRecentlyLoaded { get; set; }
    [HideInInspector] public static bool IsResumeAble { get; set; }
    [HideInInspector] public static bool IsPlayerControllable { get; private set; }
    [HideInInspector]
    public static bool IsGameOver {
        get {
            return ((PlayerWins[0] == ROUNDS_PER_GAME) ||
                (PlayerWins[1] == ROUNDS_PER_GAME));
        }
    }

    [HideInInspector] public static int[] PlayerWins = { 0, 0 };

    public enum Players {
        None = -1, One = 0, Two
    }

    #endregion Game Properties


    void Start() {
        if (Instance == null) {
            Instance = this;
        }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);

        if(SceneLoader.IsInGameWorld)
            IsResumeAble = true;
    }

    void Update() {
        if (IsRecentlyLoaded) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                IsRecentlyLoaded = false;
                RoundStart.EnableUI(false);
                ResumeGame();
                return;
            }
        }

        else if (IsResumeAble && Input.GetKeyDown(KeyCode.Escape)) {
            if (!IsPaused) {
                PauseGameUI();
            }
            else {
                ResumeGameUI();
            }
        }
    }


    public static void StartGame() {
        // Reset Rounds
        PlayerWins[0] = 0;
        PlayerWins[1] = 0;
        Instance.m_players = new IResetAble[PlayerWins.Length];

        SceneLoader.LoadScene(SceneLoader.Scenes.Game_World);

        IsPlayerControllable = false;
        IsRecentlyLoaded = true;

        PauseGame();
    }

    public static void PauseGameUI() {
        IsResumeAble = true;

        PauseGame();
        PauseMenu.Load();
    }

    public static void ResumeGameUI() {
        ResumeGame();
        PauseMenu.UnLoad();
    }

    public static void PauseGame() {
        IsPaused = true;
        IsPlayerControllable = false;
        Time.timeScale = 0.0f;
    }

    public static void ResumeGame() {
        IsPaused = false;
        IsPlayerControllable = true;
        Time.timeScale = 1.0f;
    }

    public static void ExitToMenu() {
        IsResumeAble = false;
        IsPlayerControllable = false;
        SceneLoader.LoadScene(SceneLoader.Scenes.Title_Screen);
    }

    public static void QuitGame() {
        Destroy(Instance);
        Application.Quit();
    }

    public static void SubmitBall(IResetAble ball) {
        Instance.m_ball = ball;
    }

    public static void SubmitPlayer(IResetAble player) {
        if (Instance.m_players == null) {
            Instance.m_players = new IResetAble[NUM_PLAYERS];
        }

        if (Instance.m_players[0] == null) {
            Instance.m_players[0] = player;
        }
        else if (Instance.m_players[1] == null) {
            Instance.m_players[1] = player;
        }
    }


    public static void ResetRound() {
        IsResumeAble = true;

        IsPlayerControllable = false;

        for (int i = 0; i < NUM_PLAYERS; i++) {
            if (Instance.m_players[i] != null) {
                Instance.m_players[i].Reset();
            }
        }

        Instance.m_ball.Reset();
        IsRecentlyLoaded = true;
        RoundStart.EnableUI();
    }

    public static void FinishRound(Players winner) {
        IsResumeAble = false;
        PauseGame();

        //CurrentRound++;

        ColorPlayer(winner);

        if (winner == Players.One) {
            PlayerWins[(int)Players.One]++;
        }
        else if (winner == Players.Two) {
            PlayerWins[(int)Players.Two]++;
        }
        PlayerHUD.UpdateWinCount();

        if (IsGameOver) {
            FinishGame();
        } 
        else {
            RoundEnd.DeclareWinner(winner);
        }
    }


    public static void FinishGame() {
        if (!IsGameOver)
            return;
        Players winner = Players.None;
        // Find Winner
        if (PlayerWins[(int)Players.One] == ROUNDS_PER_GAME)
            winner = Players.One;
        else if (PlayerWins[(int)Players.Two] == ROUNDS_PER_GAME)
            winner = Players.Two;

        ColorPlayer(winner);

        // Play Victory Sound
        if(s_victorySound == null) {
            s_victorySound = GameObject.Find("Players").GetComponent<AudioSource>();
        }
        s_victorySound.Play();

        // Load the winner declaration UI
        EndGame.DeclareWinner(winner);
    }

    public static void ColorPlayer(Players winner) {
        // Highlight Winner
        foreach (Player p in FindObjectsOfType<Player>()) {
            if (winner == p.PlayerID) {
                p.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
    }
}