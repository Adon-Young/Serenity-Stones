using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Public variables to hold references to the different screen panels
    public GameObject startScreen;
    public GameObject howToPlayScreen;
    public GameObject themeScreen;
    public GameObject scenarioScreen;

    // Method called at the start of the game
    void Start()
    {
        // Show the start screen when the game starts
        ShowStartScreen();
    }

    // Method to show the start screen
    public void ShowStartScreen()
    {
        // Hide all screens first
        HideAllScreens();
        // Then activate the start screen
        startScreen.SetActive(true);
    }
    public void ShowScenarioScreen()
    {
        HideAllScreens();

        scenarioScreen.SetActive(true);
    }

    // Method to show the "How To Play" screen
    public void ShowHowToPlayScreen()
    {
        // Hide all screens first
        HideAllScreens();
        // Then activate the "How To Play" screen
        howToPlayScreen.SetActive(true);
    }

    // Method to show the "Theme" screen
    public void ShowThemeScreen()
    {
        // Hide all screens first
        HideAllScreens();
        // Then activate the "Theme" screen
        themeScreen.SetActive(true);
    }

    // Method to hide all screens
    private void HideAllScreens()
    {
        // Deactivate the start screen
        startScreen.SetActive(false);
        // Deactivate the "How To Play" screen
        howToPlayScreen.SetActive(false);
        // Deactivate the "Theme" screen
        themeScreen.SetActive(false);
        scenarioScreen.SetActive(false);
    }

    // Method to load the main game scene
    public void LoadScenario1Scene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadScenario2Scene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}