using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonCO2Filter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button myButton;
    [SerializeField] private Texture2D CO2Filter;

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
            Tooltips.ShowTooltipsStatic("<color=#00FFFFFF>CO2 Filter: Reduces CO2</color>" +
                "\n<color=#00FF00>Reduce: </color>" + gameManager.CO2Loss.ToString() + 
                " PPM/S\n<color=#FFD700FF>Cost: </color>" + gameManager.CO2Cost.ToString() +
                " \n<color=#B22222>Consume: </color>" + gameManager.waterNeed.ToString() + " ML || " +
                gameManager.electricityNeed.ToString() + " kMh");
        }
    }

    // Event handler for the button click event
    void OnClick()
    {
        if (GlobalVariable.isBuilding)
        {
            if(GlobalVariable.buildingWaterFilter || GlobalVariable.buildingElectricGenerator || GlobalVariable.buildingGoldMine) 
            {
                GlobalVariable.buildingCO2Filter = true;

                GlobalVariable.buildingWaterFilter = false;
                GlobalVariable.buildingElectricGenerator = false;
                GlobalVariable.buildingGoldMine = false;

                Cursor.SetCursor(CO2Filter, new Vector2(CO2Filter.width / 2, CO2Filter.height / 2), CursorMode.ForceSoftware);
            }
            else
            {
                GlobalVariable.isBuilding = false;
                GlobalVariable.buildingCO2Filter = false;

                Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            }
           
        }
        else
        {
            GlobalVariable.isBuilding = true;
            GlobalVariable.buildingCO2Filter = true;

            Cursor.SetCursor(CO2Filter, new Vector2(CO2Filter.width / 2, CO2Filter.height / 2), CursorMode.ForceSoftware);
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
