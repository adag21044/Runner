using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the magnet effect on the player.
/// </summary>
public class MagnetEffect : MonoBehaviour
{
    private bool isMagnetActive = false; // Magnet etkisinin aktif olup olmadığını takip eder
    private float magnetRange;
    private float pullSpeed = 20f; // Coinlerin çekilme hızı
    private float remainingTime; // Geriye kalan süreyi takip eder

    /// <summary>
    /// Magnet etkisini belirli bir süreyle etkinleştirir.
    /// </summary>
    public void ActivateMagnet(float range, float duration)
    {
        if (!isMagnetActive)
        {
            magnetRange = range;
            remainingTime = duration;
            Debug.Log($"MagnetEffect: Magnet activated for {duration} seconds.");
            StartCoroutine(MagnetCoroutine(duration));
        }
    }

    private IEnumerator MagnetCoroutine(float duration)
    {
        isMagnetActive = true;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; // Süreyi azalt
            yield return null; // Bir sonraki frame'i bekle
        }

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
