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



    private void Start()
    {
        freezeGamePlay = true;
        GameCanvas.SetActive(false);//we want the canvas disabled when the how to play screen is on

        if (scenario1chosen)
        {

           
            Scenario1Description.SetActive(true);
            
        }
        else
        {

            
            Scenario2Description.SetActive(true);
            
        }

        UpdateButtonStates();
    }


    public void OpenHowToPlayScreen()
    {
       
            GameCanvas.SetActive(false);//we want the canvas disabled when the how to play screen is on
            ShowTutorialUI();
            freezeGamePlay = true;
            UpdateButtonStates();
    }

    public void CloseHowToPlayScreen()
    {
        GameCanvas.SetActive(true);//we want the canvas disabled when the how to play screen is on
        ShowTutorialUI();
        freezeGamePlay = false;
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


    private void ShowTutorialUI()
    {
        HowToPlayWalkThrough.SetActive(true);
        UpdateScreen(); // Initialize the screen update
    }


    public void CloseScenario()
    {
        Scenario1Description.SetActive(false);
        Scenario2Description.SetActive(false);
        GameCanvas.SetActive(true);//we want the canvas disabled when the how to play screen is on
        freezeGamePlay = false;

    }
}
