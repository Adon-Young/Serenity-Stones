using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewStone : MonoBehaviour
{
    // Array of stone prefabs to choose from
    public GameObject[] stonePrefabs;
    public List<GameObject> stones = new List<GameObject>();

    // Stone-related variables
    public float minimumScale = 0.2f;
    public float maximumScale = 1f;
    public float stoneMass = 10;
    public Color initialColor = new Color(0.3396226f, 0.3396226f, 0.3396226f);

    public GameObject[] StartingPositions;
    public TMP_Dropdown wordDropdown; // Reference to the TMP_Dropdown UI element

    private List<string> words = new List<string>
    {
        "AWARENESS", "BALANCE", "CALM", "DETOX", "ENERGY", "FULFILLMENT", "GRATITUDE", "HARMONY", "INSIGHT",
        "JOY", "KINDNESS", "LOVE", "MINDFULNESS", "NURTURE", "OPTIMISM", "PEACE", "QUALITY", "RELAXATION",
        "SERENITY", "TRANQUILITY", "UNDERSTANDING", "VITALITY", "WELLNESS", "XENIAL", "YOGA", "ZEN",
        "ACCOMPLISHMENT", "BELONGING", "COMPASSION", "DEDICATION", "EMPATHY", "FAMILY", "GENEROSITY", "HOPE",
        "INTEGRITY", "JUSTICE", "KNOWLEDGE", "LEADERSHIP", "MENTORSHIP", "NETWORKING", "OPPORTUNITY", "PURPOSE",
        "QUESTIONING", "RESPECT", "SUPPORT", "TRUST", "UNITY", "VALUES", "WISDOM", "XENODOCHIAL", "YIELD", "ZEAL"
    };

    private Dictionary<string, WordAttributes> wordAttributes = new Dictionary<string, WordAttributes>(); // Map words to their attributes
    private GameObject selectedStone; // Add this line to keep track of the selected stone

    void Start()
    {
        PopulateWordAttributes();
        PopulateWordDropdown();
        CreateStones(5);
    }

    void PopulateWordAttributes()
    {
        // Here you define the attributes for each word.
        wordAttributes.Add("AWARENESS", new WordAttributes { Friction = 0.5f, Scale = 0.8f });
        wordAttributes.Add("BALANCE", new WordAttributes { Friction = 0.8f, Scale = 1.0f });
        // Add other word attributes...
    }

    void PopulateWordDropdown()
    {
        wordDropdown.ClearOptions();
        wordDropdown.AddOptions(words);
        wordDropdown.onValueChanged.AddListener(delegate { WordSelected(wordDropdown); });
    }

    public void WordSelected(TMP_Dropdown dropdown)
    {
        string selectedWord = dropdown.options[dropdown.value].text;
        ApplyWordToSelectedStone(selectedWord);
    }

    void ApplyWordToSelectedStone(string word)
    {
        if (selectedStone == null || !wordAttributes.ContainsKey(word)) return;

        // Get the attributes for the selected word
        WordAttributes attributes = wordAttributes[word];

        // Apply the attributes to the stone
        Rigidbody rb = selectedStone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.mass = stoneMass * attributes.Scale;  // Example of scaling mass
        }

        MeshRenderer renderer = selectedStone.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material.color = initialColor; // Change to desired color if needed
        }

        // Apply more attributes as needed...
    }

    void CreateStones(int numberOfStones)
    {
        for (int i = 0; i < numberOfStones; i++)
        {
            int prefabIndex = Random.Range(0, stonePrefabs.Length);
            GameObject stone = Instantiate(stonePrefabs[prefabIndex], StartingPositions[i].transform.position, Quaternion.identity);

            // Randomize the scale
            float randomScale = Random.Range(minimumScale, maximumScale);
            stone.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Set stone mass
            Rigidbody rb = stone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.mass = stoneMass;
            }

            // Set initial color
            MeshRenderer renderer = stone.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = initialColor;
            }

            stones.Add(stone);
        }
    }

    private class WordAttributes
    {
        public float Friction { get; set; }
        public float Scale { get; set; }
        // Add more attributes as needed
    }
}
