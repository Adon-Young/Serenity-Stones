using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}