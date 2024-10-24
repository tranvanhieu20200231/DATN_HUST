using TMPro;
using UnityEngine;

public class DisplaySettings : MonoBehaviour
{
    public TextMeshProUGUI resolutionText;
    public TextMeshProUGUI brightnessText;

    private Resolution[] availableResolutions;
    private int currentResolutionIndex;
    private float brightness = 1.0f;

    void Start()
    {
        // Lấy danh sách các độ phân giải khả dụng
        availableResolutions = Screen.resolutions;
        currentResolutionIndex = GetCurrentResolutionIndex();
        UpdateResolutionText();
        UpdateBrightnessText();
    }

    // Hàm để tăng độ phân giải
    public void IncreaseResolution()
    {
        if (currentResolutionIndex < availableResolutions.Length - 1)
        {
            currentResolutionIndex++;
            ApplyResolution();
        }
    }

    // Hàm để giảm độ phân giải
    public void DecreaseResolution()
    {
        if (currentResolutionIndex > 0)
        {
            currentResolutionIndex--;
            ApplyResolution();
        }
    }

    // Hàm để tăng độ sáng lên 5%
    public void IncreaseBrightness()
    {
        if (brightness < 1.0f)
        {
            brightness = Mathf.Min(brightness + 0.05f, 1.0f); // Không vượt quá 100%
            UpdateBrightnessText();
            SetBrightness();
        }
    }

    // Hàm để giảm độ sáng xuống 5%
    public void DecreaseBrightness()
    {
        if (brightness > 0.05f)
        {
            brightness = Mathf.Max(brightness - 0.05f, 0.05f); // Không xuống dưới 5%
            UpdateBrightnessText();
            SetBrightness();
        }
    }

    // Hàm cài đặt độ phân giải
    private void ApplyResolution()
    {
        Resolution resolution = availableResolutions[currentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        UpdateResolutionText();
    }

    // Hàm để cập nhật TextMeshPro hiển thị độ phân giải
    private void UpdateResolutionText()
    {
        Resolution resolution = availableResolutions[currentResolutionIndex];
        resolutionText.text = $"{resolution.width}x{resolution.height}";
    }

    // Hàm để cập nhật TextMeshPro hiển thị % độ sáng
    private void UpdateBrightnessText()
    {
        int brightnessPercentage = Mathf.RoundToInt(brightness * 100);
        brightnessText.text = $"{brightnessPercentage} %";
    }

    // Hàm cài đặt độ sáng
    private void SetBrightness()
    {
        // Ở đây bạn có thể sử dụng độ sáng để điều chỉnh các cài đặt khác, ví dụ shader hoặc post-processing
        RenderSettings.ambientLight = Color.white * brightness; // Ví dụ thay đổi ánh sáng môi trường
    }

    // Lấy chỉ số của độ phân giải hiện tại
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
        return 0; // Trả về độ phân giải mặc định nếu không tìm thấy
    }
}
