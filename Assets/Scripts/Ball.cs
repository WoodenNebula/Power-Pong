using UnityEngine;

public class Ball : MonoBehaviour, IResetAble {
    [HideInInspector] public static Ball Instance { get; set; }

    [SerializeField] public Animator Animator;

    [SerializeField] float m_speed = 10.0f;
    [SerializeField] AudioClip m_clash, m_ballOut;

    Rigidbody2D m_rigidbody;
    AudioSource m_audioSource;

    public Vector2 Velocity {
        get { return m_rigidbody.velocity; }
        set { m_rigidbody.velocity = value; }
    }


    void Awake() {
        m_audioSource = GetComponent<AudioSource>();
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_rigidbody.inertia = 0;
    }

    void Start() {
        if (Instance == null) { 
            Instance = this;
            GameManager.SubmitBall(Instance);
        }
        else { Destroy(gameObject); }

        Reset();
    }

    void FixedUpdate() {
        Velocity.Normalize();
    }


    void OnCollisionEnter2D(Collision2D collision) {
        // If collision is with walls
        if (collision.collider.attachedRigidbody == null) {
            m_audioSource.clip = m_clash;
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
        //Debug.Log("Current Angle: " + angle);
        // Clip the ball's direction by clipping its y-speed

        Velocity = vel * m_speed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        m_audioSource.clip = m_ballOut;
        m_audioSource.Play();

        GameManager.Players winner;
        if (Velocity.x > 0f)
            winner = GameManager.Players.One;
        else
            winner = GameManager.Players.Two;

        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        GameManager.FinishRound(winner);
    }

    public void Reset() {
        float y = Random.Range(-1.0f, 1.0f);
        int x = (y < 0.0f) ? 1 : -1;

        Vector2 vel = new Vector2(x, y);
        vel.Normalize();

        Velocity = vel * m_speed;

        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void NormalizeBallVelocity() {
        Velocity *= m_speed;
    }
}