using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenarioSelection : MonoBehaviour
{
    public Button scenario1Button;
    public Button scenario2Button;
    public LevelController levelController;
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
        levelController.scenario1chosen = true;
        SceneManager.LoadScene("GameScene"); // Load the game scene
    }

    private void OnScenario2Selected()
    {
        levelController.scenario1chosen = false;
        SceneManager.LoadScene("GameScene"); // Load the game scene
    }
}
