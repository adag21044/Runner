using UnityEngine;

public class JumpingState : IPlayerState
{
    public void EnterState(PlayerStateMachine player)
    {
        player.Animator.SetTrigger("Jumped");
        player.Rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        player.IsGrounded = false;
        Debug.Log("PlayerStateMachine: Entered JumpingState");
    }

    public void UpdateState(PlayerStateMachine player)
    {
        if (player.IsGrounded)
        {
            player.ChangeState(new RunningState());
        }
    }

    public void OnCollisionEnter(PlayerStateMachine player, Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            player.IsGrounded = true;
        }
    }
}
