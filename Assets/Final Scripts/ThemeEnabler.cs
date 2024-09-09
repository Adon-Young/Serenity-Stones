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
            
                SetStoneMaterial(0);
                EnableSpeechBubbleTheme(0);


                break;
            case 1:
                content2.SetActive(true);
           
                SetStoneMaterial(1);
                EnableSpeechBubbleTheme(1);


                break;
            case 2:
                content3.SetActive(true);
              
                SetStoneMaterial(2);
                EnableSpeechBubbleTheme(2);


                break;
            case 3:
                content4.SetActive(true);
             
                SetStoneMaterial(3);
                EnableSpeechBubbleTheme(3);


                break;
            default:
              
                break;
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
         
        }

    }


    private void EnableSpeechBubbleTheme(int index)
    {
        if (SpeechBubbleThemes != null && index >= 0 && index < SpeechBubbleThemes.Length)
        {
            SpeechBubbleThemes[index].SetActive(true);
        }
     
    }


}
