using UnityEngine;
using TMPro;


public class Movement : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject selectedStone;
    private TMP_InputField activeInputField;
    private bool isDragging = false;
    public static bool CanReset;

    private enum StoneState
    {
        Moveability,
        Text
    }

    private StoneState currentState = StoneState.Moveability;

    private Color defaultColor; // Default color when not picked up
    public Color pickedUpColor = Color.red;  // Color when picked up and in Moveability state
    public Color editableColor = Color.green; // Color when picked up and in Text state

    public float keyRotationAmount = 0.5f; // Rotation amount

 


    void Start()
    {
        CanReset = true;
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }
    }

    void Update()
    {
        if(LevelController.freezeGamePlay == false)//if the game sint frozen then you can pick up rotate and place stones/ edit their text...
        {
            HandleInput();
        }
       
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            if (isDragging && currentState == StoneState.Moveability) // Only drop if in Moveability state
            {
                ReleaseStone();
            }
            else if (!isDragging)
            {
                TrySelectStone();
            }
        }

        if (isDragging)
        {
            switch (currentState)
            {
                case StoneState.Moveability:
                    MoveStone();
                    HandleRotation();
                    if (Input.GetMouseButtonDown(1)) // Right click
                    {
                        CanReset = false;
                        ToggleInputField();
                    }
                    break;

                case StoneState.Text:
                    if (Input.GetMouseButtonDown(1)) // Right click
                    {
                        CanReset = true;
                        SwitchToMoveabilityState();
                    }
                    break;
            }
        }
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.W)) RotateStone(Vector3.right, keyRotationAmount);
        if (Input.GetKey(KeyCode.S)) RotateStone(Vector3.right, -keyRotationAmount);
        if (Input.GetKey(KeyCode.A)) RotateStone(Vector3.up, keyRotationAmount);
        if (Input.GetKey(KeyCode.D)) RotateStone(Vector3.up, -keyRotationAmount);
    }

    void TrySelectStone()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                selectedStone = rb.gameObject;
                isDragging = true;

                MeshRenderer renderer = selectedStone.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    defaultColor = renderer.material.color;
                    renderer.material.color = pickedUpColor; // Change color to red
                    rb.isKinematic = true;

                    //resetting the position to 0 on the z axis when picked up
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
            MeshRenderer renderer = selectedStone.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = defaultColor; // Revert color to default
                Rigidbody rb = selectedStone.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
          
        }

        selectedStone = null;
        isDragging = false;
        currentState = StoneState.Moveability;
        activeInputField = null;
    }

    void MoveStone()
    {
        if (selectedStone == null) return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(selectedStone.transform.position).z;

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        selectedStone.transform.position = new Vector3(worldPosition.x, worldPosition.y, selectedStone.transform.position.z);
    }

    void RotateStone(Vector3 axis, float amount)
    {
        if (selectedStone == null) return;

        selectedStone.transform.Rotate(axis, amount, Space.Self);
    }

    void ToggleInputField()
    {
        if (selectedStone != null)
        {
            TMP_InputField inputField = FindObjectOfType<StoneCreation>().GetInputFieldFromStone(selectedStone);
            if (inputField != null)
            {
                if (currentState == StoneState.Moveability)
                {
                    SwitchToTextState(inputField);
                }
            }
         
        }
    }

    void SwitchToTextState(TMP_InputField inputField)
    {
        inputField.ActivateInputField();
        inputField.Select();
        currentState = StoneState.Text;
        MeshRenderer renderer = selectedStone.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material.color = editableColor; // Set color to green
        }
    }

    void SwitchToMoveabilityState()
    {
        if (selectedStone != null)
        {
            TMP_InputField inputField = FindObjectOfType<StoneCreation>().GetInputFieldFromStone(selectedStone);
            if (inputField != null)
            {
                inputField.DeactivateInputField();
                currentState = StoneState.Moveability;
                MeshRenderer renderer = selectedStone.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material.color = pickedUpColor; // Revert color to red
                }
            }
        }
    }
}
