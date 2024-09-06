using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToRace : MonoBehaviour
{
    public Image fadingToBlackImage;
    public GameObject imageHolder;
    private float timeOfFade = 1.0f;
    public GameObject menuAudio;
    public GameObject driftAudio;
    public GameObject EarthTrack, MarsTrack,SpaceTrack;
    public GameObject LoadingScreen;
    private AudioSource mainMenuAudioClip;
    private AudioSource menuDriftAudio;
    
    

    public void Start()
    {
        mainMenuAudioClip = menuAudio.GetComponent<AudioSource>();//getting /assigning the audio source (not clip)
        menuDriftAudio = driftAudio.GetComponent<AudioSource>();
        LoadingScreen.SetActive(false);
    }

    public void ChooseTrack()
    {
        if(EarthTrack.activeInHierarchy)
        {
            EarthTrack.SetActive(false);
            MarsTrack.SetActive(true);
            SpaceTrack.SetActive(false);
        }
        else if(MarsTrack.activeInHierarchy)
        {
            MarsTrack.SetActive(false);
            EarthTrack.SetActive(false);
            SpaceTrack.SetActive(true);
        }

        else if (SpaceTrack.activeInHierarchy)
        {
            MarsTrack.SetActive(false);
            EarthTrack.SetActive(true);
            SpaceTrack.SetActive(false);
        }
    }

    public void ChooseTrackReverse()
    {
        if (EarthTrack.activeInHierarchy)
        {
            SpaceTrack.SetActive(true);
            EarthTrack.SetActive(false);
            MarsTrack.SetActive(false);
        }
        else if (MarsTrack.activeInHierarchy)
        {
            SpaceTrack.SetActive(false);
            MarsTrack.SetActive(false);
            EarthTrack.SetActive(true);
        }
        else if (SpaceTrack.activeInHierarchy)
        {
            SpaceTrack.SetActive(false);
            MarsTrack.SetActive(true);
            EarthTrack.SetActive(false);
        }
    }

    public void TestTrack()
    {
        imageHolder.SetActive(true);
        fadingToBlackImage.enabled = true;
        StartCoroutine(FadingToBlack());
    }

  
    IEnumerator FadingToBlack()//fading out
    {
        
        float elapsedTime = 0f;
        Color imageColour = fadingToBlackImage.color;
        float menuMusicVolume = mainMenuAudioClip.volume;
        float driftVolume = menuDriftAudio.volume;
        //while the pre set time is greater than elapse time itll change the alpha value on the image, increasing it to black
        while (elapsedTime < timeOfFade)
        {
            elapsedTime += Time.deltaTime;
            imageColour.a = Mathf.Lerp(0f, 1f, elapsedTime / timeOfFade);
            //updating the image with the new alpha value
            fadingToBlackImage.color = imageColour;
            mainMenuAudioClip.volume = Mathf.Lerp(menuMusicVolume, 0f, elapsedTime / timeOfFade);
            menuDriftAudio.volume = Mathf.Lerp(driftVolume, 0f, elapsedTime / timeOfFade);
            yield return null;

            if (elapsedTime >= timeOfFade)
            {
                LoadingScreen.SetActive(true);
            }
        }


     
    }


    
    IEnumerator FadingToExit()//fading out
    {

        float elapsedTime = 0f;
        Color imageColour = fadingToBlackImage.color;
        float menuMusicVolume = mainMenuAudioClip.volume;
        float driftVolume = menuDriftAudio.volume;
        //while the pre set time is greater than elapse time itll change the alpha value on the image, increasing it to black
        while (elapsedTime < timeOfFade)
        {
            elapsedTime += Time.deltaTime;
            imageColour.a = Mathf.Lerp(0f, 1f, elapsedTime / timeOfFade);
            //updating the image with the new alpha value
            fadingToBlackImage.color = imageColour;
            mainMenuAudioClip.volume = Mathf.Lerp(menuMusicVolume, 0f, elapsedTime / timeOfFade);
            menuDriftAudio.volume = Mathf.Lerp(driftVolume, 0f, elapsedTime / timeOfFade);
            yield return null;

            if (elapsedTime >= timeOfFade)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }


      
    }


    public void LoadingScreenToTrack()
    {
        if (EarthTrack.activeInHierarchy)
        {
            SceneManager.LoadScene("EarthTrack");
        }
        else if (MarsTrack.activeInHierarchy)
        {
            SceneManager.LoadScene("MarsTrack");
        }

        else if (SpaceTrack.activeInHierarchy)
        {
            SceneManager.LoadScene("SpaceTrack");
        }

        //making sure the cursor is off at the start of each race...
        Cursor.visible = false;//turn it invisable
        Cursor.lockState = CursorLockMode.Locked;//lock its position


    }



   public void ExitButton()
    {
        imageHolder.SetActive(true);
        fadingToBlackImage.enabled = true;
        StartCoroutine(FadingToExit());
    }




}
