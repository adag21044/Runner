using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; // Singleton instance
    public static MusicManager Instance => instance;

    [SerializeField] private AudioSource backgroundMusic; // Background music audio source
    [SerializeField] private float musicVolume = 0.5f; // Music volume

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Keep the music manager between scenes
    }

    private void Start()
    {
        if(backgroundMusic != null)
        {
            backgroundMusic.loop = true; // Loop the background music
            backgroundMusic.volume = musicVolume; // Set the music volume
            backgroundMusic.Play(); // Play the background music
        }
        else
        {
            Debug.LogWarning("MusicManager: Background music reference is missing!");
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume); // Clamp the volume between 0 and 1
        
        if(backgroundMusic != null) backgroundMusic.volume = musicVolume; // Set the music volume
    }

    public void StopMusic()
    {
        if(backgroundMusic != null) backgroundMusic.Stop(); // Stop the background music
    }

    public void ResumeMusic()
    {
        if(backgroundMusic != null) backgroundMusic.Play(); // Resume the background music
    }
}
