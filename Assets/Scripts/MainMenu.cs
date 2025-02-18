using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject continueButton; // Assign in Inspector

    void Start()
    {
        // Disable Continue button if no save exists
        if (!PlayerPrefs.HasKey("PlayerX") || !PlayerPrefs.HasKey("PlayerY"))
        {
            continueButton.SetActive(false);
        }
    }

    public void StartNewGame()
    {
        SaveManager.instance.ResetSave(); // ✅ Delete saved data
        SceneManager.LoadScene("MainLevel"); // ✅ Load the main game scene
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("MainLevel"); // ✅ Load last saved position
    }
}
