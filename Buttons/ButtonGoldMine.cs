using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonGoldMine : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button myButton;
    [SerializeField] private Texture2D goldMine;

    private GameManager gameManager;
    private bool isHovering = false;
    private float hoverStartTime;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // Get a reference to the Button component
        myButton = GetComponent<Button>();

        // Add a listener for the button's click event
        myButton.onClick.AddListener(OnClick);
    }
    void Update()
    {
        if (isHovering && Time.time - hoverStartTime >= 0.5f)
        {
            // Execute your action here after the specified duration
            Tooltips.ShowTooltipsStatic("<color=#00FFFFFF>Gold Collector: Produce Gold</color>\n<color=#00FF00>Produce: </color>" + gameManager.goldGain.ToString() + "\n<color=#FFD700FF>Cost: </color>" + gameManager.goldCost);
        }
    }

    // Event handler for the button click event
    void OnClick()
    {
        if (GlobalVariable.isBuilding)
        {
            if(GlobalVariable.buildingCO2Filter || GlobalVariable.buildingWaterFilter || GlobalVariable.buildingElectricGenerator) 
            {
                GlobalVariable.buildingGoldMine = true;
                
                GlobalVariable.buildingCO2Filter = false;
                GlobalVariable.buildingWaterFilter = false;
                GlobalVariable.buildingElectricGenerator = false;

                Cursor.SetCursor(goldMine, new Vector2(goldMine.width / 2, goldMine.height / 2), CursorMode.ForceSoftware);
            }
            else
            {
                GlobalVariable.isBuilding = false;
                GlobalVariable.buildingGoldMine = false;

                Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            }
        }
        else
        {
            GlobalVariable.isBuilding = true;
            GlobalVariable.buildingGoldMine = true;
            Cursor.SetCursor(goldMine, new Vector2(goldMine.width / 2, goldMine.height / 2), CursorMode.ForceSoftware);
        }
        // Add your code to execute when the button is clicked
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GlobalVariable.popping)
        {
            isHovering = true;
            hoverStartTime = Time.time;
        }
    }

    // Event handler for when the pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        Tooltips.HideTooltipsStatic();
    }
}

