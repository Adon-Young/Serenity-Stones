using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopUpInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverButton;


    void Start()
    {
        HoverButton.SetActive(false);//the ship information is origionally set to false
    }

    private bool mouseHover = false;//if this variable is true then the information is revealed to the player
    void Update()
    {
        if (mouseHover)
        {
            Debug.Log("Mouse Over");//this was just for testing to see if the code was working properly
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseHover = true;
        HoverButton.SetActive(true);//this sets the ships stats infor box to true allowing the player to see the ship information
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseHover = false;//if the mouse is not hovering over the button then the informaiton doesnt pop up for  the ship
        HoverButton.SetActive(false);
    }

}//end of class
