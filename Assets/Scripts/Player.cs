using UnityEngine;

public class Player : MonoBehaviour {
    public GameManager.Players PlayerID {  get; private set; }

    [SerializeField] private float m_speed = 15f;
    [SerializeField] private float m_lengthScale = 1f;

    private Rigidbody2D m_rigidBody;

    private void Awake() {
        m_rigidBody = GetComponent<Rigidbody2D>();

        if (gameObject.name == "PlayerA") {
            PlayerID = GameManager.Players.One;
        }
        else {
            PlayerID = GameManager.Players.Two;
        }

        Vector3 scale = transform.localScale;
        scale.y *= m_lengthScale;
        transform.localScale = scale;
    }


    private void FixedUpdate() {
        if (GameManager.IsPlaying && !GameManager.IsPaused)
            HandleMovementInput();
    }


    private void HandleMovementInput() {
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


    public Vector2 GetVelocityDir() {
        Vector2 vel = m_rigidBody.velocity;
        vel.Normalize();
        return vel;
    }
}