using UnityEngine;
using TMPro;

public class NewMovement : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject selectedStone;
    private bool isDragging = false;
    private NewStone stoneCreation;  // Reference to the NewStone script

    private enum StoneState
    {
        Moveability,
        Text
    }

    private StoneState currentState = StoneState.Moveability;

    void Start()
    {
        mainCamera = Camera.main;
        stoneCreation = FindObjectOfType<NewStone>();  // Find the NewStone script in the scene
    }

    void Update()
    {
        if (!LevelController.freezeGamePlay)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isDragging && currentState == StoneState.Moveability)
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
                    if (Input.GetMouseButtonDown(1)) // Right click
                    {
                        ToggleWordSelectionUI(true);
                    }
                    break;

                case StoneState.Text:
                    if (Input.GetMouseButtonDown(1)) // Right click
                    {
                        ToggleWordSelectionUI(false);
                    }
                    break;
            }
        }
    }

    void ToggleWordSelectionUI(bool show)
    {
        if (stoneCreation != null)
        {
            stoneCreation.wordDropdown.gameObject.SetActive(show); // Show or hide the dropdown
        }

        if (show)
        {
            currentState = StoneState.Text;
        }
        else
        {
            currentState = StoneState.Moveability;
        }
    }

    void TrySelectStone()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Stone"))
            {
                selectedStone = hit.collider.gameObject;
                isDragging = true;
            }
        }
    }

    void MoveStone()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Vector3 newPosition = hit.point;
                selectedStone.transform.position = newPosition;
            }
        }
    }

    void ReleaseStone()
    {
        isDragging = false;
        selectedStone = null;
    }
}
