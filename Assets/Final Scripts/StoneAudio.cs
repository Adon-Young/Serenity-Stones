using UnityEngine;

public class StoneAudio : MonoBehaviour
{
    // Singleton instance to ensure only one StoneAudio exists
    public static StoneAudio Instance { get; private set; }

    // AudioSource to play collision sounds
    public AudioSource collisionSound;

    private void Awake()
    {
        // Implement Singleton pattern to ensure only one instance of StoneAudio exists
        if (Instance == null)
        {
            Instance = this; // Set the current instance as the Singleton instance
            // Removed DontDestroyOnLoad as it's not needed for this use case
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Method to play the collision sound
    public void PlayCollisionSound()
    {
        // Check if the collisionSound AudioSource and its clip are set
        if (collisionSound != null && collisionSound.clip != null)
        {
            collisionSound.Play(); // Play the collision sound
        }
    }
}
