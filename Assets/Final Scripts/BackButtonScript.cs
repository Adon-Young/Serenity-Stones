using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    // Function that gets triggered when the back button is pressed
    // This will handle switching the scene back to the main menu
    public void GoToMainMenu()
    {
        // Load the "MainMenu" scene
        // SceneManager is a built-in Unity class used for managing scene transitions
        SceneManager.LoadScene("MainMenu");
        // The scene name must match exactly what's in the build settings, so double-check the spelling!
    }
}