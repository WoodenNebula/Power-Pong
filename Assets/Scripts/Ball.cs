using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField] float m_speed = 10.0f;

    [HideInInspector] public static Ball Instance { get; set; }

    Rigidbody2D m_rigidbody;
    AudioSource m_audioSource;

    public Vector2 Velocity {
        get { return m_rigidbody.velocity; }
        set { m_rigidbody.velocity = value; }
    }


    void Awake() {
        if (Instance == null) { Instance = this; }
        //else { Destroy(gameObject); }

        m_audioSource = GetComponent<AudioSource>();

        m_rigidbody = GetComponent<Rigidbody2D>();
        m_rigidbody.inertia = 0;
        Velocity = Vector2.left * m_speed;
    }

    void FixedUpdate() {
        Velocity.Normalize();
    }


    void OnCollisionEnter2D(Collision2D collision) {
        // If collision is with walls
        if (collision.collider.attachedRigidbody == null) {
            m_audioSource.Play();
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        var paddle = collision.collider.attachedRigidbody;

        if (paddle == null)
            return;

        if (paddle.velocity.Equals(Vector2.zero)) {
            return;
        }


        Vector2 vel = paddle.velocity.normalized + Velocity.normalized;
        vel.Normalize();

        float angle = Mathf.Atan2(vel.y, vel.x);
        Debug.Log("Current Angle: " + angle);
        // Clip the ball's direction by clipping its y-speed

        Velocity = vel * m_speed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        GameManager.Players winner;
        if (Velocity.x > 0f)
            winner = GameManager.Players.One;
        else
            winner = GameManager.Players.Two;

        Destroy(gameObject, 1.0f);

        GameManager.FinishGame(winner);
    }


    public void NormalizeBallVelocity() {
        Velocity *= m_speed;
    }
}