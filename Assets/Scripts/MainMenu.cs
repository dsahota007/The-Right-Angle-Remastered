using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject continueButton;

    void Start()
    {
        // 🔥 Ensure Time is running at normal speed when returning to menu
        Time.timeScale = 1f;

        // Disable Continue button if no save exists
        if (!PlayerPrefs.HasKey("PlayerX") || !PlayerPrefs.HasKey("PlayerY"))
        {
            continueButton.SetActive(false);
        }
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        SceneManager.LoadScene("MainLevel");
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
        {
            SceneManager.LoadScene("MainLevel");
        }
    }
}
