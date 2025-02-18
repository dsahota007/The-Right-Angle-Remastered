using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public float playerX;
    public float playerY;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private string savePath;

    private Vector2 defaultSpawnPoint = new Vector2(0, -50); // ✅ Change this to your desired spawn

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Application.persistentDataPath + "/savefile.json";
    }

    public void SavePlayerPosition(Vector2 position)
    {
        SaveData data = new SaveData { playerX = position.x, playerY = position.y };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public Vector2 LoadPlayerPosition()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return new Vector2(data.playerX, data.playerY);
        }
        return defaultSpawnPoint; // ✅ If no save exists, return default spawn
    }

    public void ResetSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath); // ✅ Delete the save file
        }
    }
}
