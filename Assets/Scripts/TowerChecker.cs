using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerChecker : MonoBehaviour
{
    public GameObject gameObjectComplete;
    public string stoneTag = "Stone";
    public int requiredStoneCount = 5;
    private bool towerIsComplete;
    private int stonesCollidingWithStones = 0;
    public GameObject CanvasObj;
    public GameObject gameOverCanvas;

    public Text countdownText;
    public AudioSource countdownAudio;
    public AudioClip countdownClip;

    public Camera screenshotCamera;
    private bool screenshotTaken = false;

    public Image scenario1DisplayEndOfLevel;
    public Image scenario2DisplayEndOfLevel;
    public GameObject S1stickyNote;
    public GameObject S2stickyNote;

    public GameObject S1CanvasPage;
    public GameObject S2CanvasPage;

    public TMP_InputField scenario1ReflectionInput; // Input field for scenario 1 reflection
    public TMP_InputField scenario2ReflectionInput; // Input field for scenario 2 reflection

    private bool countdownActive = false;
    private bool gameOver = false;  // New variable to track game over state

    private void Start()
    {
        // Defaulting to false at the very start to clear the screen elements
        S1stickyNote.SetActive(false);
        S2stickyNote.SetActive(false);
        scenario1DisplayEndOfLevel.gameObject.SetActive(false);
        scenario2DisplayEndOfLevel.gameObject.SetActive(false);

        // Activate the screen for the chosen scenario after clearing the screen of previous active elements
        if (LevelController.scenario1chosen)
        {
            scenario1DisplayEndOfLevel.gameObject.SetActive(true);
            scenario2DisplayEndOfLevel.gameObject.SetActive(false);
            S1stickyNote.SetActive(true);
            S2stickyNote.SetActive(false);
        }
        else
        {
            scenario1DisplayEndOfLevel.gameObject.SetActive(false);
            scenario2DisplayEndOfLevel.gameObject.SetActive(true);
            S1stickyNote.SetActive(false);
            S2stickyNote.SetActive(true);
        }
    }

    void Update()
    {
        if (!gameOver) // Only check tower completion if the game is not over
        {
            CheckTowerCompletion();
        }
    }

    void CheckTowerCompletion()
    {
        GameObject[] stones = GameObject.FindGameObjectsWithTag(stoneTag);
        Debug.Log("Found " + stones.Length + " stones with tag '" + stoneTag + "' in the scene.");

        stonesCollidingWithStones = 0;
        bool allNonKinematic = AllStonesNonKinematicAndCheckCollisions(stones);

        if (allNonKinematic && stonesCollidingWithStones == requiredStoneCount)
        {
            if (!countdownActive)
            {
                Debug.Log("Tower is complete! StonesCollidingWithStones: " + stonesCollidingWithStones);
                towerIsComplete = true;
                StartCoroutine(WaitForStability());
            }
        }
        else
        {
            towerIsComplete = false;
            Debug.Log("Tower is not complete. StonesCollidingWithStones: " + stonesCollidingWithStones);

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
                return false;
            }

            StoneCollisionDetector tracker = stone.GetComponent<StoneCollisionDetector>();
            if (tracker == null)
            {
                Debug.LogWarning("No StoneCollisionDetector component found on stone: " + stone.name);
                continue;
            }

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

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            float countdownTime = 5f;

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
                yield return null;

                if (!towerIsComplete || gameOver) // Check if game over or tower is no longer complete
                {
                    countdownText.gameObject.SetActive(false);
                    countdownActive = false;
                    yield break;
                }
            }

            countdownText.text = "0";
            countdownText.gameObject.SetActive(false);

            if (towerIsComplete && !screenshotTaken && !gameOver)
            {
                gameObjectComplete.SetActive(true);
                TakeTheScreenshot();
                SavePlayerReflections();  // Save the player's reflections for both scenarios
                DisplayScreenshot(); // Display the screenshot after it's taken
                GameOver();
            }
        }
        else
        {
            Debug.LogError("CountdownText is not assigned.");
        }

        countdownActive = false;
    }

    void TakeTheScreenshot()
    {
        RenderTexture rt = new RenderTexture(720, 720, 24);
        screenshotCamera.targetTexture = rt;
        screenshotCamera.Render();
        RenderTexture.active = rt;

        Texture2D screenShot = new Texture2D(720, 720, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, 720, 720), 0, 0);
        screenShot.Apply();

        screenshotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        ScreenshotSaving.Instance.SaveScreenshot(screenShot, LevelController.scenario1chosen);

        screenshotTaken = true;
    }

    void DisplayScreenshot()
    {
        // Get the saved screenshot
        Texture2D screenshot = ScreenshotSaving.Instance.GetScreenshot(LevelController.scenario1chosen);

        if (screenshot != null)
        {
            // Convert screenshot to a Sprite
            Sprite screenshotSprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));

            // Assign the sprite to the appropriate Image component based on the scenario
            if (LevelController.scenario1chosen)
            {
                scenario1DisplayEndOfLevel.sprite = screenshotSprite;
            }
            else
            {
                scenario2DisplayEndOfLevel.sprite = screenshotSprite;
            }
        }
        else
        {
            Debug.LogError("Screenshot is null. Unable to create sprite.");
        }
    }

    // New function to save player's reflections for both scenarios
    void SavePlayerReflections()
    {
        if (scenario1ReflectionInput != null && scenario2ReflectionInput != null)
        {
            if (LevelController.scenario1chosen)
            {
                string scenario1Reflection = scenario1ReflectionInput.text;
                ScreenshotSaving.Instance.SaveReflection(scenario1Reflection, true);
            }
            else
            {
                string scenario2Reflection = scenario2ReflectionInput.text;
                ScreenshotSaving.Instance.SaveReflection(scenario2Reflection, false);
            }
        }
        else
        {
            Debug.LogError("Reflection InputFields are not assigned.");
        }
    }



    // New function to handle game over state
    public void GameOver()
    {
        gameOver = true; // Set game over state
        StopAllCoroutines(); // Stop all coroutines
        countdownText.gameObject.SetActive(false); // Hide the countdown text

        // Used to turn off the game's UI depending on what scenario was selected...
        if (LevelController.scenario1chosen)
        {
            S1CanvasPage.SetActive(false);
            countdownText.gameObject.SetActive(false);
            countdownActive = false;
            CanvasObj.SetActive(false);
        }
        else
        {
            S2CanvasPage.SetActive(false);
            countdownText.gameObject.SetActive(false);
            countdownActive = false;
            CanvasObj.SetActive(false);
        }
    }
}
