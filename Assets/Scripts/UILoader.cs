using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UILoader : MonoBehaviour {
    public static UILoader Instance { get; private set; }


    [Header("First Selected Buttons")]
    [SerializeField] private GameObject m_startButton;
    [SerializeField] private GameObject m_resumeButton;
    [SerializeField] private GameObject m_playAgainButton;

    [HideInInspector] public static TextMeshProUGUI WinnerDeclareTextBox {  get; set; }

    private GameObject m_titleScreenUI;
    private GameObject m_pauseMenuUI;
    private GameObject m_endGameUI;
    //private GameObject m_playerHUD;

    public enum UI {
        Title_Screen,
        Pause_Menu,
        End_Game,
        //Player_HUD
    }

    private void Start() {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        Instance.m_titleScreenUI = GameObject.Find("TitleScreenUI");
        Instance.m_pauseMenuUI = GameObject.Find("PauseMenuUI");
        //Instance.m_playerHUD = GameObject.Find("PlayerHUD");
        Instance.m_endGameUI = GameObject.Find("EndGameUI");

        Instance.m_startButton = GameObject.Find("StartButton");
        Instance.m_resumeButton = GameObject.Find("ResumeButton");
        Instance.m_playAgainButton = GameObject.Find("PlayAgainButton");


        WinnerDeclareTextBox = Instance.m_endGameUI.GetComponentInChildren<TextMeshProUGUI>();

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Instance.m_endGameUI);
        DontDestroyOnLoad(Instance.m_titleScreenUI);
        DontDestroyOnLoad(Instance.m_pauseMenuUI);
    }


    // Initialize all the UI for first scene
    public static void Initialize() {
        LoadUI(UI.Pause_Menu, false);
        LoadUI(UI.End_Game, false);
        LoadUI(UI.Title_Screen);
        //LoadUI(UI.Player_HUD, false);
    }


    public static void ResetUIElements() {
        Destroy(Instance.m_endGameUI);
        Destroy(Instance.m_titleScreenUI);
        Destroy(Instance.m_pauseMenuUI);
    }


    public static void LoadUI(UI ui, bool shouldLoad = true) {
        var eventSystem = EventSystem.current;
        Debug.Log("Pausing " + ui.ToString());
        switch (ui) {
            case UI.Title_Screen:
                if (Instance.m_titleScreenUI != null) { 
                    Instance.m_titleScreenUI.SetActive(shouldLoad); 
                }
                eventSystem.SetSelectedGameObject(
                    Instance.m_startButton,
                    new BaseEventData(eventSystem));
                break;

            case UI.Pause_Menu:
                if (Instance.m_pauseMenuUI != null) { 
                    Instance.m_pauseMenuUI.SetActive(shouldLoad); 
                }
                eventSystem.SetSelectedGameObject(
                    Instance.m_resumeButton, 
                    new BaseEventData(eventSystem));
                break;

            case UI.End_Game:
                if (Instance.m_endGameUI != null) { 
                    Instance.m_endGameUI.SetActive(shouldLoad);
                }
                eventSystem.SetSelectedGameObject(
                    Instance.m_playAgainButton, 
                    new BaseEventData(eventSystem));
                break;

                //case UI.Player_HUD:
                //if (m_playerHUD != null) { m_playerHUD.SetActive(shouldLoad); }
                //break;
        }
    }

    public static void DeclareWinner(string winner) {
        if (WinnerDeclareTextBox != null) {
            WinnerDeclareTextBox.text = "Player " + winner + " Won";
        }
    }
}