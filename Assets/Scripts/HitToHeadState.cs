using UnityEngine;

public class HitToHeadState : IPlayerState
{
    public void EnterState(PlayerStateMachine player)
    {
        player.Animator.SetTrigger("HitToWall");
        Debug.Log("PlayerStateMachine: Entered HitToHeadState");
        player.ForwardSpeed = 0f;
        player.HorizontalSpeed = 0f;

        Debug.Log("Game Over: Stopping player.");
    }

    public void UpdateState(PlayerStateMachine player)
    {
        var stateInfo = player.Animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Hit To Head") && stateInfo.normalizedTime >= 1.0f)
        {
            player.Animator.speed = 0f; // Freeze all animations
            Debug.Log("HitToHeadState: Animation finished. Animator frozen.");
            player.ChangeState(new GameOverState()); // Move to GameOverState
        }
    }

    public void OnCollisionEnter(PlayerStateMachine player, Collision collision)
    {
        // No collision handling in this state
    }
}
