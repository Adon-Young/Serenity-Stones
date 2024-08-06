using UnityEngine;

public class ResetStone : MonoBehaviour
{
    public StoneCreation stoneCreation; // Reference to the StoneCreation script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            GameObject stone = other.gameObject;

            if (stone == null)
            {
                Debug.LogWarning("Collided object is null.");
                return;
            }

            // Check if the stoneCreation reference is assigned
            if (stoneCreation != null)
            {
                stoneCreation.ResetStone(stone);
            }
            else
            {
                Debug.LogError("StoneCreation reference is not assigned.");
            }
        }
    }
}