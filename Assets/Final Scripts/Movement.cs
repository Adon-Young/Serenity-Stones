using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject selectedStone;
    private TMP_InputField activeInputField;
    public bool isDragging = false;
    public static bool CanReset;

    private enum StoneState
    {
        Moveability, // State where the stone can be moved and rotated
        Text         // State where the stone is in text editing mode
    }

    private StoneState currentState = StoneState.Moveability;

    private Color defaultColor; // Default color when not picked up
    public Color pickedUpColor = Color.red;  // Color when picked up and in Moveability state
    public Color editableColor = Color.green; // Color when picked up and in Text state

    public float keyRotationAmount = 0.5f; // Rotation amount

    void Start()
    {
        CanReset = true; // Allow resetting initially
        mainCamera = Camera.main; // Get the main camera
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!"); // Log error if main camera is not found
        }
    }

    void Update()
    {
        if (LevelController.freezeGamePlay == false) // Check if gameplay is not frozen
        {
            HandleInput(); // Handle player input
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse click
        {
            if (isDragging && currentState == StoneState.Moveability) // If dragging and in Moveability state
            {
                ReleaseStone(); // Release the stone
            }
            else if (!isDragging)
            {
                TrySelectStone(); // Try selecting a stone
            }
        }

        if (isDragging) // If dragging a stone
        {
            switch (currentState)
            {
                case StoneState.Moveability:
                    MoveStone(); // Move the stone
                    HandleRotation(); // Handle stone rotation
                    if (Input.GetMouseButtonDown(1)) // Check for right mouse click
                    {
                        CanReset = false; // Disable reset
                        ToggleInputField(); // Toggle input field
                    }
                    break;

                case StoneState.Text:
                    if (Input.GetMouseButtonDown(1)) // Check for right mouse click
                    {
                        CanReset = true; // Enable reset
                        SwitchToMoveabilityState(); // Switch to Moveability state
                    }
                    break;
            }
        }
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.W)) RotateStone(Vector3.right, keyRotationAmount); // Rotate up
        if (Input.GetKey(KeyCode.S)) RotateStone(Vector3.right, -keyRotationAmount); // Rotate down
        if (Input.GetKey(KeyCode.A)) RotateStone(Vector3.up, keyRotationAmount); // Rotate left
        if (Input.GetKey(KeyCode.D)) RotateStone(Vector3.up, -keyRotationAmount); // Rotate right
    }

    void TrySelectStone()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera to the mouse position
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) // Cast the ray
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>(); // Get the Rigidbody component of the hit object
            if (rb != null)
            {
                selectedStone = rb.gameObject; // Set the selected stone
                isDragging = true; // Start dragging

                MeshRenderer renderer = selectedStone.GetComponent<MeshRenderer>(); // Get the MeshRenderer component
                if (renderer != null)
                {
                    defaultColor = renderer.material.color; // Store the default color
                    renderer.material.color = pickedUpColor; // Change color to red
                    rb.isKinematic = true; // Make the Rigidbody kinematic

                    // Resetting the position to 0 on the z-axis when picked up
                    Vector3 position = selectedStone.transform.position;
                    position.z = 0f;
                    selectedStone.transform.position = position;
                }
            }
        }
    }

    void ReleaseStone()
    {
        if (selectedStone != null)
        {
            MeshRenderer renderer = selectedStone.GetComponent<MeshRenderer>(); // Get the MeshRenderer component
            if (renderer != null)
            {
                renderer.material.color = defaultColor; // Revert color to default
                Rigidbody rb = selectedStone.GetComponent<Rigidbody>(); // Get the Rigidbody component
                if (rb != null)
                {
                    rb.isKinematic = false; // Make the Rigidbody non-kinematic
                }
            }
        }

        selectedStone = null; // Clear selected stone
        isDragging = false; // Stop dragging
        currentState = StoneState.Moveability; // Reset state to Moveability
        activeInputField = null; // Clear active input field
    }

    void MoveStone()
    {
        if (selectedStone == null) return;

        Vector3 mousePosition = Input.mousePosition; // Get mouse position
        mousePosition.z = mainCamera.WorldToScreenPoint(selectedStone.transform.position).z; // Set z distance

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // Convert mouse position to world position
        selectedStone.transform.position = new Vector3(worldPosition.x, worldPosition.y, selectedStone.transform.position.z); // Move stone
    }

    void RotateStone(Vector3 axis, float amount)
    {
        if (selectedStone == null) return;

        selectedStone.transform.Rotate(axis, amount, Space.Self); // Rotate stone around specified axis
    }

    void ToggleInputField()
    {
        if (selectedStone != null)
        {
            TMP_InputField inputField = FindObjectOfType<StoneCreation>().GetInputFieldFromStone(selectedStone); // Find the input field from StoneCreation
            if (inputField != null)
            {
                if (currentState == StoneState.Moveability)
                {
                    SwitchToTextState(inputField); // Switch to Text state
                }
            }
        }
    }

    void SwitchToTextState(TMP_InputField inputField)
    {
        inputField.ActivateInputField(); // Activate the input field
        inputField.Select(); // Select the input field
        currentState = StoneState.Text; // Change state to Text
        MeshRenderer renderer = selectedStone.GetComponent<MeshRenderer>(); // Get the MeshRenderer component
        if (renderer != null)
        {
            renderer.material.color = editableColor; // Set color to green
        }
    }

    void SwitchToMoveabilityState()
    {
        if (selectedStone != null)
        {
            TMP_InputField inputField = FindObjectOfType<StoneCreation>().GetInputFieldFromStone(selectedStone); // Find the input field from StoneCreation
            if (inputField != null)
            {
                inputField.DeactivateInputField(); // Deactivate the input field
                currentState = StoneState.Moveability; // Change state to Moveability
                MeshRenderer renderer = selectedStone.GetComponent<MeshRenderer>(); // Get the MeshRenderer component
                if (renderer != null)
                {
                    renderer.material.color = pickedUpColor; // Revert color to red
                }
            }
        }
    }
}
