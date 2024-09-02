using UnityEngine;

public class StoneAudio : MonoBehaviour
{
    public static StoneAudio Instance { get; private set; }
    public AudioSource collisionSound;

    private void Awake()
    {
        // Implement Singleton pattern to ensure only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayCollisionSound()
    {
        if (collisionSound != null && collisionSound.clip != null)
        {
            collisionSound.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing on the StoneAudio.");
        }
    }
}
