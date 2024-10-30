using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemeSelection : MonoBehaviour
{
    // Static variable to store the index of the currently selected button.
    public static int selectedButtonIndex = 0;

    // This method is called when a button is selected.
    // It updates the selectedButtonIndex with the index of the clicked button.
    public void OnButtonSelected(int index)
    {
        // Set the static variable to the index of the selected button.
        selectedButtonIndex = index;

        // Additional functionality can be added here, such as loading a scene or updating UI elements
        // based on the selected index.
    }
}
