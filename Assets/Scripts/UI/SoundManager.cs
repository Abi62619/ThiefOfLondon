using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Volume Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider UiSoundSlider;
    [SerializeField] private Slider voiceSlider;

    [Header("Volume Labels")]
    [SerializeField] private TextMeshProUGUI masterLabel;
    [SerializeField] private TextMeshProUGUI musicLabel;
    [SerializeField] private TextMeshProUGUI sfxLabel;
    [SerializeField] private TextMeshProUGUI ambientLabel;
    [SerializeField] private TextMeshProUGUI voiceLabel;

    private const string MASTER_VOLUME = "MasterVolume";
    private const string MUSIC_VOLUME = "MusicVolume";
    private const string SFX_VOLUME = "SFXVolume";
    private const string AMBIENT_VOLUME = "UiSoundVolume";
    private const string VOICE_VOLUME = "DialogeVolume";

    void Start()
    {
        // Load saved values and initialize sliders
        InitializeVolume(MASTER_VOLUME, masterSlider, masterLabel);
        InitializeVolume(MUSIC_VOLUME, musicSlider, musicLabel);
        InitializeVolume(SFX_VOLUME, sfxSlider, sfxLabel);
        InitializeVolume(AMBIENT_VOLUME, UiSoundSlider, ambientLabel);
        InitializeVolume(VOICE_VOLUME, voiceSlider, voiceLabel);

        // Add listeners to update volume when sliders change
        masterSlider.onValueChanged.AddListener(value => SetVolume(MASTER_VOLUME, value, masterLabel));
        musicSlider.onValueChanged.AddListener(value => SetVolume(MUSIC_VOLUME, value, musicLabel));
        sfxSlider.onValueChanged.AddListener(value => SetVolume(SFX_VOLUME, value, sfxLabel));
        UiSoundSlider.onValueChanged.AddListener(value => SetVolume(AMBIENT_VOLUME, value, ambientLabel));
        voiceSlider.onValueChanged.AddListener(value => SetVolume(VOICE_VOLUME, value, voiceLabel));
    }

    private void InitializeVolume(string volumeParameter, Slider slider, TextMeshProUGUI label)
    {
        float savedVolume = PlayerPrefs.GetFloat(volumeParameter, 0.75f); // Default to 0.75
        slider.value = savedVolume;
        SetVolume(volumeParameter, savedVolume, label);
    }

    private void SetVolume(string volumeParameter, float sliderValue, TextMeshProUGUI label)
    {
        float volume = Mathf.Lerp(-80, 0, sliderValue); // Linear interpolation from -80 dB to 0 dB
        audioMixer.SetFloat(volumeParameter, volume);
        label.text = $"{Mathf.RoundToInt(sliderValue * 100)}%";

        // Save the volume setting
        PlayerPrefs.SetFloat(volumeParameter, sliderValue);
    }

    private void OnDisable()
    {
        // Save all settings when the object is disabled
        PlayerPrefs.Save();
    }
}
