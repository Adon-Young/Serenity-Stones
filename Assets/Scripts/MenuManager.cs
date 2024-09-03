using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject howToPlayScreen;
    public GameObject themeScreen;
    public GameObject scenarioScreen;
    public GameObject creditsScreen;

    public Image scenario1DisplayMainMenu;
    public Image scenario2DisplayMainMenu;

    void Start()
    {
        if (ScreenshotSaving.Instance != null)
        {
            Texture2D scenario1Screenshot = ScreenshotSaving.Instance.GetScreenshot(true);
            Texture2D scenario2Screenshot = ScreenshotSaving.Instance.GetScreenshot(false);

            if (scenario1Screenshot != null)
            {
                scenario1DisplayMainMenu.sprite = Sprite.Create(scenario1Screenshot, new Rect(0, 0, scenario1Screenshot.width, scenario1Screenshot.height), Vector2.zero);
            }

            if (scenario2Screenshot != null)
            {
                scenario2DisplayMainMenu.sprite = Sprite.Create(scenario2Screenshot, new Rect(0, 0, scenario2Screenshot.width, scenario2Screenshot.height), Vector2.zero);
            }
        }

        ShowStartScreen();
    }

    public void ShowStartScreen()
    {
        HideAllScreens();
        startScreen.SetActive(true);
    }

    public void ShowScenarioScreen()
    {
        HideAllScreens();
        scenarioScreen.SetActive(true);
    }

    public void ShowHowToPlayScreen()
    {
        HideAllScreens();
        howToPlayScreen.SetActive(true);
    }

    public void ShowThemeScreen()
    {
        HideAllScreens();
        themeScreen.SetActive(true);
    }

    public void ShowCreditsScreen()
    {
        HideAllScreens();
        creditsScreen.SetActive(true);
    }

    private void HideAllScreens()
    {
        startScreen.SetActive(false);
        howToPlayScreen.SetActive(false);
        themeScreen.SetActive(false);
        scenarioScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }
}
