using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Mesh;

public class StoneCreation : MonoBehaviour
{
    // Array of stone prefabs to choose from
    public GameObject[] stonePrefabs;
    // Default number of stones in the list
    public int numberOfStonesInList = 3;
    public List<GameObject> stones = new List<GameObject>();

    // Transform variables
    public Transform stoneCurrentTransform;
    public Transform stoneResetTransform;
    public float Xrotation = 1;
    public float Yrotation = 1;
    public float rotationSpeed = 1;
    private float stoneWeight = 1;
    private float stoneScaleX = 1;
    private float stoneScaleY = 1;
    private float stoneScaleZ = 1;


    public GameObject[] StartingPositions;


    //-----------------------------

    // Physics related component variables
    public Rigidbody stoneRB;
    public MeshCollider stoneCollider;
    public float vertPos = 1;
    //-----------------------------------

    // Visuals for the stone
    public MeshFilter stoneMeshFilter;
    public MeshRenderer stoneMeshRender;
    public int vertexDensity = 1;
    //-----------------------------------

    // Variables for transporting falling stones
    public float dropDistance = -5;
    //-----------------------------

    // Variables for text object on the stone
    public TMP_InputField stoneTextBox;
    public GameObject mainGameCamera;
    private List<string> words = new List<string>
    {
        // List of default Words on the stones

        "AWARENESS",
        "BALANCE",
        "CALM",
        "DETOX",
        "ENERGY",
        "FULFILLMENT",
        "GRATITUDE",
        "HARMONY",
        "INSIGHT",
        "JOY",
        "KINDNESS",
        "LOVE",
        "MINDFULNESS",
        "NURTURE",
        "OPTIMISM",
        "PEACE",
        "QUALITY",
        "RELAXATION",
        "SERENITY",
        "TRANQUILITY",
        "UNDERSTANDING",
        "VITALITY",
        "WELLNESS",
        "XENIAL",
        "YOGA",
        "ZEN",

        "ACCOMPLISHMENT",
        "BELONGING",
        "COMPASSION",
        "DEDICATION",
        "EMPATHY",
        "FAMILY",
        "GENEROSITY",
        "HOPE",
        "INTEGRITY",
        "JUSTICE",
        "KNOWLEDGE",
        "LEADERSHIP",
        "MENTORSHIP",
        "NETWORKING",
        "OPPORTUNITY",
        "PURPOSE",
        "QUESTIONING",
        "RESPECT",
        "SUPPORT",
        "TRUST",
        "UNITY",
        "VALUES",
        "WISDOM",
        "XENODOCHIAL",
        "YIELD",
        "ZEAL"
    };
    //--------------------------------------------

    void Start()
    {
        // Assigning the game camera-already in scene
        mainGameCamera = GameObject.Find("Main Camera");
        CreateStones(numberOfStonesInList);
    }

    void Update()
    {
        foreach (GameObject stone in stones)
        {
            // Find the Canvas Transform within the stone
            Transform canvasTransform = FindCanvasTransform(stone);

            if (canvasTransform != null)
            {
                // Update canvas position and rotation to keep it in front of the stone
                UpdateCanvasPositionAndRotation(canvasTransform, stone.transform);
            }
        }
    }

    void UpdateCanvasPositionAndRotation(Transform canvasTransform, Transform stoneTransform)
    {
        // Define the desired offset in front of the stone
        Vector3 offset = new Vector3(0, 0, 1); // Adjust as needed to control how far in front of the stone the canvas should be

        // Calculate the canvas position based on the stone's position and the camera's position
        Vector3 directionToCamera = (mainGameCamera.transform.position - stoneTransform.position).normalized;
        Vector3 newCanvasPosition = stoneTransform.position + directionToCamera * offset.z;

        // Set the canvas position
        canvasTransform.position = newCanvasPosition;

        // Ensure the canvas always faces the camera
        FaceTheCamera(canvasTransform);
    }

