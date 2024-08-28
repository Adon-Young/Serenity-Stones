using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemeSelection : MonoBehaviour
{
    // Static variable to store the selected button index
    public static int selectedButtonIndex = -1;

    // Button click handler
    public void OnButtonSelected(int index)
    {
        // Set the selected button index
        selectedButtonIndex = index;

    }
}
