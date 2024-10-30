using UnityEngine;
using UnityEngine.UI;

public class ScreenshotSaving : MonoBehaviour
{
    // Singleton instance to allow global access to this class
    public static ScreenshotSaving Instance { get; private set; }

    // Variables to hold screenshots for scenarios
    public Texture2D scenario1Screenshot;
    public Texture2D scenario2Screenshot;

    // Variables to hold reflection texts for scenarios
    public string scenario1Reflection;
    public string scenario2Reflection;

    // UI Text components for displaying reflections (if needed)
    public Text S1Reflection;
    public Text S2Reflection;

    // Ensures this object persists between scenes and manages singleton behavior
    private void Awake()
    {
        // If an instance already exists, destroy the duplicate
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Assign this instance and prevent it from being destroyed on scene load
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Method to save a screenshot for the chosen scenario (true for scenario 1, false for scenario 2)
    public void SaveScreenshot(Texture2D screenshot, bool scenario1Chosen)
    {
        if (scenario1Chosen)
        {
            scenario1Screenshot = screenshot;  // Save to scenario 1
        }
        else
        {
            scenario2Screenshot = screenshot;  // Save to scenario 2
        }
    }

    // Method to retrieve the saved screenshot for the chosen scenario
    public Texture2D GetScreenshot(bool scenario1Chosen)
    {
        // Return the appropriate screenshot based on scenario selection
        return scenario1Chosen ? scenario1Screenshot : scenario2Screenshot;
    }

    // Method to save the reflection text for the chosen scenario
    public void SaveReflection(string reflection, bool scenario1Chosen)
    {
        if (scenario1Chosen)
        {
            scenario1Reflection = reflection;  // Save to scenario 1
        }
        else
        {
            scenario2Reflection = reflection;  // Save to scenario 2
        }
    }

    // Method to retrieve the saved reflection text for the chosen scenario
    public string GetReflection(bool scenario1Chosen)
    {
        // Return the appropriate reflection based on scenario selection
        return scenario1Chosen ? scenario1Reflection : scenario2Reflection;
    }
}
