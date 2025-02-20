using UnityEngine;

public class FullscreenBackground : MonoBehaviour
{
    void Start()
    {
        ResizeToScreen();
    }

    void ResizeToScreen()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("No SpriteRenderer found on " + gameObject.name);
            return;
        }

        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        Vector3 newScale = new Vector3(screenWidth / sr.bounds.size.x, screenHeight / sr.bounds.size.y, 1);
        transform.localScale = newScale;
    }
}
