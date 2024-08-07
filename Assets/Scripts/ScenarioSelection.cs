using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenarioSelection : MonoBehaviour
{
    public Button scenario1Button;
    public Button scenario2Button;
    private static LevelController levelController;
    private void Start()
    {
        // Add listeners to buttons
        scenario1Button.onClick.AddListener(OnScenario1Selected);
        scenario2Button.onClick.AddListener(OnScenario2Selected);
    }


    //this should turn the bool on/ off which is fine for now as we only play to have 2 levels
    //if we want more level then we will use intigers in an array or list with a switch case
    private void OnScenario1Selected()
    {
        
        SceneManager.LoadScene("SampleScene"); // Load the game scene
        levelController.scenario1chosen = true;

    }

    private void OnScenario2Selected()
    {
        
        SceneManager.LoadScene("SampleScene"); // Load the game scene
        levelController.scenario1chosen = false;
    }
}
