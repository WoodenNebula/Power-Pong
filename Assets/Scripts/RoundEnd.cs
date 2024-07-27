using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoundEnd : MonoBehaviour {
    static RoundEnd Instance { get; set; }

    [SerializeField] GameObject m_roundEndCanvas;
    [SerializeField] TextMeshProUGUI m_roundWinnerDeclareText;
    [SerializeField] GameObject m_nextRoundButton;
    [SerializeField] GameObject m_exitToMenuButton;



    void Start() {
        if (Instance == null) {
            Instance = this;
        }

        EnableUI();

        Button nextRoundButton = m_nextRoundButton.GetComponent<Button>();

        nextRoundButton.onClick.AddListener(() => {
            Debug.Log("Resetting!");
            GameManager.ResetRound();
            EnableUI(false);
        });

        Button exitToMenuButton = m_exitToMenuButton.GetComponent<Button>();
        exitToMenuButton.onClick.AddListener(() => { ButtonFunctions.ExitToMenu(); });
        EnableUI(false);
    }

    public static void EnableUI(bool shouldEnable = true) {
        Instance.m_roundEndCanvas.SetActive(shouldEnable);
    }


    public static void DeclareWinner(GameManager.Players winner) {
        Debug.Log("Declaring Winner!");
        GameManager.PauseGame();
        EnableUI();

        EventSystem.current.SetSelectedGameObject(Instance.m_nextRoundButton, new BaseEventData(EventSystem.current));

        if (Instance.m_roundWinnerDeclareText != null) {
            if (winner == GameManager.Players.None) {
                Instance.m_roundWinnerDeclareText.text = "Draw!";
            }
            else {
                Instance.m_roundWinnerDeclareText.text = "Player " + winner.ToString() + " Won the round!";
            }
        }
        else { Debug.LogError("WINNER IS NOT DETECTED"); }
    }
}