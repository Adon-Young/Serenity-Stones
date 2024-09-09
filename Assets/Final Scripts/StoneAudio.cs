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
            // Removed DontDestroyOnLoad as it's not needed
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
     
    }
}