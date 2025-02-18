using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject pauseButton;
    private bool isPaused = false;
    private Rigidbody2D playerRb;

    void Start()
    {
        playerRb = FindObjectOfType<Rigidbody2D>();

        // 🔥 **Ensure Pause Menu is completely hidden at game start**
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);

        // 🔥 **Ensure game is running normally after loading from Main Menu**
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (playerRb != null)
        {
            SaveManager.instance.SavePlayerPosition(playerRb.transform.position); // Auto-save on pause
        }

        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        isPaused = false;
    }

    public void SaveAndReturnToMainMenu()
    {
        if (playerRb != null)
        {
            SaveManager.instance.SavePlayerPosition(playerRb.transform.position);
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevelBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenuBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
