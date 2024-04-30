using UnityEngine;
using System.Collections;

public class Breath : MonoBehaviour
{
    public float minScale = 0.5f; // Minimum scale value
    public float maxScale = 1.0f; // Maximum scale value
    public float duration = 1.0f; // Duration of each breath (in seconds)

    private void Start()
    {
        // Start the breathing coroutine
        StartCoroutine(BreathingCoroutine());
    }

    private IEnumerator BreathingCoroutine()
    {
        while (true)
        {
            // Scale up
            yield return ScaleOverTime(transform.localScale, maxScale, duration);

            // Scale down
            yield return ScaleOverTime(transform.localScale, minScale, duration);
        }
    }

    private IEnumerator ScaleOverTime(Vector3 startScale, float targetScale, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            // Interpolate between startScale and targetScale over time
            transform.localScale = Vector3.Lerp(startScale, new Vector3(targetScale, targetScale, 1.0f), elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure that the final scale is exactly the target scale
        transform.localScale = new Vector3(targetScale, targetScale, 1.0f);
    }
}