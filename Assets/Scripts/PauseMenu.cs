using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject pauseButton;
    public Button musicToggleButton;

    private bool isPaused = false;
    private Rigidbody2D playerRb;

    public GameObject snowEffect;
    public Light2D playerLight;
    public PlayerTrail playerTrailScript;
    public LineRenderer playerLineRenderer;

    private bool isSnowActive = true;
    private bool isLightActive = true;
    private bool isRedLight = false;
    private bool isTrailActive = true;

    void Start()
    {
        playerRb = FindObjectOfType<Rigidbody2D>();

        // 🔥 Ensure Pause Menu is hidden at start
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;

        // ✅ Update Button Text
        UpdateMusicToggleText();
    }

    public void PauseGame()
    {
        if (playerRb != null)
        {
            SaveManager.instance.SavePlayerPosition(playerRb.transform.position);
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

    // 🔥 **Toggle Snow Effect**
    public void ToggleSnow()
    {
        isSnowActive = !isSnowActive;
        if (snowEffect != null)
        {
            snowEffect.SetActive(isSnowActive);
        }
    }

    // 🔥 **Toggle Light Mode**
    public void ToggleLight()
    {
        isLightActive = !isLightActive;
        if (playerLight != null)
        {
            playerLight.enabled = isLightActive;
        }
    }

    public void ToggleRedLight()
    {
        if (playerLight == null) return;

        playerLight.color = isRedLight ? Color.white : Color.red;
        isRedLight = !isRedLight;
    }

    // 🔥 **Toggle Trail Effect On/Off**
    public void ToggleTrail()
    {
        if (playerTrailScript == null || playerLineRenderer == null) return;

        isTrailActive = !isTrailActive;
        playerTrailScript.enabled = isTrailActive;

        if (!isTrailActive)
        {
            playerLineRenderer.positionCount = 0; // ✅ Clears trail instantly
        }
    }

    // 🔥 **Toggle Music Button**
    public void ToggleMusic()
    {
        if (AudioManager.instance == null) return; // Ensure AudioManager exists

        AudioSource audioSource = AudioManager.instance.GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute; // Toggle mute

            // Save state
            PlayerPrefs.SetInt("MusicMuted", audioSource.mute ? 1 : 0);
            PlayerPrefs.Save();
        }
    }


    private void UpdateMusicToggleText()
    {
        if (musicToggleButton != null)
        {
            Text buttonText = musicToggleButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = AudioManager.instance.IsMusicMuted() ? "Music: OFF" : "Music: ON";
            }
        }
    }
}
