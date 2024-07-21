using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    public static AudioSettingsController Instance;
    public AudioSource audioSource;
    public Slider volumeSlider;
    public Toggle muteToggle;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioSource == null)
        {

            audioSource = FindObjectOfType<AudioSource>();
        }

        volumeSlider.value = audioSource.volume;
        muteToggle.isOn = !audioSource.mute;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteToggle.onValueChanged.AddListener(SetMute);
    }

    void SetVolume(float value)
    {
        audioSource.volume = value;
    }

    void SetMute(bool isMuted)
    {
        audioSource.mute = !isMuted;
    }
}
