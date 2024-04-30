using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class DataButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering = false;
    private float hoverStartTime;

    private PostProcessVolume ppVolume;
    [SerializeField] private Button myButton;
    public GameObject data;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        data.SetActive(false);
        ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovering && Time.time - hoverStartTime >= 0.5f)
        {
            // Execute your action here after the specified duration
            Tooltips.ShowTooltipsStatic("Data Sheet");
        }
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


    public void OnClick()
    {
        if (!GlobalVariable.popping)
        {
            data.SetActive(true);
            animator.SetTrigger("Fade in data");
            DataManager.UpdateValue();
            ppVolume.enabled = true;

            GlobalVariable.popping = true;
            GlobalVariable.isBuilding = false;
            GlobalVariable.buildingWaterFilter = false;
            GlobalVariable.buildingCO2Filter = false;
            GlobalVariable.buildingGoldMine = false;
            GlobalVariable.buildingElectricGenerator = false;
        }
    }
}
