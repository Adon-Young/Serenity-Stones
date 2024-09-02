using UnityEngine;

public class StoneCollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Debugging message
        Debug.Log($"Collision detected with: {collision.gameObject.name}");

        // Call the method to play sound from the CollisionAudioManager
        if (StoneAudio.Instance != null)
        {
            StoneAudio.Instance.PlayCollisionSound();
        }
        else
        {
            Debug.LogWarning("CollisionAudioManager instance not found.");
        }
    }
}