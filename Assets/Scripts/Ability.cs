using UnityEngine;

public interface IAbility: IResetAble {
    public int RemainingUsage { get; set; }
    public float Cooldown { get; set; }

    public void Use();
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
        if (RemainingUsage <= 0 || Cooldown > 0.3f) {
            PlayerHUD.UpdateAbilityDisplay(m_user.PlayerID, 0);
            return;
        }

        Vector3 abilityUserPos = m_user.transform.position;

        Vector3 ballPos = ball.transform.position;

        switch (m_user.PlayerID) {
            case GameManager.Players.One:
                if (ball.Velocity.x < 0.0f)
                    break;

                // Play Ball warp out
                ballPos.y = abilityUserPos.y;
                ball.transform.position = ballPos;

                ball.Velocity = Vector2.right;
                ball.NormalizeBallVelocity();
                // Play Ball warp in
                RemainingUsage--;
                break;

            case GameManager.Players.Two:
                if (ball.Velocity.x > 0.0f)
                    break;

                // Play Ball warp out
                ballPos.y = abilityUserPos.y;

                ball.transform.position = ballPos;

                ball.Velocity = Vector2.left;
                ball.NormalizeBallVelocity();
                // Play Ball warp in
                RemainingUsage--;
                break;
        }
        PlayerHUD.UpdateAbilityDisplay(m_user.PlayerID, RemainingUsage);
    }

    public void Reset() {
        RemainingUsage = m_maxUsage;
        Cooldown = m_maxCooldown;
    }
}