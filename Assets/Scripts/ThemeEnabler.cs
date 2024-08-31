using System;
using System.Reflection;
using UnityEngine;


public class ThemeEnabler : MonoBehaviour
{
    // References to your content GameObjects
    public GameObject content1;
    public GameObject content2;
    public GameObject content3;
    public GameObject content4;

    public Material[] skyboxes; // The array of skyboxes to choose from
    public Material[] stoneMaterials;
    public GameObject[] SpeechBubbleThemes;

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
                SetTheSkyBox(0); // Use the first skybox
                SetStoneMaterial(0);
                EnableSpeechBubbleTheme(0);


                break;
            case 1:
                content2.SetActive(true);
                SetTheSkyBox(1); // Use the second skybox
                SetStoneMaterial(1);
                EnableSpeechBubbleTheme(1);


                break;
            case 2:
                content3.SetActive(true);
                SetTheSkyBox(2); // Use the third skybox
                SetStoneMaterial(2);
                EnableSpeechBubbleTheme(2);


                break;
            case 3:
                content4.SetActive(true);
                SetTheSkyBox(3); // Use the fourth skybox
                SetStoneMaterial(3);
                EnableSpeechBubbleTheme(3);


                break;
            default:
                Debug.LogError("Invalid button index selected. Index: " + ThemeSelection.selectedButtonIndex);
                break;
        }
    }

    private void SetTheSkyBox(int index)
    {
        // Setting the skyboxes to use the same index variable as the buttons for the themes
        if (index >= 0 && index < skyboxes.Length)
        {
            RenderSettings.skybox = skyboxes[index];
            Debug.Log("Skybox selected: " + index); // Corrected debug log
            DynamicGI.UpdateEnvironment(); // Updates the lighting to match skybox for rendering purposes
        }
        else
        {
            Debug.LogError("Invalid skybox index: " + index);
        }
    }

    private void SetStoneMaterial(int index)
    {

        if (index >= 0 && index < stoneMaterials.Length)
        {
            StoneCreation stoneCreation = FindObjectOfType<StoneCreation>();
            if(stoneCreation != null)
            {
                stoneCreation.UpdateStoneMaterials(stoneMaterials[index]);
            }
            else
            {
                Debug.Log("stone creation script not found");
            }
        }

    }


    private void EnableSpeechBubbleTheme(int index)
    {
        if (SpeechBubbleThemes != null && index >= 0 && index < SpeechBubbleThemes.Length)
        {
            SpeechBubbleThemes[index].SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid theme index or themeObjects array is not set up correctly.");
        }
    }


}
