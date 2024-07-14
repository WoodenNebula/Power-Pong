using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public static void StartGame() {
        GameManager.StartGame();
    }

    public static void Pause() {
        GameManager.PauseGameUI();
    }

    public static void Resume() {
        GameManager.ResumeGameUI();
    }

    public static void PlayAgain() {
        GameManager.StartGame();
    }

    public static void ExitToMenu() { 
        GameManager.ExitToMenu();
    }

    public static void Quit() {
        GameManager.QuitGame();
    }
}