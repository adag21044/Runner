using UnityEngine;

/// <summary>
/// Handles player's collection behavior.
/// Observes collectible objects.
/// </summary>
public class PlayerCollection : Observer
{
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ScoreManager scoreManager; // Reference to the score manager
    //[SerializeField] private ParticleSystem collectionEffect; // Optional collection effect

    private void Awake()
    {
        if (scoreManager == null)
        {
            Debug.LogError("PlayerCollection: ScoreManager reference is missing!");
        }

        if (audioSource == null)
        {
             // Ses kaynağı arka plan müziğini etkilemesin diye yeni bir AudioSource oluşturuluyor
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false; // Koleksiyon sesi loop olmamalı
            audioSource.playOnAwake = false; // Oyun başladığında çalmamalı
        }
    }

    public override void OnNotify(NotificationTypes type)
    {
        if (type == NotificationTypes.Collect)
        {
            Debug.Log("PlayerCollection: Coin collected!");
            scoreManager.AddScore(1); // Add score for coin collection

            PlayCollectSound();
        }
    }

    private void PlayCollectSound()
    {
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound); // PlayOneShot ile mevcut arka plan müziğini etkilemeden çalar
        }
    }
}
