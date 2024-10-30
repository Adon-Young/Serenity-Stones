using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerChecker : MonoBehaviour
{
    public GameObject gameObjectComplete; // Object to activate when the tower is complete
    public string stoneTag = "Stone"; // Tag used to identify stone objects
    public int requiredStoneCount = 5; // Number of stones required to complete the tower
    private bool towerIsComplete; // Tracks whether the tower is complete
    private int stonesCollidingWithStones = 0; // Number of stones currently colliding with other stones
    public GameObject CanvasObj; // Main canvas object
    public GameObject gameOverCanvas; // Canvas displayed when the game is over

    public Text countdownText; // Text component for displaying countdown
    public AudioSource countdownAudio; // Audio source for countdown sound
    public AudioClip countdownClip; // Audio clip for countdown sound

    public Camera screenshotCamera; // Camera used to take screenshots
    private bool screenshotTaken = false; // Tracks whether a screenshot has been taken

    public Image scenario1DisplayEndOfLevel; // Image to display for scenario 1 at the end of the level
    public Image scenario2DisplayEndOfLevel; // Image to display for scenario 2 at the end of the level
    public GameObject S1stickyNote; // Sticky note for scenario 1
    public GameObject S2stickyNote; // Sticky note for scenario 2

    public GameObject S1CanvasPage; // Canvas page for scenario 1
    public GameObject S2CanvasPage; // Canvas page for scenario 2

    public TMP_InputField scenario1ReflectionInput; // Input field for scenario 1 reflection
    public TMP_InputField scenario2ReflectionInput; // Input field for scenario 2 reflection
    public TextMeshProUGUI stonesCountText1; // Text for displaying stone count in scenario 1
    public TextMeshProUGUI stonesCountText2; // Text for displaying stone count in scenario 2
    private bool countdownActive = false; // Tracks whether the countdown is active

    public bool gameOver = false; // Tracks whether the game is over

    //-----------------------------------------------TESTING

     public DataCollection dataCollectionReference;

    //-----------------------------------------------TESTING



    private void Start()
    {
        stonesCollidingWithStones = 0;
        // Initialize UI elements
        S1stickyNote.SetActive(false);
        S2stickyNote.SetActive(false);
        scenario1DisplayEndOfLevel.gameObject.SetActive(false);
        scenario2DisplayEndOfLevel.gameObject.SetActive(false);

        // Activate UI elements based on selected scenario
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
            stonesCountText1.text = "" + stonesCollidingWithStones + "/5";
            stonesCountText2.text = "" + stonesCollidingWithStones + "/5";
            CheckTowerCompletion();
        }
    }

    void CheckTowerCompletion()
    {
        GameObject[] stones = GameObject.FindGameObjectsWithTag(stoneTag);

        stonesCollidingWithStones = 0;
        bool allNonKinematic = AllStonesNonKinematicAndCheckCollisions(stones);

        if (allNonKinematic && stonesCollidingWithStones == requiredStoneCount)
        {
            if (!countdownActive)
            {
                towerIsComplete = true;
                StartCoroutine(WaitForStability());
            }
        }
        else
        {
            towerIsComplete = false;

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
            if (rb == null || rb.isKinematic)
            {
                return false; // Return false if any stone is missing a Rigidbody or is kinematic
            }

            StoneCollisionDetector tracker = stone.GetComponent<StoneCollisionDetector>();
            if (tracker != null && tracker.IsCollidingWithStone)
            {
                stonesCollidingWithStones++;
            }
        }

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

            while (countdownTime > 0)
            {
                countdownText.text = Mathf.Ceil(countdownTime).ToString();
                countdownTime -= Time.deltaTime;
                yield return null;

                if (!towerIsComplete || gameOver) // Check if game over or tower is no longer complete
                {
                    countdownText.gameObject.SetActive(false);
                    countdownActive = false;

                    // Stop the countdown audio if it's playing
                    if (countdownAudio != null && countdownAudio.isPlaying)
                    {
                        countdownAudio.Stop();
                    }

                    yield break;
                }
            }

            countdownText.text = "0";
            countdownText.gameObject.SetActive(false);

            if (towerIsComplete && !screenshotTaken && !gameOver)
            {
                gameObjectComplete.SetActive(true);
                TakeTheScreenshot();

                DisplayScreenshot(); // Display the screenshot after it's taken
                GameOver(); // End the game
            }
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
    }

    // Function to save player's reflections for both scenarios
    public void SavePlayerReflections()
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
    }

    // Function to handle game over state
    public void GameOver()
    {
        gameOver = true; // Set game over state
        StopAllCoroutines(); // Stop all coroutines
        countdownText.gameObject.SetActive(false); // Hide the countdown text
        LevelController.freezeGamePlay = true; // Freeze gameplay

        // Hide the UI based on selected scenario
        if (LevelController.scenario1chosen)
        {
            S1CanvasPage.SetActive(false);
            CanvasObj.SetActive(false);
        }
        else
        {
            S2CanvasPage.SetActive(false);
            CanvasObj.SetActive(false);
        }



        //-----------------------------------------------TESTING

        dataCollectionReference.StopTimer();

        //-----------------------------------------------TESTING



    }
}
