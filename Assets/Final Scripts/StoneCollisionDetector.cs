using UnityEngine;

public class StoneCollisionDetector : MonoBehaviour
{
    public bool IsCollidingWithStone { get; private set; } = false;
    public bool IsCollidingWithBase { get; private set; } = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (StoneAudio.Instance != null)
        {
            StoneAudio.Instance.PlayCollisionSound();
        }
        else
        {
            Debug.LogWarning("StoneAudio instance not found.");
        }

        if (collision.gameObject.CompareTag("Stone"))
        {
            IsCollidingWithStone = true;
            Debug.Log(gameObject.name + " started colliding with another stone: " + collision.gameObject.name);
        }
   
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            IsCollidingWithStone = false;
            Debug.Log(gameObject.name + " stopped colliding with another stone: " + collision.gameObject.name);
        }
    
    }
}
