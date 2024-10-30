using System;
using System.Reflection;
using UnityEngine;

public class ThemeEnabler : MonoBehaviour
{
    // References to the content GameObjects for different themes.
    public GameObject content1;
    public GameObject content2;
    public GameObject content3;
    public GameObject content4;

    // Array of materials for stones, corresponding to different themes.
    public Material[] stoneMaterials;

    // Array of GameObjects representing speech bubble themes.
    public GameObject[] SpeechBubbleThemes;

    private void Start()
    {
        // Initialize the game screen setup based on the selected theme.
        SetupGameScreen();
    }

    private void SetupGameScreen()
    {
        // Disable all content GameObjects initially.
        content1.SetActive(false);
        content2.SetActive(false);
        content3.SetActive(false);
        content4.SetActive(false);

        // Enable the appropriate content GameObject and set related materials and themes
        // based on the selected button index from ThemeSelection.
        switch (ThemeSelection.selectedButtonIndex)
        {
            case 0:
                // Enable content1 and set its associated materials and speech bubble theme.
                content1.SetActive(true);
                SetStoneMaterial(0);
                EnableSpeechBubbleTheme(0);
                break;
            case 1:
                // Enable content2 and set its associated materials and speech bubble theme.
                content2.SetActive(true);
                SetStoneMaterial(1);
                EnableSpeechBubbleTheme(1);
                break;
            case 2:
                // Enable content3 and set its associated materials and speech bubble theme.
                content3.SetActive(true);
                SetStoneMaterial(2);
                EnableSpeechBubbleTheme(2);
                break;
            case 3:
                // Enable content4 and set its associated materials and speech bubble theme.
                content4.SetActive(true);
                SetStoneMaterial(3);
                EnableSpeechBubbleTheme(3);
                break;
            default:
                // Optionally handle unexpected index values or errors here.
                break;
        }
    }

    private void SetStoneMaterial(int index)
    {
        // Update the stone materials based on the selected index.
        if (index >= 0 && index < stoneMaterials.Length)
        {
            // Find the StoneCreation component in the scene.
            StoneCreation stoneCreation = FindObjectOfType<StoneCreation>();
            if (stoneCreation != null)
            {
                // Update the stone materials for the StoneCreation component.
                stoneCreation.UpdateStoneMaterials(stoneMaterials[index]);
            }
        }
    }

    private void EnableSpeechBubbleTheme(int index)
    {
        // Enable the speech bubble theme based on the selected index.
        if (SpeechBubbleThemes != null && index >= 0 && index < SpeechBubbleThemes.Length)
        {
            // Deactivate all speech bubble themes first (if needed) and then activate the selected one.
            for (int i = 0; i < SpeechBubbleThemes.Length; i++)
            {
                SpeechBubbleThemes[i].SetActive(i == index);
            }
        }
    }
}
