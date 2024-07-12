using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private int m_playerID;

    [SerializeField] private float m_speed = 15f;
    [SerializeField] private float m_lengthScale = 1f;

    private Rigidbody2D m_rigidBody;



    private void Awake() {
        m_rigidBody = GetComponent<Rigidbody2D>();

        if (gameObject.name == "PlayerA") {
            m_playerID = 0;
        }
        else {
            m_playerID = 1;
        }

        Vector3 scale = transform.localScale;
        scale.y *= m_lengthScale;
        transform.localScale = scale;
    }


    private void FixedUpdate() {
        if (GameManager.IsPlaying)
            HandleMovementInput();
    }


    private void HandleMovementInput() {
        int verticalMovement = 0;
        if (m_playerID == 0) {
            if (Input.GetKey(KeyCode.W)) { verticalMovement = 1; }
            else if (Input.GetKey(KeyCode.S)) { verticalMovement = -1; }

        }
        else if (m_playerID == 1) {
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


    public void Halt() {
        m_rigidBody.velocity = Vector2.zero;
    }
}