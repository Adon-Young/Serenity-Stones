using UnityEngine;

public class ThemeEnabler : MonoBehaviour
{
    // References to your content GameObjects
    public GameObject content1;
    public GameObject content2;
    public GameObject content3;
    public GameObject content4;

    private void Start()
    {
        // Call the function to set up the game screen based on the selected index
        SetupGameScreen();
    }

    private void SetupGameScreen()
    {
        // Disable all contents first
        content1.SetActive(false);
        content2.SetActive(false);
        content3.SetActive(false);
        content4.SetActive(false);

        // Enable the content based on the selected button index
        switch (ThemeSelection.selectedButtonIndex)
        {
            case 0:
                content1.SetActive(true);
                break;
            case 1:
                content2.SetActive(true);
                break;
            case 2:
                content3.SetActive(true);
                break;
            case 3:
                content4.SetActive(true);
                break;
            default:
                Debug.LogError("Invalid button index selected. Index: " + ThemeSelection.selectedButtonIndex);
                break;
        }
    }
}
