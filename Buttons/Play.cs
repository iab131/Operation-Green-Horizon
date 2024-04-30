using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Play : MonoBehaviour, IPointerDownHandler
{
    public string sceneName; // Name of the scene to load

    // Method called when the button is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        // Load the scene
        SceneManager.LoadScene(sceneName);
    }
}
