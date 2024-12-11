using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DisplaySettings : MonoBehaviour
{
    public TextMeshProUGUI resolutionText;
    public TextMeshProUGUI brightnessText;
    public Light2D globalLight;

    private Resolution[] availableResolutions;
    private int currentResolutionIndex;
    private float brightness = 0.25f;

    void Start()
    {
        availableResolutions = Screen.resolutions;
        currentResolutionIndex = GetCurrentResolutionIndex();
        UpdateResolutionText();
        UpdateBrightnessText();
        LoadSettings();
        SetBrightness();
    }

    public void IncreaseResolution()
    {
        if (currentResolutionIndex < availableResolutions.Length - 1)
        {
            currentResolutionIndex++;
            ApplyResolution();
            SaveSettings();
        }
    }

    public void DecreaseResolution()
    {
        if (currentResolutionIndex > 0)
        {
            currentResolutionIndex--;
            ApplyResolution();
            SaveSettings();
        }
    }

    public void IncreaseBrightness()
    {
        if (brightness < 1.0f)
        {
            brightness = Mathf.Min(brightness + 0.05f, 1.0f);
            UpdateBrightnessText();
            SetBrightness();
            SaveSettings();
        }
    }

    public void DecreaseBrightness()
    {
        if (brightness > 0.05f)
        {
            brightness = Mathf.Max(brightness - 0.05f, 0.05f);
            UpdateBrightnessText();
            SetBrightness();
            SaveSettings();
        }
    }

    private void ApplyResolution()
    {
        Resolution resolution = availableResolutions[currentResolutionIndex];
        bool isFullScreen = resolution.width >= Screen.currentResolution.width && resolution.height >= Screen.currentResolution.height;
        Screen.SetResolution(resolution.width, resolution.height, isFullScreen);
        UpdateResolutionText();
        SaveSettings();
    }

    private void UpdateResolutionText()
    {
        Resolution resolution = availableResolutions[currentResolutionIndex];
        resolutionText.text = $"{resolution.width}x{resolution.height}";
    }

    private void UpdateBrightnessText()
    {
        int brightnessPercentage = Mathf.RoundToInt(brightness * 100);
        brightnessText.text = $"{brightnessPercentage} %";
    }

    private void SetBrightness()
    {
        globalLight.intensity = brightness;
        RenderSettings.ambientLight = Color.white * brightness;
    }

    private int GetCurrentResolutionIndex()
    {
        Resolution currentResolution = Screen.currentResolution;
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            if (availableResolutions[i].width == currentResolution.width && availableResolutions[i].height == currentResolution.height)
            {
                return i;
            }
        }
        return 0;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
        PlayerPrefs.SetFloat("Brightness", brightness);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
        }
        brightness = PlayerPrefs.GetFloat("Brightness", 0.25f);
        UpdateBrightnessText();
        SetBrightness();
    }
}
