using UnityEngine;

public class StoneCollisionDetector : MonoBehaviour
{
    // Indicates whether this object is currently colliding with a "Stone"
    public bool IsCollidingWithStone { get; private set; } = false;

    // Delegate type for handling collision changes
    public delegate void CollisionChange(bool isColliding);

    // Event triggered when the collision state changes
    public event CollisionChange OnCollisionChange;

    // Called when this object starts colliding with another object
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is tagged as "Stone"
        if (collision.gameObject.CompareTag("Stone"))
        {
            IsCollidingWithStone = true; // Update collision state
            OnCollisionChange?.Invoke(true); // Notify listeners of collision start
        }
    }

    // Called when this object stops colliding with another object
    void OnCollisionExit(Collision collision)
    {
        // Check if the exited object was tagged as "Stone"
        if (collision.gameObject.CompareTag("Stone"))
        {
            IsCollidingWithStone = false; // Update collision state
            OnCollisionChange?.Invoke(false); // Notify listeners of collision end
        }
    }

    // Called while this object is continuously colliding with another object
    void OnCollisionStay(Collision collision)
    {
        // Check if the colliding object is tagged as "Stone"
        if (collision.gameObject.CompareTag("Stone"))
        {
            IsCollidingWithStone = true; // Update collision state to true while still colliding
        }
    }
}
