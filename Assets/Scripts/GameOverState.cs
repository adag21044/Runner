using UnityEngine;

public class GameOverState : IPlayerState
{
    public void EnterState(PlayerStateMachine player)
    {
        Debug.Log("PlayerStateMachine: Entered GameOverState. All actions stopped.");
    }

    public void UpdateState(PlayerStateMachine player)
    {
        // No updates in GameOverState
    }

    public void OnCollisionEnter(PlayerStateMachine player, Collision collision)
    {
        // No collision handling in GameOverState
    }
}
