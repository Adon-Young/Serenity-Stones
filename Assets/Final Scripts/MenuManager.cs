using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // GameObjects representing different screens in the menu
    public GameObject startScreen;
    public GameObject howToPlayScreen;
    public GameObject themeScreen;
    public GameObject scenarioScreen;
    public GameObject creditsScreen;

    // UI elements for scenario displays and reflection texts
    public Image scenario1DisplayMainMenu;
    public Image scenario2DisplayMainMenu;
    public TextMeshProUGUI scenario1ReflectionText;
    public TextMeshProUGUI scenario2ReflectionText;

    void Start()
    {
        // If there's an instance of ScreenshotSaving, display screenshots and reflections
        if (ScreenshotSaving.Instance != null)
        {
            DisplayScreenshots();  // Display stored screenshots for scenarios
            DisplayReflections();  // Display reflections for each scenario
        }

        // Start by showing the main screen
        ShowStartScreen();
    }

    // Load and display screenshots for the main menu from ScreenshotSaving
    private void DisplayScreenshots()
    {
        // Get the screenshots for scenario 1 and scenario 2
        Texture2D scenario1Screenshot = ScreenshotSaving.Instance.GetScreenshot(true);
        Texture2D scenario2Screenshot = ScreenshotSaving.Instance.GetScreenshot(false);

        // If a screenshot for scenario 1 exists, convert it to a sprite and display it
        if (scenario1Screenshot != null)
        {
            scenario1DisplayMainMenu.sprite = Sprite.Create(scenario1Screenshot, new Rect(0, 0, scenario1Screenshot.width, scenario1Screenshot.height), new Vector2(0.5f, 0.5f));
        }

        // If a screenshot for scenario 2 exists, convert it to a sprite and display it
        if (scenario2Screenshot != null)
        {
            scenario2DisplayMainMenu.sprite = Sprite.Create(scenario2Screenshot, new Rect(0, 0, scenario2Screenshot.width, scenario2Screenshot.height), new Vector2(0.5f, 0.5f));
        }
    }

    // Display reflections (text) for each scenario in the menu
    private void DisplayReflections()
    {
        // Get the reflection texts for scenario 1 and scenario 2
        string scenario1Reflection = ScreenshotSaving.Instance.GetReflection(true);
        string scenario2Reflection = ScreenshotSaving.Instance.GetReflection(false);

        // If a reflection for scenario 1 exists, display it; otherwise, show "..." as a placeholder
        scenario1ReflectionText.text = !string.IsNullOrEmpty(scenario1Reflection) ? scenario1Reflection : "...";

        // If a reflection for scenario 2 exists, display it; otherwise, show "..." as a placeholder
        scenario2ReflectionText.text = !string.IsNullOrEmpty(scenario2Reflection) ? scenario2Reflection : "...";
    }

    // Show the main start screen and hide the rest
    public void ShowStartScreen()
    {
        HideAllScreens();
        startScreen.SetActive(true);  // Activate the start screen
    }

    // Show the scenario selection screen and hide the rest
    public void ShowScenarioScreen()
    {
        HideAllScreens();
        scenarioScreen.SetActive(true);  // Activate the scenario screen
    }

    // Show the How to Play screen and hide the rest
    public void ShowHowToPlayScreen()
    {
        HideAllScreens();
        howToPlayScreen.SetActive(true);  // Activate the how-to-play screen
    }

    // Show the theme selection screen and hide the rest
    public void ShowThemeScreen()
    {
        HideAllScreens();
        themeScreen.SetActive(true);  // Activate the theme screen
    }

    // Show the credits screen and hide the rest
    public void ShowCreditsScreen()
    {
        HideAllScreens();
        creditsScreen.SetActive(true);  // Activate the credits screen
    }

    // Hide all screens by deactivating each one
    private void HideAllScreens()
    {
        startScreen.SetActive(false);    // Deactivate start screen
        howToPlayScreen.SetActive(false); // Deactivate how-to-play screen
        themeScreen.SetActive(false);    // Deactivate theme screen
        scenarioScreen.SetActive(false); // Deactivate scenario screen
        creditsScreen.SetActive(false);  // Deactivate credits screen
    }
}
