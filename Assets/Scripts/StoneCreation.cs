using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


using static UnityEngine.Mesh;

public class StoneCreation : MonoBehaviour
{
    // Array of stone prefabs to choose from
    public GameObject[] stonePrefabs;
    // Default number of stones in the list
    private int numberOfStonesInList = 5;
    public List<GameObject> stones = new List<GameObject>();

    // Transform variables
    public Transform stoneCurrentTransform;
    public Transform stoneResetTransform;
    public float Xrotation = 1;
    public float Yrotation = 1;
    public float rotationSpeed = 5;
    private float minimumScale = 20f;
    private float maximumScale = 100f;
    private float stoneMass = 10;
    public Color initialColor = new Color(0.3396226f, 0.3396226f, 0.3396226f);

   

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
    private List<string> S1words = new List<string>
    {
        // List of default Words on the stones

    "Balanced Meals",
    "Morning Stretch",
    "Mindful Eating",
    "Early to Bed",
    "Intermittent Fasting",
    "Lean Proteins",
    "Dark Chocolate",
    "Breathing Exercises",
    "Daily Steps",
    "Whole Foods",
    "Herbal Teas",
    "Natural Sweeteners",
    "Water Intake",
    "Fiber-Rich Foods",
    "Mental Clarity",
    "Consistent Routine",
    "Balanced Snacks",
    "Probiotics",
    "Outdoor Activity",
    "Limit Processed Foods"
    };

    private List<string> S2words = new List<string>
    {
        // List of default Words on the stones
    "Set Boundaries",
    "Delegate Tasks",
    "Personal Growth",
    "Unplug from Work",
    "Mindfulness Practice",
    "Quality Time",
    "Power Naps",
    "Team Building",
    "Networking Events",
    "Plan Leisure Time",
    "Cultural Activities",
    "Fitness Routine",
    "Creative Pursuits",
    "Flexible Schedule",
    "Weekend Getaways",
    "Lunch Breaks",
    "Disconnect to Reconnect",
    "Family Dinners",
    "Volunteering",
    "Morning Rituals"
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
        Vector3 offset = new Vector3(0, 0, 1.5f); // Adjust as needed to control how far in front of the stone the canvas should be
        
        // Calculate the canvas position based on the stone's position and the camera's position
        Vector3 directionToCamera = (mainGameCamera.transform.position - stoneTransform.position).normalized;
        Vector3 newCanvasPosition = stoneTransform.position + directionToCamera * offset.z;

        // Set the canvas position
        canvasTransform.position = newCanvasPosition;

        // Ensure the canvas always faces the camera
        FaceTheCamera(canvasTransform);
    }

    public void CreateStones(int numberOfStones)
    {
        // Ensure there are enough starting positions for the number of stones
        int maxPositions = Mathf.Min(numberOfStones, StartingPositions.Length);

        for (int i = 0; i < maxPositions; i++)
        {
            // Randomly choose a prefab from the array
            GameObject chosenPrefab = stonePrefabs[Random.Range(0, stonePrefabs.Length)];

            // Instantiate a new stone from the chosen prefab
            GameObject stone = Instantiate(chosenPrefab, StartingPositions[i].transform.position, Quaternion.identity);

            stone.tag = "Stone";
            SetStoneColor(stone, initialColor);

            //all x y and z scale for the stones should be the same random value
            float randomScaleAllAxis = Random.Range(minimumScale, maximumScale);
            stone.transform.localScale = new Vector3(randomScaleAllAxis, randomScaleAllAxis, randomScaleAllAxis);
            stone.GetComponent<Rigidbody>().mass = stoneMass;
           
            foreach (Transform child in stone.transform)
            {
                child.localScale = new Vector3(
                    0.01f / randomScaleAllAxis,
                    0.01f / randomScaleAllAxis,
                    0.01f / randomScaleAllAxis
                );
            }

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
    private void SetStoneColor(GameObject stone, Color color)
    {
        MeshRenderer meshRenderer = stone.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material.color = color;
        }
        else
        {
            Debug.LogWarning("No MeshRenderer found on the stone.");
        }
    }
    public void ResetStone(GameObject stone)
    {
        if (stone == null) return;

        // Find the index of the stone in the list
        int index = stones.IndexOf(stone);
        if (index < 0 || index >= StartingPositions.Length)
        {
            Debug.LogError("Stone index out of bounds.");
            return;
        }

        // Get the starting position GameObject
        GameObject startPositionObject = StartingPositions[index];
        if (startPositionObject == null)
        {
            Debug.LogError("Starting position GameObject is null.");
            return;
        }

        // Get the Transform component of the starting position GameObject
        Transform startPosition = startPositionObject.transform;

        // Reset stone properties
        stone.transform.position = startPosition.position;
        stone.transform.rotation = startPosition.rotation;

        float randomScaleAllAxis = Random.Range(minimumScale, maximumScale);
        stone.transform.localScale = new Vector3(randomScaleAllAxis, randomScaleAllAxis, randomScaleAllAxis);
        stone.GetComponent<Rigidbody>().mass = stoneMass;

        foreach (Transform child in stone.transform)
        {
            child.localScale = new Vector3(
                0.01f / randomScaleAllAxis,
                0.01f / randomScaleAllAxis,
                0.01f / randomScaleAllAxis
            );
        }

        Rigidbody rb = stone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Set Rigidbody to kinematic
            rb.velocity = Vector3.zero; // Reset velocity
            rb.angularVelocity = Vector3.zero; // Reset angular velocity
        }

        stone.tag = "Stone";
    }




    Transform FindCanvasTransform(GameObject stone)
    {
        if (stone == null) return null; // Check if the stone is null

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

        if(LevelController.scenario1chosen)
        {
            int randomIndex = Random.Range(0, S1words.Count);
            return S1words[randomIndex];
        }

        else
        {

            int randomIndex = Random.Range(0, S2words.Count);
            return S2words[randomIndex];
        }

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


    public void UpdateStoneMaterials(Material newMaterial)
    {
        foreach (GameObject stone in stones)
        {
            MeshRenderer meshRenderer = stone.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = newMaterial;
            }
        }
    }



}

