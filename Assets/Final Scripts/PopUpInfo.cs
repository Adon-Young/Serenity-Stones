using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PopUpInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverButton; // UI element to display on hover
    private bool mouseHover = false; // Flag to track mouse hover state
    private Coroutine hideCoroutine = null; // Coroutine reference for hiding the UI element

    void Start()
    {
        HoverButton.SetActive(false); // Initially hide the HoverButton
    }

    void Update()
    {
        // Optional: Log message when the mouse is over the UI element
        if (mouseHover)
        {
            Debug.Log("Mouse Over");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set mouseHover flag and show the HoverButton
        mouseHover = true;
        HoverButton.SetActive(true);

        // Stop any existing hide coroutine if the mouse re-enters
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Set mouseHover flag and start coroutine to hide the HoverButton after a delay
        mouseHover = false;
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds before hiding
        if (!mouseHover) // Ensure the mouse is still not over the UI element
        {
            HoverButton.SetActive(false);
        }
    }
}
