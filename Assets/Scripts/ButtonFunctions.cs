using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public static void StartGame() {
        GameManager.StartGame();
    }

    public static void PauseGame() {
        GameManager.PauseGameUI();
    }

    public static void ResumeGame() {
        GameManager.ResumeGameUI();
    }

    public static void PlayAgain() {
        GameManager.StartGame();
    }

    public static void ExitToMenu() { 
        GameManager.ExitToMenu();
    }

    public static void QuitGame() {
        GameManager.QuitGame();
    }
}