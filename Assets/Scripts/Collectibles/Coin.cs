using UnityEngine;

/// <summary>
/// Coin that notifies observers when collected.
/// Implements Subject in the Observer Pattern.
/// </summary>
public class Coin : Subject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin: Player collected a coin.");
            Notify(NotificationTypes.Collect); // Notify observers of the collection
            Destroy(gameObject); // Remove the coin from the scene
        }
    }
}
