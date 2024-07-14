using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {
    private static EndGame Instance { get; set; }

    private static GameObject s_endGameUI;
    private static TextMeshProUGUI s_winnerDeclareText;

    private static GameObject s_playAgainButton;
    private static GameObject s_exitToMenuButton;

    private void Start() {
        s_endGameUI = GameObject.Find("EndGameUI");

        s_endGameUI.transform.Find("Canvas").gameObject.SetActive(true);

        s_winnerDeclareText = s_endGameUI.GetComponentInChildren<TextMeshProUGUI>(true);

        s_playAgainButton = GameObject.Find("PlayAgainButton");
        Button playAgainButton = s_playAgainButton.GetComponent<Button>();
        playAgainButton.onClick.AddListener(() => { SceneLoader.LoadScene(SceneLoader.Scenes.Game_World); });

        s_exitToMenuButton = GameObject.Find("ExitToMenuButton");
        Button exitToMenuButton = s_exitToMenuButton.GetComponent<Button>();
        exitToMenuButton.onClick.AddListener(() => { SceneLoader.LoadScene(SceneLoader.Scenes.Title_Screen); });

        s_endGameUI.SetActive(false);
    }

    public static void DeclareWinner(GameManager.Players winner) {
        GameManager.PauseGame();
        EventSystem.current.SetSelectedGameObject(s_playAgainButton, new BaseEventData(EventSystem.current));


        if (s_winnerDeclareText != null) {
            s_winnerDeclareText.text = "Player " + winner.ToString() + " Won!";
        }
        else { Debug.LogError("WINNER IS NOT DETECTED"); }

        s_endGameUI.SetActive(true);
    }
}