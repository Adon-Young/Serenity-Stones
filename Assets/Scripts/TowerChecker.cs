using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Import this to use UI elements

public class TowerChecker : MonoBehaviour
{
    public GameObject gameObjectComplete;  // Reference to the GameObject to set active
    public string stoneTag = "Stone";      // Tag assigned to all stone GameObjects
    public int requiredStoneCount = 5;     // Number of stones required for completion
    private bool towerIsComplete;
    private int stonesCollidingWithStones = 0;  // Counter for stones colliding with other stones

    public Text countdownText; // Reference to the UI Text component
    public AudioSource countdownAudio; // Reference to the AudioSource component
    public AudioClip countdownClip; // Audio clip to play during countdown

    private bool countdownActive = false;

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
            if (!countdownActive)
            {
                Debug.Log("Tower is complete! StonesCollidingWithStones: " + stonesCollidingWithStones);
                towerIsComplete = true;
                StartCoroutine(WaitForStability()); // Start stability check with countdown
            }
        }
        else
        {
            towerIsComplete = false;
            Debug.Log("Tower is not complete. StonesCollidingWithStones: " + stonesCollidingWithStones);

            // Ensure countdown is stopped and text is hidden
            if (countdownActive)
            {
                StopCoroutine(WaitForStability());
                countdownText.gameObject.SetActive(false);
                countdownActive = false;
            }
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
        countdownActive = true;

        // Ensure the countdown text is initialized and visible
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            float countdownTime = 5f; // Countdown duration in seconds

            // Play countdown audio
            if (countdownAudio != null && countdownClip != null)
            {
                countdownAudio.clip = countdownClip;
                countdownAudio.Play();
            }
            else
            {
                Debug.LogWarning("Countdown AudioSource or AudioClip is not assigned.");
            }

            while (countdownTime > 0)
            {
                countdownText.text = Mathf.Ceil(countdownTime).ToString();
                countdownTime -= Time.deltaTime;
                yield return null; // Wait for the next frame

                // If stones fall during the countdown, stop the countdown
                if (!towerIsComplete)
                {
                    countdownText.gameObject.SetActive(false);
                    countdownActive = false;
                    yield break; // Exit the coroutine
                }
            }

            countdownText.text = "0"; // Ensure the text shows 0 at the end
            countdownText.gameObject.SetActive(false); // Hide text when done

            // After the countdown, check if the tower is still complete
            if (towerIsComplete)
            {
                gameObjectComplete.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("CountdownText is not assigned.");
        }

        countdownActive = false;
    }
}
