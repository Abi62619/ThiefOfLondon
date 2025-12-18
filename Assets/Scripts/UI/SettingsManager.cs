using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SettingsManager : MonoBehaviour
{
    [Header("Resolution Settings")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Graphics Settings")]
    [SerializeField] private Slider lodSlider;
    [SerializeField] private TextMeshProUGUI lodSliderLabel;
    [SerializeField] private Slider shadowQualitySlider;
    [SerializeField] private TextMeshProUGUI shadowQualityLabel;

    private List<Resolution> uniqueResolutions;
    private bool isFullscreen;

    void Start()
    {
        SetupResolutionSettings();
        SetupLODSlider();
        SetupShadowQualitySlider();
    }

    // Sets up the resolution dropdown and fullscreen toggle
    private void SetupResolutionSettings()
    {
        // Get unique resolutions based only on width and height
        uniqueResolutions = Screen.resolutions
            .GroupBy(res => new { res.width, res.height })
            .Select(group => group.First())
            .ToList();

        PopulateResolutionDropdown();
        SetupFullscreenToggle();
    }

    // Populates the dropdown with available unique resolutions
    private void PopulateResolutionDropdown()
    {
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < uniqueResolutions.Count; i++)
        {
            Resolution res = uniqueResolutions[i];
            string option = $"{res.width} x {res.height}";
            options.Add(option);

            // Set current resolution as the default selected item
            if (res.width == Screen.currentResolution.width &&
                res.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Add listener for the dropdown
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    // Sets up fullscreen toggle initial state and listener
    private void SetupFullscreenToggle()
    {
        isFullscreen = Screen.fullScreen;
        fullscreenToggle.isOn = isFullscreen;

        // Add listener for the toggle
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    // Method to change resolution based on dropdown selection
    private void SetResolution(int resolutionIndex)
    {
        Resolution selectedResolution = uniqueResolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullscreen);
    }

    // Method to toggle fullscreen mode
    private void SetFullscreen(bool fullscreen)
    {
        isFullscreen = fullscreen;
        Screen.fullScreen = isFullscreen;
    }

    // Sets up the LOD slider and listener
    private void SetupLODSlider()
    {
        lodSlider.minValue = 0.5f; // Minimum LOD Bias
        lodSlider.maxValue = 2.0f; // Maximum LOD Bias
        lodSlider.value = QualitySettings.lodBias; // Set to current LOD Bias
        UpdateLODLabel(lodSlider.value);

        lodSlider.onValueChanged.AddListener(value =>
        {
            QualitySettings.lodBias = value;
            UpdateLODLabel(value);
        });
    }

    // Updates the LOD label with the current slider value
    private void UpdateLODLabel(float value)
    {
        lodSliderLabel.text = $"LOD Bias: {value:F1}";
    }

    // Sets up the Shadow Quality slider and listener
    private void SetupShadowQualitySlider()
    {
        shadowQualitySlider.minValue = 0; // Low Quality
        shadowQualitySlider.maxValue = 2; // High Quality
        shadowQualitySlider.wholeNumbers = true;
        shadowQualitySlider.value = (int)QualitySettings.shadows; // Set to current shadow quality
        UpdateShadowQualityLabel((int)shadowQualitySlider.value);

        shadowQualitySlider.onValueChanged.AddListener(value =>
        {
            SetShadowQuality((int)value);
            UpdateShadowQualityLabel((int)value);
        });
    }

    // Sets the shadow quality based on slider value
    private void SetShadowQuality(int value)
    {
        switch (value)
        {
            case 0:
                QualitySettings.shadows = ShadowQuality.Disable;
                break;
            case 1:
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break;
            case 2:
                QualitySettings.shadows = ShadowQuality.All;
                break;
        }
    }

    // Updates the Shadow Quality label with the current setting
    private void UpdateShadowQualityLabel(int value)
    {
        string quality = value switch
        {
            0 => "Disabled",
            1 => "Hard Shadows",
            2 => "Soft Shadows",
            _ => "Unknown"
        };
        shadowQualityLabel.text = $"Shadows: {quality}";
    }
}
