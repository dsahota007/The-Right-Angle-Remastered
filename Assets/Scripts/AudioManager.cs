using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 🔥 Keeps music playing between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = true;

        // 🔥 Load and apply saved settings
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        audioSource.mute = PlayerPrefs.GetInt("MusicMuted", 0) == 1;

        // 🔥 Make sure we have an audio clip assigned
        if (audioSource.clip == null)
        {
            audioSource.clip = Resources.Load<AudioClip>("Fantasy Ambience FREE TRACK"); // ✅ Put your actual file name here
            audioSource.Play();
        }
    }

    public void ToggleMusic()
    {
        audioSource.mute = !audioSource.mute;
        PlayerPrefs.SetInt("MusicMuted", audioSource.mute ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsMusicMuted()
    {
        return audioSource.mute;
    }
}
