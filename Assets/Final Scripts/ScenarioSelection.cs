using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioSelection : MonoBehaviour
{

    private void Start()
    {
        // You no longer need to find or use an instance of LevelController here.
        LevelController.scenario1chosen = true; // Set default scenario if needed
    }

    public void OnScenario1Selected()
    {
        // Access the static member directly through the class name
        LevelController.scenario1chosen = true;
        SceneManager.LoadScene("SampleScene"); // Load the game scene
    }

    public void OnScenario2Selected()
    {
        // Access the static member directly through the class name
        LevelController.scenario1chosen = false;
        SceneManager.LoadScene("SampleScene"); // Load the game scene
    }
}
