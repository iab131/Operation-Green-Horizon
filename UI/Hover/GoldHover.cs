using UnityEngine;

public class GoldHover : MonoBehaviour
{
    private bool isHovering = false;
    private float hoverStartTime;

    private void OnMouseEnter()
    {
        if (!GlobalVariable.popping)
        {
            isHovering = true;
            hoverStartTime = Time.time;
        }
    }

    private void OnMouseExit()
    {
        isHovering = false;
        Tooltips.HideTooltipsStatic();
    }

    private void Update()
    {
        if (isHovering && Time.time - hoverStartTime >= 0.5f)
        {
            // Execute your action here after the specified duration
            Tooltips.ShowTooltipsStatic("<color=#F9C500>Gold: \nTo Buy Buildings</color>");
        }
    }
}