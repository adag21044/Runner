using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the magnet effect on the player.
/// </summary>
public class MagnetEffect : MonoBehaviour
{
    private bool isMagnetActive = false; // Magnet etkisinin aktif olup olmadığını takip eder
    private float magnetRange;
    private float pullSpeed = 10f; // Coinlerin çekilme hızı

    /// <summary>
    /// Magnet etkisini belirli bir süreyle etkinleştirir.
    /// </summary>
    public void ActivateMagnet(float range, float duration)
    {
        if (!isMagnetActive)
        {
            magnetRange = range;
            Debug.Log("MagnetEffect: Magnet activated.");
            StartCoroutine(MagnetCoroutine(duration));
        }
    }

    private IEnumerator MagnetCoroutine(float duration)
    {
        isMagnetActive = true;

        // Belirli süre boyunca magnet etkisi devam eder
        yield return new WaitForSeconds(duration);

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
    /// Yakındaki coinleri oyuncuya çeker.
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
