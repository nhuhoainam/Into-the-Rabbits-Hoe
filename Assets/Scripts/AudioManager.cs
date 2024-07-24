using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    public static AudioSettingsController Instance;

    // Âm thanh nhạc nền
    public AudioSource musicAudioSource;
    public Slider musicVolumeSlider;
    public Toggle musicMuteToggle;

    // Âm thanh hiệu ứng
    public AudioSource sfxAudioSource;
    public Slider sfxVolumeSlider;
    public Toggle sfxMuteToggle;

    // void Awake()
    // {

    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    void Start()
    {

        if (musicAudioSource != null)
        {
            musicVolumeSlider.value = musicAudioSource.volume;
            musicMuteToggle.isOn = !musicAudioSource.mute;
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            musicMuteToggle.onValueChanged.AddListener(SetMusicMute);
        }

        if (sfxAudioSource != null)
        {
            sfxVolumeSlider.value = sfxAudioSource.volume;
            sfxMuteToggle.isOn = !sfxAudioSource.mute;
            sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
            sfxMuteToggle.onValueChanged.AddListener(SetSfxMute);
        }
    }

    void SetMusicVolume(float value)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = value;
        }
    }

    void SetMusicMute(bool isMuted)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.mute = !isMuted;
        }
    }

    void SetSfxVolume(float value)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = value;
        }
    }

    void SetSfxMute(bool isMuted)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.mute = !isMuted;
        }
    }
}
