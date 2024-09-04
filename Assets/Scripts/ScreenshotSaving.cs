using UnityEngine;
using UnityEngine.UI;
public class ScreenshotSaving : MonoBehaviour
{
    public static ScreenshotSaving Instance { get; private set; }

    public Texture2D scenario1Screenshot;
    public Texture2D scenario2Screenshot;

    public string scenario1Reflection;
    public string scenario2Reflection;

    public Text S1Reflection;
    public Text S2Reflection;

    private void Awake()
    {
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

    public void SaveReflection(string reflection, bool scenario1Chosen)
    {
        if (scenario1Chosen)
        {
            scenario1Reflection = reflection;
        }
        else
        {
            scenario2Reflection = reflection;
        }
    }

    public string GetReflection(bool scenario1Chosen)
    {
        return scenario1Chosen ? scenario1Reflection : scenario2Reflection;
    }

}