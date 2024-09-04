using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public static bool scenario1chosen; // Determines which scenario to show
    public GameObject scenario1Screen; // UI for Scenario 1
    public GameObject scenario2Screen; // UI for Scenario 2
    public static bool freezeGamePlay; // Controls whether gameplay is frozen
   
    // HowToPlayWalkthrough will hold all the screens for the how to play information
    public GameObject HowToPlayWalkThrough;
    public GameObject[] howToPlayScreens;
    private int currentScreenIndex = 0; // Fixed typo

    // Buttons for navigation
    public Button nextButton;
    public Button previousButton;
    public Button startGameButton; // Disables the How to Play walkthrough screen and unfreezes the gameplay
    public GameObject GameCanvas;

    public GameObject Scenario1Description;
    public GameObject Scenario2Description;


    private void Awake()
    {
   
        freezeGamePlay = true;
    }

    private void Start()
    {
        // Check if the player has already seen the HowToPlay walkthrough
        if (PlayerPrefs.GetInt("HasSeenHowToPlayInfo", 0) == 0)
        {
            GameCanvas.SetActive(false);//we want the canvas disabled when the how to play screen is on
            ShowTutorialUI();
            // Set the flag so we don't show it again
            PlayerPrefs.SetInt("HasSeenHowToPlayInfo", 1);
            PlayerPrefs.Save(); // Save the changes
            freezeGamePlay = true;

        }
        else
        {
            GameCanvas.SetActive(true);//reactivate if player has already seen the how to play screen
            // If already seen, hide the HowToPlayWalkThrough
            HowToPlayWalkThrough.SetActive(false);


            freezeGamePlay = false;
        }

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
        if (currentScreenIndex < howToPlayScreens.Length - 1)
        {
            currentScreenIndex++;
            UpdateScreen();
        }
    }

    public void PreviousButton()
    {
        // Move to the previous screen if not at the start of the list
        if (currentScreenIndex > 0)
        {
            currentScreenIndex--;
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
            howToPlayScreens[currentScreenIndex].SetActive(true);
        }

        // Update button states based on the current index
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        if (howToPlayScreens.Length > 0)
        {
            // Disable the "Previous" button on the first screen
            previousButton.interactable = currentScreenIndex > 0;

            // Disable the "Next" button on the last screen
            nextButton.interactable = currentScreenIndex < howToPlayScreens.Length - 1;

            // Show the "Start Game" button only on the last screen
            startGameButton.gameObject.SetActive(currentScreenIndex == howToPlayScreens.Length - 1);
        }
    }

    // Attached to start button to unfreeze the gameplay
    public void Play()
    {
        freezeGamePlay = false;
        GameCanvas.SetActive(true);//reactivate if player has already seen the how to play screen
        // Disable the HowToPlayWalkThrough and all its children
        HowToPlayWalkThrough.gameObject.SetActive(false);
    }

    private void ShowTutorialUI()
    {
        HowToPlayWalkThrough.SetActive(true);
        UpdateScreen(); // Initialize the screen update
    }
}
