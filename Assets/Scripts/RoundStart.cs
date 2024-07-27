using TMPro;
using UnityEngine;

public class RoundStart : MonoBehaviour {
    static RoundStart Instance { get; set; }

    [SerializeField] GameObject m_roundStartCanvas;
    [SerializeField] TextMeshProUGUI m_infoText;


    void Start() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public static void EnableUI(bool shouldEnable = true) {
        Debug.Log("Should Start enable = " + shouldEnable);
        if (!shouldEnable) {
            Instance.m_infoText.text = "<s>PRESS ESC TO START</s>";
        } else {
            Instance.m_infoText.text = "PRESS ESC TO START";
        }
        Instance.m_roundStartCanvas.SetActive(shouldEnable);
    }
}