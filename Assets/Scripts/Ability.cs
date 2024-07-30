using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface IAbility : IResetAble {
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

    private void WarpBallTo(float targetY, Vector2 targetDir) {
        Ball ball = Ball.Instance;

        Ball.Instance.Animator.SetTrigger("Warp");

        Vector3 ballPos = ball.transform.position;
        ballPos.y = targetY;
        ball.transform.position = ballPos;

        ball.Velocity = targetDir;
        ball.NormalizeBallVelocity();

        RemainingUsage--;
    }

    public void Use() {
        if (RemainingUsage <= 0 || Cooldown > 0.3f) {
            PlayerHUD.UpdateAbilityDisplay(m_user.PlayerID, 0);
            return;
        }

        float yPosUser = m_user.transform.position.y;
        float xVelBall = Ball.Instance.Velocity.x;

        switch (m_user.PlayerID) {
            case GameManager.Players.One:

                if (xVelBall > 0.0f) {
                    WarpBallTo(yPosUser, Vector2.right);
                }
                break;

            case GameManager.Players.Two:
                if (xVelBall < 0.0f) {
                    WarpBallTo(yPosUser, Vector2.left);
                }
                break;
        }
        PlayerHUD.UpdateAbilityDisplay(m_user.PlayerID, RemainingUsage);
    }

    public void Reset() {
        RemainingUsage = m_maxUsage;
        Cooldown = m_maxCooldown;
    }
}