using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonWaterFilter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button myButton;
    [SerializeField] private Texture2D waterFilter;

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
            Tooltips.ShowTooltipsStatic("<color=#00FFFFFF>Water Filter: Filter Polluted Water</color>\n<color=#00FF00>Produce: </color>" + gameManager.waterProduce.ToString() + " ML\n<color=#FFD700FF>Cost: </color>" + gameManager.waterCost);
        }
    }

    // Event handler for the button click event
    void OnClick()
    {
        if (GlobalVariable.isBuilding)
        {
            if (GlobalVariable.buildingGoldMine || GlobalVariable.buildingCO2Filter || GlobalVariable.buildingElectricGenerator)
            {
                GlobalVariable.buildingWaterFilter = true;

                GlobalVariable.buildingCO2Filter = false;
                GlobalVariable.buildingGoldMine = false;
                GlobalVariable.buildingElectricGenerator = false;

                Cursor.SetCursor(waterFilter, new Vector2(waterFilter.width / 2, waterFilter.height / 2), CursorMode.ForceSoftware);
            }
            else
            {
                GlobalVariable.isBuilding = false;
                GlobalVariable.buildingWaterFilter = false;

                Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            }
        }
        else
        {
            GlobalVariable.isBuilding = true;
            GlobalVariable.buildingWaterFilter = true;

            Cursor.SetCursor(waterFilter, new Vector2(waterFilter.width / 2, waterFilter.height / 2), CursorMode.ForceSoftware);
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
