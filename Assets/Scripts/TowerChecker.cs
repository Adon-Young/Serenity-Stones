using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerChecker : MonoBehaviour
{
    public GameObject gameObjectComplete;  // Reference to the GameObject to set active
    public string stoneTag = "Stone";      // Tag assigned to all stone GameObjects
    public int requiredStoneCount = 5;     // Number of stones required for completion
    private bool towerIsComplete;
    private int stonesCollidingWithStones = 0;  // Counter for stones colliding with other stones

    void Update()
    {
        CheckTowerCompletion();
    }

    void CheckTowerCompletion()
    {
        // Find all GameObjects with the stone tag
        GameObject[] stones = GameObject.FindGameObjectsWithTag(stoneTag);
        Debug.Log("Found " + stones.Length + " stones with tag '" + stoneTag + "' in the scene.");

        // Reset collision counter
        stonesCollidingWithStones = 0;

        // Check if all stones are non-kinematic and calculate collision counts
        bool allNonKinematic = AllStonesNonKinematicAndCheckCollisions(stones);

        // Check if conditions for completing the tower are met
        if (allNonKinematic && stonesCollidingWithStones == requiredStoneCount)
        {
            
            Debug.Log("Tower is complete! StonesCollidingWithStones: " + stonesCollidingWithStones);

            towerIsComplete = true;
            StartCoroutine(WaitForStability());//waiting for the tower to become stable

        }
        else
        {
            towerIsComplete = false;
            
            Debug.Log("Tower is not complete. StonesCollidingWithStones: " + stonesCollidingWithStones);
        }
    }

    bool AllStonesNonKinematicAndCheckCollisions(GameObject[] stones)
    {
        foreach (GameObject stone in stones)
        {
            Rigidbody rb = stone.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogWarning("No Rigidbody found on stone: " + stone.name);
                return false;
            }

            if (rb.isKinematic)
            {
                Debug.Log("Stone " + stone.name + " is kinematic.");
                return false; // Return false if any stone is kinematic
            }

            StoneCollisionDetector tracker = stone.GetComponent<StoneCollisionDetector>();
            if (tracker == null)
            {
                Debug.LogWarning("No StoneCollisionDetector component found on stone: " + stone.name);
                continue;
            }

            // Count the stones colliding with another stone
            if (tracker.IsCollidingWithStone)
            {
                stonesCollidingWithStones++;
                Debug.Log(stone.name + " is colliding with another stone.");
            }
        }

        Debug.Log("All stones are non-kinematic.");
        return true;
    }



  private IEnumerator WaitForStability()
    {
        yield return new WaitForSeconds(5);

        if (towerIsComplete == true)
        {
            //so after the 5 seconds if the tower is still true then perform following logic...
            gameObjectComplete.SetActive(true);
        }
        else
        {

        }



    }
}