    void CreateStones(int numberOfStones)
    {
        // Ensure there are enough starting positions for the number of stones
        int maxPositions = Mathf.Min(numberOfStones, StartingPositions.Length);

        for (int i = 0; i < maxPositions; i++)
        {
            // Randomly choose a prefab from the array
            GameObject chosenPrefab = stonePrefabs[Random.Range(0, stonePrefabs.Length)];

            // Instantiate a new stone from the chosen prefab
            GameObject stone = Instantiate(chosenPrefab, StartingPositions[i].transform.position, Quaternion.identity);
            stone.transform.localScale = new Vector3(1, 1, 1);

            // Add the stone to the list
            stones.Add(stone);

          

            // Find the Canvas component within the instantiated stone
            Transform canvasTransform = FindCanvasTransform(stone);

            if (canvasTransform != null)
            {
                // Ensure the canvas faces the camera
                FaceTheCamera(canvasTransform);
            }
            else
            {
                Debug.LogWarning("No Canvas found in the instantiated stone prefab.");
            }

            // Optionally: Find the TMP_InputField component and set the text
            CreateStoneText(stone);
        }
    }

    Transform FindCanvasTransform(GameObject stone)
    {
        // Search for the Canvas within the stone hierarchy
        Transform canvasTransform = stone.transform.Find("StoneCanvas");

        if (canvasTransform == null)
        {
            // If not found directly, search recursively
            canvasTransform = SearchForCanvasRecursively(stone.transform);
        }

        return canvasTransform;
    }

    Transform SearchForCanvasRecursively(Transform parent)
    {
        // Check if the current transform has a Canvas
        Canvas canvas = parent.GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            return canvas.transform;
        }

        // Recursively search children
        foreach (Transform child in parent)
        {
            Transform result = SearchForCanvasRecursively(child);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }






    public void FaceTheCamera(Transform canvasTransform)
    {
        if (mainGameCamera == null)
        {
            Debug.LogError("MainCam Not found");
            return;
        }

        // Calculate the direction to the camera
        Vector3 directionToCamera = mainGameCamera.transform.position - canvasTransform.position;

        // Make the canvas face the camera
        canvasTransform.LookAt(canvasTransform.position + directionToCamera);

        // Adjust the rotation to ensure the text is not flipped
        canvasTransform.Rotate(Vector3.up * 180f); // This might flip the canvas
    }

    void CreateStoneText(GameObject stone)
    {
        // Find the TMP_InputField component in the stone prefab
        TMP_InputField stoneTextBox = stone.GetComponentInChildren<TMP_InputField>();

        if (stoneTextBox != null)
        {
            // Get the placeholder component
            TextMeshProUGUI placeholderText = stoneTextBox.placeholder as TextMeshProUGUI;

            if (placeholderText != null)
            {
                // Set the placeholder text to a random word from the list
                string randomWord = GetRandomWord();
                placeholderText.text = randomWord;

                // Log the assigned text for debugging purposes
                Debug.Log($"Assigned placeholder text '{randomWord}' to the existing TextObject on the stone.");
            }
            else
            {
                Debug.LogWarning("TextMeshProUGUI component not found in the TMP_InputField's placeholder.");
            }

            // Find the canvas that is a child of the stone and face the camera
            Transform canvasTransform = FindCanvasTransform(stone);
            if (canvasTransform != null)
            {
                FaceTheCamera(canvasTransform);
            }
            else
            {
                Debug.LogWarning("No Canvas found on the stone prefab.");
            }
        }
        else
        {
            Debug.LogWarning("No TMP_InputField found on the stone prefab.");
        }
    }



    string GetRandomWord()
    {
        int randomIndex = Random.Range(0, words.Count);
        return words[randomIndex];
    }

    public TMP_InputField GetInputFieldFromStone(GameObject stone)
    {
        // Check if the stone has a Canvas
        Transform canvasTransform = stone.transform.Find("StoneCanvas");
        if (canvasTransform != null)
        {
            // Find the TMP_InputField within the Canvas
            TMP_InputField inputField = canvasTransform.GetComponentInChildren<TMP_InputField>();
            if (inputField != null)
            {
                return inputField;
            }
            else
            {
                Debug.LogWarning("No TMP_InputField found within the Canvas on the stone.");
            }
        }
        else
        {
            Debug.LogWarning("No Canvas named 'StoneCanvas' found on the stone.");
        }

        return null;
    }


}

