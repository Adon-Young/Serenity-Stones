using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
public class PopUpInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverButton;
    private bool mouseHover = false;
    private Coroutine hideCoroutine = null;

    void Start()
    {
        HoverButton.SetActive(false);
    }

    void Update()
    {
        if (mouseHover)
        {
            Debug.Log("Mouse Over");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseHover = true;
        HoverButton.SetActive(true);

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseHover = false;
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay time if needed
        if (!mouseHover) // Double-check the mouse hasn't re-entered
        {
            HoverButton.SetActive(false);
        }
    }
}