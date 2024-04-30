using UnityEngine;
using UnityEngine.EventSystems;

public class QuitGame : MonoBehaviour
{

    public void OnPointerDown()
    {
        // Quit the application (explicitly using UnityEngine namespace)
        Debug.Log("k");
        UnityEngine.Application.Quit();
    }
}
