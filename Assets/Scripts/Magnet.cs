using UnityEngine;

/// <summary>
/// Represents a magnet collectible.
/// </summary>
public class Magnet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Magnet: Collided with {other.name} (Tag: {other.tag})."); // Çarpışan nesne ve Tag bilgisi
        if (other.CompareTag("Player"))
        {
            Debug.Log("Magnet: Collected by the player.");
            MagnetEffect magnetEffect = other.GetComponent<MagnetEffect>();
            if (magnetEffect != null)
            {
                Debug.Log("Magnet: MagnetEffect component found on Player.");
                magnetEffect.ActivateMagnet();
            }
            else
            {
                Debug.LogError("Magnet: Player does not have a MagnetEffect component!");
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Magnet: Ignored collision with object.");
        }
    }

}
