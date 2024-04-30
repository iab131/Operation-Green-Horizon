using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ProgressHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering = false;
    private float hoverStartTime;
    public Toggle toggleSwitch;

    void Update()
    {
        if (isHovering && Time.time - hoverStartTime >= 0.5f)
        {
            // Execute your action here after the specified duration
            Tooltips.ShowTooltipsStatic("Progress Left Until \nReaching A New Earth");
        }
    }
    // Method called when the mouse enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Quit the application
        if (!GlobalVariable.popping && toggleSwitch.isOn)
        {
            isHovering = true;
            hoverStartTime = Time.time;
        }
    }

    // Method called when the mouse exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        Tooltips.HideTooltipsStatic();
    }

    public void SetGameObjectActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
