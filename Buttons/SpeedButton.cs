using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SpeedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button myButton;
    public TextMeshProUGUI speedDisplay;

    private bool isHovering = false;
    private float hoverStartTime;
    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClick);
    }
    void Update()
    {
        if (isHovering && Time.time - hoverStartTime >= 0.5f)
        {
            // Execute your action here after the specified duration
            Tooltips.ShowTooltipsStatic("Click to Expedite The Game");
        }
    }
    // Update is called once per frame
    public void OnClick()
    {
        // Start the coroutine to delay enabling the ppVolume
        if (GlobalVariable.speedInGame == 1)
        {
            GlobalVariable.speedInGame = 2;
        }
        else if (GlobalVariable.speedInGame == 2)
        {
            GlobalVariable.speedInGame = 4;
        }
        else if (GlobalVariable.speedInGame == 4)
        {
            GlobalVariable.speedInGame = 8;
        }
        else
        {
            GlobalVariable.speedInGame = 1;
        }

        speedDisplay.text = GlobalVariable.speedInGame.ToString() + "X";
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        hoverStartTime = Time.time;
    }

    // Event handler for when the pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        Tooltips.HideTooltipsStatic();
    }
}
