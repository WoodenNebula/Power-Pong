using UnityEngine;

public class Player : MonoBehaviour {
    public GameManager.Players PlayerID { get; private set; }

    [SerializeField] float m_speed = 15f;
    [SerializeField] float m_lengthScale = 1f;

    AudioSource m_audioSource;
    Rigidbody2D m_rigidBody;


    IAbility warpAbility;

    void Awake() {
        m_rigidBody = GetComponent<Rigidbody2D>();

        if (gameObject.name == "PlayerA") {
            PlayerID = GameManager.Players.One;
        }
        else {
            PlayerID = GameManager.Players.Two;
        }


        warpAbility = new WarpBall(this);

        m_audioSource = GetComponent<AudioSource>();

        Vector3 scale = transform.localScale;
        scale.y *= m_lengthScale;
        transform.localScale = scale;
    }


    void Update() {
        if (GameManager.IsPlaying && !GameManager.IsPaused)
            HandleAbilityInput();
    }

    void FixedUpdate() {
        if (GameManager.IsPlaying && !GameManager.IsPaused) 
            HandleMovementInput();
    }


    void HandleAbilityInput() {
        if (PlayerID == GameManager.Players.One) {
            if (Input.GetKeyDown(KeyCode.D)) {
                Debug.Log("Ability Called by One");
                warpAbility.Use();
            }
        }
        else if (PlayerID == GameManager.Players.Two) {
            if (Input.GetKeyDown(KeyCode.RightShift)) {
                Debug.Log("Ability Called by Two");
                warpAbility.Use();
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


    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.name == "Ball") {
            m_audioSource.Play();
        }
    }

    public Vector2 GetVelocityDir() {
        Vector2 vel = m_rigidBody.velocity;
        vel.Normalize();
        return vel;
    }
}