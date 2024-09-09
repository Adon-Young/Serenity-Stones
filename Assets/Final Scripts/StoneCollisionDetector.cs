using UnityEngine;

public class StoneCollisionDetector : MonoBehaviour
{
    public bool IsCollidingWithStone { get; private set; } = false;
    public delegate void CollisionChange(bool isColliding);
    public event CollisionChange OnCollisionChange;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            IsCollidingWithStone = true;
            OnCollisionChange?.Invoke(true);  // Notify TowerChecker that a collision started
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            IsCollidingWithStone = false;
            OnCollisionChange?.Invoke(false);  // Notify TowerChecker that a collision ended
        }
    }

    // OnCollisionStay to update if stone is still colliding
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            IsCollidingWithStone = true;
        }
    }
}
