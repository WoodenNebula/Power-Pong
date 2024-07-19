using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{

    static ButtonFunctions Instance;
    AudioSource m_audioSource;

    void Awake() {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);

        m_audioSource = gameObject.GetComponent<AudioSource>();
    }

    static void PlayAudio() {
        Instance.m_audioSource.Play();
    }


    public static void StartGame() {
        PlayAudio();
        GameManager.StartGame();
    }

    public static void Pause() {
        PlayAudio();
        GameManager.PauseGameUI();
    }

    public static void Resume() {
        PlayAudio();
        GameManager.ResumeGameUI();
    }

    public static void PlayAgain() {
        PlayAudio();
        GameManager.StartGame();
    }

    public static void ExitToMenu() { 
        PlayAudio();
        GameManager.ExitToMenu();
    }

    public static void Quit() {
        PlayAudio();
        GameManager.QuitGame();
    }
}