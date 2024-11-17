using UnityEngine;

/// <summary>
/// Represents a magnet collectible.
/// </summary>
public class Magnet : MonoBehaviour
{
    [SerializeField] private float magnetRange = 5f; // Küresel çekim alanı
    [SerializeField] private float magnetDuration = 5f; // Magnet etkisinin süresi

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Magnet: Collected by the player.");

            MagnetEffect magnetEffect = other.GetComponent<MagnetEffect>();
            if (magnetEffect != null)
            {
                magnetEffect.ActivateMagnet(magnetRange, magnetDuration); // Magnet etkisini etkinleştir
            }
            else
            {
                Debug.LogError("Magnet: Player does not have a MagnetEffect component!");
            }

            Destroy(gameObject); // Magnet sahneden kaldırılır
        }
    }
}
