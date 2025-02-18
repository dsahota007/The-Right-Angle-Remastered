using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private Vector2 defaultSpawnPoint = new Vector2(0, -3); // Default ground spawn position

    private void Awake()
    {
        // Singleton Pattern (Ensure only one instance)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep SaveManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerPosition(Vector2 position)
    {
        PlayerPrefs.SetFloat("PlayerX", position.x);
        PlayerPrefs.SetFloat("PlayerY", position.y);
        PlayerPrefs.Save(); // Save data immediately
    }

    public Vector2 LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
        {
            return new Vector2(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
        }
        return defaultSpawnPoint; // If no save exists, return ground spawn
    }
}
