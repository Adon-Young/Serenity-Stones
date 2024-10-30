using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance; // Singleton instance to access LevelController easily

    // Variables to track which scenario is selected and control gameplay freeze
    public static bool scenario1chosen; // True if Scenario 1 is selected, false for Scenario 2
    public GameObject scenario1Screen; // UI screen for Scenario 1
    public GameObject scenario2Screen; // UI screen for Scenario 2
    public static bool freezeGamePlay; // If true, gameplay is paused/frozen

    // Walkthrough screens for the "How To Play" section
    public GameObject HowToPlayWalkThrough; // Parent object for How to Play screens
    public GameObject[] howToPlayScreens; // Array of How to Play screens
    private int currentScreenIndex = 0; // Index of the current walkthrough screen

    // UI Buttons for navigation and starting the game
    public Button nextButton; // Button to go to the next screen
    public Button previousButton; // Button to go to the previous screen
    public Button startGameButton; // Button to start the game and disable How to Play screen
    public GameObject GameCanvas; // Main game canvas, which will be hidden during How to Play

    // Descriptions for the scenarios (shown based on which scenario is chosen)
    public GameObject Scenario1Description;
    public GameObject Scenario2Description;

    private void Start()
    {
        freezeGamePlay = true; // Freeze gameplay at the start while the tutorial is up
        GameCanvas.SetActive(false); // Hide the game canvas until How to Play walkthrough is done

        // Show the correct scenario description based on the user's selection
        if (scenario1chosen)
        {
            Scenario1Description.SetActive(true); // Show Scenario 1's description
        }
        else
        {
            Scenario2Description.SetActive(true); // Show Scenario 2's description
        }

        UpdateButtonStates(); // Update button states (like disabling previous button if needed)
    }

    // Opens the How To Play screen and freezes gameplay
    public void OpenHowToPlayScreen()
    {
        GameCanvas.SetActive(false); // Hide the game canvas while How to Play is active
        ShowTutorialUI(); // Display the tutorial screens
        freezeGamePlay = true; // Freeze gameplay while in the tutorial
        UpdateButtonStates(); // Make sure buttons like "Next" and "Previous" are updated
    }

    // Closes the How To Play screen and unfreezes gameplay
    public void CloseHowToPlayScreen()
    {
        GameCanvas.SetActive(true); // Show the game canvas again
        ShowTutorialUI(); // Close the tutorial UI
        freezeGamePlay = false; // Unfreeze gameplay
        UpdateButtonStates(); // Update buttons based on the current tutorial screen
    }

    private void Update()
    {
        // Update the visibility of the scenario screens based on the chosen scenario
        if (scenario1chosen)
        {
            scenario1Screen.SetActive(true); // Show Scenario 1 screen
            scenario2Screen.SetActive(false); // Hide Scenario 2 screen
        }
        else
        {
            scenario1Screen.SetActive(false); // Hide Scenario 1 screen
            scenario2Screen.SetActive(true); // Show Scenario 2 screen
        }
    }

    // Advances to the next screen in the tutorial if there's one left
    public void NextButton()
    {
        if (currentScreenIndex < howToPlayScreens.Length - 1) // Check if we aren't on the last screen
        {
            currentScreenIndex++; // Move to the next screen
            UpdateScreen(); // Refresh the screen to show the new one
        }
    }

    // Goes back to the previous screen in the tutorial if not at the first screen
    public void PreviousButton()
    {
        if (currentScreenIndex > 0) // Check if we aren't on the first screen
        {
            currentScreenIndex--; // Move to the previous screen
            UpdateScreen(); // Refresh the screen to show the new one
        }
    }

    // Updates the visible screen in the How to Play tutorial
    private void UpdateScreen()
    {
        foreach (GameObject screen in howToPlayScreens) // Hide all tutorial screens first
        {
            screen.SetActive(false); // Hide the screen
        }

        if (howToPlayScreens.Length > 0) // Make sure we have screens to display
        {
            howToPlayScreens[currentScreenIndex].SetActive(true); // Show the current screen
        }

        UpdateButtonStates(); // Update button states after the screen change
    }

    // Updates the state of the navigation buttons based on the current screen index
    private void UpdateButtonStates()
    {
        if (howToPlayScreens.Length > 0)
        {
            // Disable the "Previous" button on the first screen
            previousButton.interactable = currentScreenIndex > 0;

            // Disable the "Next" button on the last screen
            nextButton.interactable = currentScreenIndex < howToPlayScreens.Length - 1;

            // Show the "Start Game" button only on the last tutorial screen
            startGameButton.gameObject.SetActive(currentScreenIndex == howToPlayScreens.Length - 1);
        }
    }

    // Shows the How to Play UI and prepares the first screen
    private void ShowTutorialUI()
    {
        HowToPlayWalkThrough.SetActive(true); // Enable the How to Play UI
        UpdateScreen(); // Refresh the screen so the first one shows
    }

    // Closes the scenario description and resumes gameplay
    public void CloseScenario()
    {
        Scenario1Description.SetActive(false); // Hide Scenario 1 description
        Scenario2Description.SetActive(false); // Hide Scenario 2 description
        GameCanvas.SetActive(true); // Show the game canvas again
        freezeGamePlay = false; // Unfreeze the game
    }
}
