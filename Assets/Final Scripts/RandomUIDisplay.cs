using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomUIDisplay : MonoBehaviour
{
    public float displayTime = 10f;       // Time to display each UI element
    public float minWaitTime = 15f;       // Minimum wait time before displaying the next UI element
    public float maxWaitTime = 25f;       // Maximum wait time before displaying the next UI element

    // Lists for UI elements specific to each scenario
    public List<GameObject> Scenario1UI;
    public List<GameObject> Scenario2UI;

    // UI elements and holders
    public GameObject backgroundBubble;
    public GameObject S1UIholder;
    public GameObject S2UIholder;

    public GameObject MadhurQuiet;
    public GameObject MadhurSpeaking;

    public GameObject OtherPersonQuiet;
    public GameObject OtherPersonSpeaking;

    void Start()
    {
        // Initialize UI elements and start the coroutine for displaying UI elements
        InitializeUI();
        StartCoroutine(WaitAtStart());
    }

    // Coroutine to wait for 10 seconds before starting the random UI display
    IEnumerator WaitAtStart()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(DisplayRandomUI());
    }

    void InitializeUI()
    {
        // Initially hide background bubble and both UI holders
        backgroundBubble.SetActive(false);
        S1UIholder.SetActive(false);
        S2UIholder.SetActive(false);

        // Show quiet images based on the selected scenario
        if (LevelController.scenario1chosen)
        {
            MadhurQuiet.SetActive(true);
            MadhurSpeaking.SetActive(false);
            OtherPersonQuiet.SetActive(false);
            OtherPersonSpeaking.SetActive(false);
        }
        else
        {
            OtherPersonQuiet.SetActive(true);
            OtherPersonSpeaking.SetActive(false);
            MadhurQuiet.SetActive(false);
            MadhurSpeaking.SetActive(false);
        }
    }

    // Coroutine to display random UI elements based on the scenario
    IEnumerator DisplayRandomUI()
    {
        while (true)
        {
            if (LevelController.scenario1chosen)
            {
                HandleScenario1();
                yield return StartCoroutine(DisplayUIElements(Scenario1UI, MadhurSpeaking, MadhurQuiet));
            }
            else
            {
                HandleScenario2();
                yield return StartCoroutine(DisplayUIElements(Scenario2UI, OtherPersonSpeaking, OtherPersonQuiet));
            }
        }
    }

    void HandleScenario1()
    {
        // Activate UI holder for Scenario 1 and deactivate Scenario 2 holder
        S1UIholder.SetActive(true);
        S2UIholder.SetActive(false);
    }

    void HandleScenario2()
    {
        // Activate UI holder for Scenario 2 and deactivate Scenario 1 holder
        S1UIholder.SetActive(false);
        S2UIholder.SetActive(true);
    }

    // Coroutine to display each UI element in the list with a randomized order
    IEnumerator DisplayUIElements(List<GameObject> uiElements, GameObject speakingImage, GameObject quietImage)
    {
        // Shuffle the list to randomize the display order
        ShuffleList(uiElements);

        foreach (GameObject uiElement in uiElements)
        {
            // Show the speaking image and hide the quiet image
            speakingImage.SetActive(true);
            quietImage.SetActive(false);
            backgroundBubble.SetActive(true);

            // Display the UI element
            uiElement.SetActive(true);
            yield return new WaitForSeconds(displayTime);

            // Hide the UI element and switch to the quiet image
            uiElement.SetActive(false);
            speakingImage.SetActive(false);
            quietImage.SetActive(true);
            backgroundBubble.SetActive(false);

            // Wait for a random time before displaying the next UI element
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    // Function to shuffle a list using the Fisher-Yates algorithm
    private void ShuffleList(List<GameObject> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
