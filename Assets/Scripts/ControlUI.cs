using UnityEngine;

public class ControlUI : MonoBehaviour {
    public static ControlUI Instance { get; private set; }
    GameObject m_controlUI;
    GameObject m_controlUICanvas;


    void Start() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        m_controlUI = GameObject.Find("ControlUI");
        m_controlUICanvas = m_controlUI.transform.Find("Canvas").gameObject;
        Debug.Log("Is First game = " + GameManager.IsFirstGame);
        EnableUI(GameManager.IsFirstGame);
    }

    public static void EnableUI(bool shouldEnable = true) {
        Instance.m_controlUICanvas.SetActive(shouldEnable);
    }
}