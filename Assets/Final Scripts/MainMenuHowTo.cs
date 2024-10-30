using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHowTo : MonoBehaviour
{
    // Array to hold all the How to Play screens (slides of instructions)
    public GameObject[] howToPlayScreens;
    private int currentScreenIndex = 0; // Keeps track of the current screen we're on

    // Buttons for navigation (Next and Previous)
    public Button nextButton;
    public Button previousButton;

    // Start is called before the first frame update
    public void Start()
    {
        // Ensure the "Previous" button is interactable based on the current index (should be disabled at start)
        previousButton.interactable = currentScreenIndex > 0;
    }

    // Called when the "Next" button is clicked
    public void NextButton()
    {
        // Move to the next screen if we're not already on the last one
        if (currentScreenIndex < howToPlayScreens.Length - 1)
        {
            currentScreenIndex++; // Increment the screen index
            UpdateScreen(); // Update the screen visuals
        }
    }

    // Called when the "Previous" button is clicked
    public void PreviousButton()
    {
        // Move to the previous screen if we're not already on the first one
        if (currentScreenIndex > 0)
        {
            currentScreenIndex--; // Decrement the screen index
            UpdateScreen(); // Update the screen visuals
        }
    }

    // Updates the current screen based on the index
    private void UpdateScreen()
    {
        // Loop through all screens and disable them
        foreach (GameObject screen in howToPlayScreens)
        {
            screen.SetActive(false); // Deactivate all screens
        }

        // Activate the screen at the current index
        if (howToPlayScreens.Length > 0)
        {
            howToPlayScreens[currentScreenIndex].SetActive(true);
        }

        // Update button states (whether Next/Previous should be enabled)
        UpdateButtonStates();
    }

    // Updates the states of navigation buttons (Next and Previous)
    private void UpdateButtonStates()
    {
        if (howToPlayScreens.Length > 0)
        {
            // Disable the "Previous" button if we're on the first screen
            previousButton.interactable = currentScreenIndex > 0;

            // Disable the "Next" button if we're on the last screen
            nextButton.interactable = currentScreenIndex < howToPlayScreens.Length - 1;

            // Note: Any additional UI elements like a "Start Game" button could be handled elsewhere
        }
    }

    // Resets the How to Play screen to the beginning
    public void ResetHowToPlayScreen()
    {
        // Make sure the array is not empty before resetting
        if (howToPlayScreens.Length > 0)
        {
            // Deactivate all the screens
            foreach (GameObject screen in howToPlayScreens)
            {
                screen.SetActive(false); // Turn off all screens
            }

            // Activate the first screen in the array
            howToPlayScreens[0].SetActive(true);

            // Reset the screen index to the first one
            currentScreenIndex = 0;

            // Update button states after resetting
            UpdateButtonStates();
        }
    }
}