using UnityEngine;

public class Ball : MonoBehaviour {
    [HideInInspector] public static Ball Instance { get; set; }

    [SerializeField] float m_speed = 10.0f;

    Rigidbody2D m_rigidbody;

    void Awake() {
        if (Instance == null) { Instance = this; }
        //else { Destroy(gameObject); }

        m_rigidbody = GetComponent<Rigidbody2D>();
        m_rigidbody.inertia = 0;
        m_rigidbody.velocity = Vector2.left * m_speed;
    }

    void FixedUpdate() {
        m_rigidbody.velocity.Normalize();
    }


    void OnCollisionExit2D(Collision2D collision) {
        var paddle = collision.collider.attachedRigidbody;

        if (paddle == null)
            return;

        if (paddle.velocity != Vector2.zero) {
            Vector2 vel = paddle.velocity + m_rigidbody.velocity;
            vel.Normalize();

            // Clip the ball's direction by clipping its y-speed
            //if (Mathf.Abs(vel.y) > 0.7f)
                //vel.y = Mathf.Sign(vel.y) * 0.5f;

            m_rigidbody.velocity = vel * m_speed;
        }
    }

    void OnTriggerEnter(Collider other) {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        GameManager.Players winner;
        if (m_rigidbody.velocity.x > 0f)
            winner = GameManager.Players.One;
        else
            winner = GameManager.Players.Two;

        Destroy(gameObject, 1.0f);

        
        GameManager.FinishGame(winner);
    }

}