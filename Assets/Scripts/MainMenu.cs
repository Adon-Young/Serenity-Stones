using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Continent
{
    public string continentName;
    public Sprite continentImage;
    public List<Country> countries;
}

[System.Serializable]
public class Country
{
    public string countryName;
    public Sprite countryImage;

}




public class MainMenu : MonoBehaviour
{
    public GameObject continentPrefabButton;
    public GameObject countryPrefabButton;
    public Transform continentParent;
    public Transform countryParent;
    public List<Continent> continents;
    private Continent selectedContinent;
    public Button backButton;


    public void Start()
    {
        PopulateContinentButtons();

        // Hide the back button initially
        backButton.gameObject.SetActive(false);
    }

    private void PopulateContinentButtons()
    {
        foreach (Transform child in continentParent)
        {
            Destroy(child.gameObject);
        }

        float yOffset = 0f;
        float buttonSpacing = 100f; // Adjust this value as needed

        foreach (var continent in continents)
        {
            GameObject button = Instantiate(continentPrefabButton, continentParent);
            button.GetComponent<Image>().sprite = continent.continentImage;
            button.GetComponent<Button>().onClick.AddListener(() => SelectContinent(continent));

            // Adjust the position of the button
            RectTransform rt = button.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, yOffset);
            yOffset -= buttonSpacing;
        }
    }


    void SelectContinent(Continent continent)
    {
        selectedContinent = continent;
        PopulateCountryFlags(); // Populate country flags when a continent is selected

        // Hide the continent panel and show the country panel
        continentParent.gameObject.SetActive(false);
        countryParent.gameObject.SetActive(true);

        // Show the back button
        backButton.gameObject.SetActive(true);
    }

    void PopulateCountryFlags()
    {
        // Clear existing country flags (if any)
        foreach (Transform child in countryParent)
        {
            Destroy(child.gameObject);
        }

        // Check if a continent is selected
        if (selectedContinent == null)
        {
            return;
        }

        // Create a flag for each country in the selected continent
        foreach (var country in selectedContinent.countries)
        {
            GameObject flag = Instantiate(countryPrefabButton, countryParent);
            flag.GetComponent<Image>().sprite = country.countryImage; // Set the country flag image
            flag.GetComponent<Button>().onClick.AddListener(() => SelectCountry(country));
        }
    }


    private void SelectCountry(Country country)
    {
        // Handle country selection
        Debug.Log("Selected country: " + country.countryName);
    }



    public void OnBackButtonClicked()
    {
        // Disable the country panel and enable the continent panel
        countryParent.gameObject.SetActive(false);
        continentParent.gameObject.SetActive(true);

        // Disable the back button itself when returning to the continent panel
        backButton.gameObject.SetActive(false);

        // Clear existing country flags
        foreach (Transform child in countryParent)
        {
            Destroy(child.gameObject);
        }

        selectedContinent = null; // Optionally clear the selected continent
    }

}
