using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject continueButton;  // ✅ Assign in Inspector
    public GameObject newGameConfirmationUI; // ✅ Assign the "Are You Sure?" UI Panel in Inspector

    void Start()
    {
        // ✅ Disable Continue button if no save exists
        if (!PlayerPrefs.HasKey("PlayerX") || !PlayerPrefs.HasKey("PlayerY"))
        {
            continueButton.SetActive(false);
        }

        // ✅ Hide confirmation panel at the start
        if (newGameConfirmationUI != null)
        {
            newGameConfirmationUI.SetActive(false);
        }
    }

    // 🔥 Show Confirmation Panel Instead of Directly Starting a New Game
    public void ShowNewGameConfirmation()
    {
        if (newGameConfirmationUI != null)
        {
            newGameConfirmationUI.SetActive(true);
        }
    }

    // ✅ If Player Clicks "Yes" - Start a New Game
    public void ConfirmNewGame()
    {
        if (newGameConfirmationUI != null)
        {
            newGameConfirmationUI.SetActive(false);
        }

        SaveManager.instance.ResetSave(); // ✅ Delete saved data
        SceneManager.LoadScene("MainLevel"); // ✅ Load the main game scene
    }

    // ❌ If Player Clicks "No" - Cancel Action
    public void CancelNewGame()
    {
        if (newGameConfirmationUI != null)
        {
            newGameConfirmationUI.SetActive(false);
        }
    }

    // ✅ Continue Game Normally
    public void ContinueGame()
    {
        SceneManager.LoadScene("MainLevel"); // ✅ Load last saved position
    }
}
