using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IResetAble {
    public GameManager.Players PlayerID { get; private set; }

    [SerializeField] float m_speed = 15f;

    AudioSource m_audioSource;
    Rigidbody2D m_rigidBody;


    IAbility m_warpAbility;

    void Awake() {
        m_warpAbility = new WarpBall(this);

        m_rigidBody = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
    }


    void Start() {
        if (gameObject.name == "PlayerA") {
            PlayerID = GameManager.Players.One;
            GameManager.SubmitPlayer(this);
        }
        else {
            PlayerID = GameManager.Players.Two;
            GameManager.SubmitPlayer(this);
        }
        PlayerHUD.UpdateAbilityDisplay(PlayerID, m_warpAbility.RemainingUsage);

        Reset();
    }


    void Update() {
        if (GameManager.IsPlayerControllable && !GameManager.IsPaused)
            HandleAbilityInput();
    }

    void FixedUpdate() {
        if (GameManager.IsPlayerControllable && !GameManager.IsPaused)
            HandleMovementInput();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.name == "Ball") {
            m_audioSource.Play();
        }
    }

    void OnDestroy() {
        Debug.Log("Destroyed!");
    }


    public void Reset() {
        if (PlayerID == GameManager.Players.One) {
            transform.position = new Vector3(-20.0f, 0.0f, 0.0f);
        }
        else if (PlayerID == GameManager.Players.Two) {
            transform.position = new Vector3(20.0f, 0.0f, 0.0f);
        }

        gameObject.GetComponent<SpriteRenderer>().color = new Color(82/255.0f, 255/255.0f, 0/255.0f);
    }


    void HandleAbilityInput() {
        if (PlayerID == GameManager.Players.One) {
            if (Input.GetKeyDown(KeyCode.D)) {
                Debug.Log("Ability Called by One");
                m_warpAbility.Use();
            }
        }
        else if (PlayerID == GameManager.Players.Two) {
            if (Input.GetKeyDown(KeyCode.RightShift)) {
                Debug.Log("Ability Called by Two");
                m_warpAbility.Use();
            }
        }
    }

    void HandleMovementInput() {
        int verticalMovement = 0;
        if (PlayerID == GameManager.Players.One) {
            if (Input.GetKey(KeyCode.W)) { verticalMovement = 1; }
            else if (Input.GetKey(KeyCode.S)) { verticalMovement = -1; }
        }
        else if (PlayerID == GameManager.Players.Two) {
            if (Input.GetKey(KeyCode.UpArrow)) { verticalMovement = 1; }
            else if (Input.GetKey(KeyCode.DownArrow)) { verticalMovement = -1; }
        }

        // if no movement
        if (verticalMovement == 0) { m_rigidBody.velocity = Vector2.zero; }
        else { m_rigidBody.velocity = transform.up * (verticalMovement * m_speed * Time.fixedDeltaTime); }
    }


}