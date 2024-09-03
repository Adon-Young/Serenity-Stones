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
    
    }



 

}
