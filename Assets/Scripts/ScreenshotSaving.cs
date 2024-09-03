using UnityEngine;

public class ScreenshotSaving : MonoBehaviour
{
    public static ScreenshotSaving Instance { get; private set; }

    public Texture2D scenario1Screenshot;
    public Texture2D scenario2Screenshot;

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveScreenshot(Texture2D screenshot, bool scenario1Chosen)
    {
        if (scenario1Chosen)
        {
            scenario1Screenshot = screenshot;
        }
        else
        {
            scenario2Screenshot = screenshot;
        }
    }

    public Texture2D GetScreenshot(bool scenario1Chosen)
    {
        return scenario1Chosen ? scenario1Screenshot : scenario2Screenshot;
    }
}
