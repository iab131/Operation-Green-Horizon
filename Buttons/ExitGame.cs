using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ExitGame : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public string sceneName; // Name of the scene to load

    // Method called when the button is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        // Load the scene
        SceneManager.LoadScene(sceneName);
    }
}