using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import for TextMeshPro

public class DataCollection : MonoBehaviour
{
    // Player ID variables
    public TMP_InputField playerIDInput; // TextMeshPro Input Field
    private string playerID;

    // Reset counters
    private int accidentalResets = 0;
    private int strategicResets = 0;
    private int buttonResets = 0;

    // Timer variables
    private float elapsedTime = 0f;
    private bool isTiming = false;

    // UI Text for displaying data
    public TextMeshProUGUI playerIDText;
    public TextMeshProUGUI accidentalResetText;
    public TextMeshProUGUI purposefulResetText;
    public TextMeshProUGUI buttonResetText;
    public TextMeshProUGUI timerText;

    // Button to toggle data display
    public Button toggleButton;

    // Button to start the timer (reference to your button)
    public Button startTimerButton;

    // Reference to the GameObject that holds all the UI text objects
    public GameObject dataHolder;
    public GameObject nextGameButton;

    // Bool to show/hide data
    private bool displayData = false;

    // Public variable to see the elapsed time in the Inspector
    public float debugElapsedTime;

    private void Start()
    {
        Debug.Log($"Initial Button Resets: {buttonResets}");
        Debug.Log($"Initial Accidental Resets: {accidentalResets}");
        Debug.Log($"Initial Strategic Resets: {strategicResets}");

        // Attach listener to save Player ID on input field end edit
        playerIDInput.onEndEdit.AddListener(SavePlayerID);

        // Attach listener to toggle button and initially hide data
        toggleButton.onClick.AddListener(ToggleDisplay);

        // Attach listener to start timer button
        startTimerButton.onClick.AddListener(StartTimer);
    }

    private void Update()
    {
        // Update timer if timing is active
        if (isTiming)
        {
            elapsedTime += Time.deltaTime; // Keeps track of elapsed time
            debugElapsedTime = elapsedTime; // Update debug variable for Inspector
        }

        // Update displayed text if displayData is true
        if (displayData)
        {
            accidentalResetText.text = "Accidental Resets: " + accidentalResets;
            purposefulResetText.text = "Purposeful Resets: " + strategicResets;
            buttonResetText.text = "Button Resets: " + buttonResets;
            timerText.text = "Time: " + GetFormattedTime();
        }
    }

    // Save player ID from the input field
    private void SavePlayerID(string input)
    {
        playerID = input;
        playerIDText.text = "Player ID: " + playerID; // Update text display immediately
    }

    // Start the timer
    public void StartTimer()
    {
        isTiming = true;
        elapsedTime = 0f; // Reset timer each time it starts
        timerText.text = "Time: " + GetFormattedTime(); // Display the timer immediately
    }

    // Stop the timer
    public void StopTimer()
    {
        isTiming = false;
    }

    // Format the elapsed time into minutes, seconds, and milliseconds
    private string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime - Mathf.Floor(elapsedTime)) * 1000); // Calculate milliseconds

        return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds); // Format with milliseconds
    }

    // Toggle display of data on and off
    private void ToggleDisplay()
    {
        displayData = !displayData;

        // Enable or disable the entire data holder
        dataHolder.SetActive(displayData);
        nextGameButton.SetActive(displayData);
    }

    // This adds the number of times a player presses the R button to reset their stones.
    public void ButtonResetCounter()
    {
        if (Input.GetKeyDown(KeyCode.R) && Movement.CanReset == true)
        {
            buttonResets++;
            Debug.Log($"Button Reset Counter Incremented: {buttonResets}"); // Log the count
        }
    }

    // Whenever the stones just fall and reset due to mistakes or impatience.
    public void AccidentalResetCounter()
    {
        accidentalResets++; // Increment accidental resets
        Debug.Log($"Accidental Reset Counter Incremented: {accidentalResets}"); // Log the count
    }

    public void StrategicResetCounter()
    {
        strategicResets++;
        Debug.Log($"Strategic Reset Counter Incremented: {strategicResets}"); // Log the count
    }
}
