using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltips : MonoBehaviour
{
    private static Tooltips instance;

    [SerializeField] private Camera uiCamera;
    [SerializeField] private RectTransform canvasRectTransform;


    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private RectTransform backgroundRectTransform;

    private void Awake()
    {
        gameObject.SetActive(false);
        instance = this;
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();

    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        localPoint.x += 70.0f;
        localPoint.y -= 70.0f;
        transform.localPosition = localPoint;

        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width + 50)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width + 50;
        }
        if (anchoredPosition.y < 25)
        {
            anchoredPosition.y = backgroundRectTransform.rect.height-50;
        }
        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }
    public void ShowTooltip(string tooltipString)
    {
        tooltipText.text = tooltipString;
        float textPaddingSize = 6f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight);
        backgroundRectTransform.sizeDelta = backgroundSize;
        gameObject.SetActive(true);

    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltipsStatic(string a)
    {
        instance.ShowTooltip(a);
    }
    public static void HideTooltipsStatic()
    {
        instance.HideTooltip();
    }
}
