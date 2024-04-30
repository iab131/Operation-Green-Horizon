using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class PopUpSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;

    public void PopUp(string text)
    {
        GlobalVariable.popping = true;
        GlobalVariable.isBuilding = false;
        GlobalVariable.buildingWaterFilter = false;
        GlobalVariable.buildingCO2Filter = false;
        GlobalVariable.buildingGoldMine = false;
        GlobalVariable.buildingElectricGenerator = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);

        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("pop"); 

        StartCoroutine(EnableAfterDelay());
    }
    private IEnumerator EnableAfterDelay()
    {
        // Wait for 0.2 seconds
        yield return new WaitForSeconds(0f);

        PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        ppVolume.enabled = true;
    }
}
