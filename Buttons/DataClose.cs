using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class DataClose : MonoBehaviour
{
    private PostProcessVolume ppVolume;
    [SerializeField] private Button myButton;
    public GameObject data;
    public Animator animator;

    void Start()
    {
        // Get the PostProcessVolume component
        ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClick);
    }

    // This method is called when the button is clicked
    public void OnClick()
    {
        // Start the coroutine to delay enabling the ppVolume
        GlobalVariable.popping = false;
        StartCoroutine(EnableAfterDelay());
        animator.SetTrigger("Fade out data");
    }

    // Coroutine to enable the ppVolume after a delay
    private IEnumerator EnableAfterDelay()
    {
        // Wait for 0.2 seconds
        yield return new WaitForSeconds(0.1f);

        // Enable the ppVolume
        ppVolume.enabled = false;
        yield return new WaitForSeconds(0.3f);
        data.SetActive(false);
    }
}
