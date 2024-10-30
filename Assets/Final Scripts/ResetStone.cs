//using UnityEngine;

//public class ResetStone : MonoBehaviour
//{
//    // Reference to the StoneCreation script for resetting stones
//    public StoneCreation stoneCreation;





//    private void OnTriggerEnter(Collider other)
//    {
//        // Check if the object entering the trigger is tagged as "Stone"
//        if (other.CompareTag("Stone"))
//        {
//            // Get the GameObject that triggered the collision
//            GameObject stone = other.gameObject;

//            // Check if the stone GameObject is not null
//            if (stone == null)
//            {
//                // If no stone, exit the method
//                return;
//            }

//            // Verify that the stoneCreation reference has been assigned
//            if (stoneCreation != null)
//            {
//                // Call the ResetStone method from StoneCreation to handle the reset logic
//                stoneCreation.ResetStone(stone);
//            }
//            else
//            {
//                // If stoneCreation is not assigned, log a warning for debugging purposes
//                Debug.LogWarning("StoneCreation reference is not assigned in ResetStone script.");
//            }
//        }

//    }
//}






//-------------------------------------------------------------------------------------------------------------------------TESTING



using UnityEngine;
using System.Collections;

public class ResetStone : MonoBehaviour
{
    // Reference to the StoneCreation script for resetting stones
    public StoneCreation stoneCreation;

    // Data collection reference
    public DataCollection dataCollectionScript;
    public Movement movementScriptForDataCollection;

    // Flag to track if the currently held stone has been reset
    private bool hasResetCurrentStone = false;

    // Delay duration for resetting logic
    public float resetDelay = 1.0f; // Adjust this value as needed
    private bool isResetting = false; // Flag to check if we're in the middle of a reset

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Stone"
        if (other.CompareTag("Stone"))
        {
            // Get the GameObject that triggered the collision
            GameObject stone = other.gameObject;

            // Check if the stone GameObject is not null
            if (stone == null)
            {
                // If no stone, exit the method
                return;
            }

            // Call the ResetStone method from StoneCreation to handle the reset logic
            if (stoneCreation != null)
            {
                stoneCreation.ResetStone(stone);
            }
            else
            {
                Debug.LogWarning("StoneCreation reference is not assigned in ResetStone script.");
            }

            // Check if the player is holding a stone
            if (movementScriptForDataCollection != null)
            {
                GameObject selectedStone = movementScriptForDataCollection.selectedStone; // Get the currently selected stone

                if (movementScriptForDataCollection.isDragging)
                {
                    // Player is holding a stone
                    if (selectedStone == stone)
                    {
                        // The stone being reset is the one the player is holding
                        if (!hasResetCurrentStone && !isResetting) // Check if it has already been reset and if we are not currently resetting
                        {
                            dataCollectionScript.StrategicResetCounter();
                            Debug.Log("Strategic Reset: Player is holding the selected stone.");
                            hasResetCurrentStone = true; // Set the flag to true
                            StartCoroutine(ResetDelay()); // Start the delay coroutine
                        }
                    }
                    else
                    {
                        // The stone being reset is not the one the player is holding
                        dataCollectionScript.AccidentalResetCounter();
                        Debug.Log("Accidental Reset: Player is holding a different stone.");
                    }
                }
                else
                {
                    // Player is not holding any stone
                    dataCollectionScript.AccidentalResetCounter();
                    Debug.Log("Accidental Reset: Player is not holding any stones.");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the hasResetCurrentStone flag when the player exits the reset area
        if (other.CompareTag("Stone"))
        {
            GameObject stone = other.gameObject;

            // Check if the stone is the selected stone
            if (movementScriptForDataCollection.selectedStone == stone)
            {
                hasResetCurrentStone = false; // Allow for a reset again when leaving the area
                Debug.Log("Exited Reset Area: Ready for new reset count.");
            }
        }
    }

    private IEnumerator ResetDelay()
    {
        isResetting = true; // Set the resetting flag to true
        yield return new WaitForSeconds(resetDelay); // Wait for the specified delay
        isResetting = false; // Reset the flag after the delay
    }
}
