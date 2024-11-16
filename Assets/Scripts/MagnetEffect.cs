using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the magnet effect on the player.
/// </summary>
public class MagnetEffect : MonoBehaviour
{
    [SerializeField] private float magnetRange = 5f; // Range of the magnet effect
    [SerializeField] private float magnetDuration = 5f; // Duration of the magnet effect
    [SerializeField] private float pullSpeed = 10f; // Speed at which coins are pulled
    private bool isMagnetActive = false; // Tracks whether the magnet is active

    /// <summary>
    /// Activates the magnet effect for a set duration.
    /// </summary>
    public void ActivateMagnet()
    {
        if (!isMagnetActive)
        {
            Debug.Log("MagnetEffect: Magnet activated.");
            StartCoroutine(MagnetCoroutine());
        }
    }

    /// <summary>
    /// Coroutine to handle the magnet effect duration.
    /// </summary>
    private IEnumerator MagnetCoroutine()
    {
        isMagnetActive = true;
        yield return new WaitForSeconds(magnetDuration);
        isMagnetActive = false;
        Debug.Log("MagnetEffect: Magnet deactivated.");
    }

    private void Update()
    {
        if (isMagnetActive)
        {
            PullCoins();
        }
    }

    /// <summary>
    /// Pulls nearby coins within the magnet range towards the player.
    /// </summary>
    private void PullCoins()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, magnetRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Coin"))
            {
                Debug.Log($"MagnetEffect: Pulling coin at {collider.transform.position}.");
                collider.transform.position = Vector3.MoveTowards(
                    collider.transform.position,
                    transform.position,
                    pullSpeed * Time.deltaTime
                );
            }
        }
    }
}
