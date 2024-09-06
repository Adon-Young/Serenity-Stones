using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Method to reset the tutorial PlayerPrefs
    public void ResetHowToPlayTutorial()
    {
        PlayerPrefs.DeleteKey("HasSeenHowToPlayInfo");
        PlayerPrefs.Save();
        Debug.Log("HowToPlay tutorial has been reset from the Main Menu.");
    }

    // Method to be called when the Reset button is pressed
    public void OnResetButtonPressed()
    {
        ResetHowToPlayTutorial();
        // Optionally, provide feedback or transition to another scene
    }
}