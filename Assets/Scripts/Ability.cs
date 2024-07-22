using UnityEngine;

public interface IAbility {
    public int RemainingUsage { get; set; }
    public float Cooldown { get; set; }

    public void Use();
    public void Reset();
}

public class WarpBall : IAbility {
    // Max Usage per Round not per game
    readonly int m_maxUsage;
    readonly float m_maxCooldown;

    Player m_user;

    public int RemainingUsage { get; set; }
    public float Cooldown { get; set; }


    public WarpBall(Player abilityUser) {
        m_maxUsage = 2;
        m_maxCooldown = 10.0f;

        Cooldown = 0.0f;
        RemainingUsage = m_maxUsage;

        m_user = abilityUser;
    }

    public void Use() {
        Ball ball = Ball.Instance;

        Debug.Log("Trying to use warp Ability");
        Debug.Log("UsageRemaining: " + RemainingUsage + "Cooldown: " + Cooldown);
        if (RemainingUsage <= 0 || Cooldown > 0.3f)
            return;

        //float playerField = 0.0f;
        Vector3 abilityUserPos = m_user.transform.position;

        Vector3 ballPos = ball.transform.position;

        switch (m_user.PlayerID) {
            case GameManager.Players.One:
                if (/*ballPos.x > playerField || */ball.Velocity.x < 0.0f)
                    break;

                Debug.Log("Warpped by Player Two");
                // Play Ball warp out
                ballPos.y = abilityUserPos.y;
                ball.transform.position = ballPos;
                
                ball.Velocity = Vector2.right;
                ball.NormalizeBallVelocity();
                // Play Ball warp in
                RemainingUsage--;
                break;

            case GameManager.Players.Two:
                if (/*ballPos.x < playerField || */ball.Velocity.x > 0.0f)
                    break;

                Debug.Log("Warpped by Player Two");
                // Play Ball warp out
                ballPos.y = abilityUserPos.y;
                
                ball.transform.position = ballPos;

                ball.Velocity = Vector2.left;
                ball.NormalizeBallVelocity();
                // Play Ball warp in
                RemainingUsage--;
                break;
        }
    }

    public void Reset() {
        RemainingUsage = m_maxUsage;
        Cooldown = m_maxCooldown;
    }
}