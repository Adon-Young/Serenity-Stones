using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomUIDisplay : MonoBehaviour
{
    public float displayTime = 10f;       // Time to display each UI element
    public float minWaitTime = 15f;       // Minimum wait time before displaying the next UI element
    public float maxWaitTime = 25f;       // Maximum wait time before displaying the next UI element

    // Lists for each scenario
    public List<GameObject> Scenario1UI;
    public List<GameObject> Scenario2UI;

    public GameObject S1UIholder;
    public GameObject S2UIholder;

    public GameObject MadhurQuiet;
    public GameObject MadhurSpeaking;

    public GameObject OtherPersonQuiet;
    public GameObject OtherPersonSpeaking;

    void Start()
    {
        InitializeUI();
        StartCoroutine(WaitAtStart());


    }


    IEnumerator WaitAtStart()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(DisplayRandomUI());
    }









    void InitializeUI()
    {
        // Set both UI holders to inactive at the start
        S1UIholder.SetActive(false);
        S2UIholder.SetActive(false);

        // Show quiet images initially based on the scenario
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
        // Activate holder and set UI elements for Scenario 1
        S1UIholder.SetActive(true);
        S2UIholder.SetActive(false);
    }

    void HandleScenario2()
    {
        // Activate holder and set UI elements for Scenario 2
        S1UIholder.SetActive(false);
        S2UIholder.SetActive(true);
    }

    IEnumerator DisplayUIElements(List<GameObject> uiElements, GameObject speakingImage, GameObject quietImage)
    {
        ShuffleList(uiElements);

        foreach (GameObject uiElement in uiElements)
        {
            // Show the speaking image and hide the quiet image
            speakingImage.SetActive(true);
            quietImage.SetActive(false);

            // Display the UI element (speech bubble)
            uiElement.SetActive(true);
            yield return new WaitForSeconds(displayTime);

            // Hide the UI element (speech bubble)
            uiElement.SetActive(false);

            // Hide the speaking image and show the quiet image
            speakingImage.SetActive(false);
            quietImage.SetActive(true);

            // Wait for a random time between minWaitTime and maxWaitTime
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    // Function to shuffle a list using Fisher-Yates algorithm
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
