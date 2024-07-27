using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour {
    public static PlayerHUD Instance { get; private set; }

    static GameObject s_playerHUD;

    public static GameObject[] PlayerAbilitiesDisplay;
    public static TextMeshProUGUI[] PlayerScoreDisplayText;


    void Awake() {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        s_playerHUD = GameObject.Find("PlayerHUD");

        PlayerScoreDisplayText = new TextMeshProUGUI[GameManager.NUM_PLAYERS];
        PlayerAbilitiesDisplay = new GameObject[GameManager.NUM_PLAYERS];

        PlayerScoreDisplayText[(int)GameManager.Players.One] = GameObject.Find("PlayerOneWinCount").GetComponent<TextMeshProUGUI>();
        PlayerScoreDisplayText[(int)GameManager.Players.Two] = GameObject.Find("PlayerTwoWinCount").GetComponent<TextMeshProUGUI>();

        PlayerAbilitiesDisplay[(int)GameManager.Players.One] = GameObject.Find("PlayerOneAbilities");
        PlayerAbilitiesDisplay[(int)GameManager.Players.Two] = GameObject.Find("PlayerTwoAbilities");

        UpdateWinCount();
    }

    public static void UpdateWinCount() {
        PlayerScoreDisplayText[(int)GameManager.Players.One].text = GameManager.PlayerWins[(int)GameManager.Players.One].ToString();
        PlayerScoreDisplayText[(int)GameManager.Players.Two].text = GameManager.PlayerWins[(int)GameManager.Players.Two].ToString();
    }

    public static void UpdateAbilityDisplay(GameManager.Players player, int remainingUsage) {
        TextMeshProUGUI ability = PlayerAbilitiesDisplay[(int)player].GetComponent<TextMeshProUGUI>();

        ability.text = remainingUsage.ToString();

        if (remainingUsage <= 0) {
            Debug.LogWarning("OUT OF USES!");
            ability.color = Color.red;
        }
    }
}