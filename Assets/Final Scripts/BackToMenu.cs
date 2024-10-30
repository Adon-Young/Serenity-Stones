using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Function called when the 'Back to Menu' button is clicked
    public void BackToMenuButton()
    {
        // Loads the Main Menu scene
        // SceneManager handles scene loading, transitioning us back to "MainMenu"
        SceneManager.LoadScene("MainMenu");

        // Make sure the "MainMenu" scene is added in the Build Settings, 
        // otherwise this won't work
    }
}