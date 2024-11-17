using UnityEngine;

/// <summary>
/// Handles player's collection behavior.
/// Observes collectible objects.
/// </summary>
public class PlayerCollection : Observer
{
    [SerializeField] private ScoreManager scoreManager; // Reference to the score manager
    //[SerializeField] private ParticleSystem collectionEffect; // Optional collection effect

    private void Awake()
    {
        if (scoreManager == null)
        {
            Debug.LogError("PlayerCollection: ScoreManager reference is missing!");
        }
    }

    public override void OnNotify(NotificationTypes type)
    {
        if (type == NotificationTypes.Collect)
        {
            Debug.Log("PlayerCollection: Coin collected!");
            scoreManager.AddScore(1); // Add score for coin collection

            // Play collection effect if available
            /*if (collectionEffect != null)
            {
                collectionEffect.Play();
            }*/
        }
    }
}
