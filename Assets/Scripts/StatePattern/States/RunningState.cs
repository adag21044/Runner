using UnityEngine;

public class RunningState : IPlayerState
{
    public void EnterState(PlayerStateMachine player)
    {
        player.Animator.SetTrigger("Running");
        Debug.Log("PlayerStateMachine: Entered RunningState");
    }

    public void UpdateState(PlayerStateMachine player)
    {
        player.MoveForward();
        player.MoveHorizontally();
    }

    public void OnCollisionEnter(PlayerStateMachine player, Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            player.ChangeState(new HitToHeadState());
        }
    }
}
