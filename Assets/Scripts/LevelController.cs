using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public bool scenario1chosen; // Determines which scenario to show
    public GameObject scenario1Screen; // UI for Scenario 1
    public GameObject scenario2Screen; // UI for Scenario 2
    public static bool freezeGamePlay; // Controls whether gameplay is frozen

    // HowToPlayWalkthrough will hold all the screens for the how to play information
    public GameObject HowToPlayWalkThrough;
    public GameObject[] howToPlayScreens;
    private int currnetScreenIndex = 0;

    // Buttons for navigation
    public Button nextButton;
    public Button previousButton;
    public Button startGameButton; // Disables the How to Play walkthrough screen and unfreezes the gameplay

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        scenario1chosen = true;
        freezeGamePlay = true;
    }

    private void Start()
    {
        HowToPlayWalkThrough.SetActive(true); // This is the how to play overlay screen

        // Initialize button states
        UpdateButtonStates();
    }

    private void Update()
    {
        // Update the scenario screens based on the chosen scenario
        if (scenario1chosen)
        {
            scenario1Screen.SetActive(true);
            scenario2Screen.SetActive(false);
        }
        else
        {
            scenario1Screen.SetActive(false);
            scenario2Screen.SetActive(true);
        }
    }

    // Buttons for moving through the how to play screen at the start of the level
    public void NextButton()
    {
        // Move to the next screen if not at the end of the list
        if (currnetScreenIndex < howToPlayScreens.Length - 1)
        {
            currnetScreenIndex++;
            UpdateScreen();
        }
    }

    public void PreviousButton()
    {
        // Move to the previous screen if not at the start of the list
        if (currnetScreenIndex > 0)
        {
            currnetScreenIndex--;
            UpdateScreen();
        }
    }

    private void UpdateScreen()
    {
        foreach (GameObject screen in howToPlayScreens)
        {
            screen.SetActive(false);
        }

        // Show the current screen
        if (howToPlayScreens.Length > 0)
        {
            howToPlayScreens[currnetScreenIndex].SetActive(true);
        }

        // Update button states based on the current index
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        if (howToPlayScreens.Length > 0)
        {
            // Disable the "Previous" button on the first screen
            previousButton.interactable = currnetScreenIndex > 0;

            // Disable the "Next" button and show the "Start Game" button on the last screen
            if (currnetScreenIndex == howToPlayScreens.Length - 1)
            {
                nextButton.interactable = false;
                startGameButton.gameObject.SetActive(true); // Show Start Game button
            }
            else
            {
                nextButton.interactable = true;
                startGameButton.gameObject.SetActive(false); // Hide Start Game button
            }
        }
    }

    // Attached to start button to unfreeze the gameplay
    public void UnfreezeLevel()
    {
        freezeGamePlay = false;

        // Disable the HowToPlayWalkThrough and all its children
        HowToPlayWalkThrough.gameObject.SetActive(false);
    }
}
