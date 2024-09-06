using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHowTo : MonoBehaviour
{
  
    public GameObject[] howToPlayScreens;
    private int currentScreenIndex = 0;
    // Buttons for navigation
    public Button nextButton;
    public Button previousButton;
 

    // Buttons for moving through the how to play screen at the start of the level


    public void Start()
    {
        previousButton.interactable = currentScreenIndex > 0;

    }
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

            // Note: The conditions for showing the "Start Game" button or any other UI element
            // should be handled elsewhere, as your example doesn't include it.
        }
    }

    public void ResetHowToPlayScreen()
    {
        // Ensure the screens array is not empty
        if (howToPlayScreens.Length > 0)
        {
            // Set all screens to inactive
            foreach (GameObject screen in howToPlayScreens)
            {
                screen.SetActive(false);
            }

            // Activate the first screen
            howToPlayScreens[0].SetActive(true);

            // Reset the current screen index to the start
            currentScreenIndex = 0;

            // Update button states to reflect the reset
            UpdateButtonStates();
        }
    }

}
