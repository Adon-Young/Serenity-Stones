using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioSelection : MonoBehaviour
{
    private void Start()
    {
        // Initialize the default scenario setting if needed
        // Set default scenario to true, meaning Scenario 1 is chosen initially
        LevelController.scenario1chosen = true;
    }

    public void OnScenario1Selected()
    {
        // When Scenario 1 is selected, set the static scenario1chosen flag to true
        LevelController.scenario1chosen = true;

        // Load the game scene; replace "SampleScene" with your actual game scene name
        SceneManager.LoadScene("SampleScene");
    }

    public void OnScenario2Selected()
    {
        // When Scenario 2 is selected, set the static scenario1chosen flag to false
        LevelController.scenario1chosen = false;

        // Load the game scene; replace "SampleScene" with your actual game scene name
        SceneManager.LoadScene("SampleScene");
    }
}