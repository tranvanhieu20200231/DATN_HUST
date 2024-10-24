using TMPro;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public TextMeshProUGUI musicVolumeText;  // TextMeshPro hiển thị âm lượng Music
    public TextMeshProUGUI sfxVolumeText;    // TextMeshPro hiển thị âm lượng SFX

    private float musicVolume = 1.0f; // Âm lượng Music hiện tại (100%)
    private float sfxVolume = 1.0f;   // Âm lượng SFX hiện tại (100%)

    private AudioSource musicSource;  // Nguồn phát âm thanh của Music
    private AudioSource sfxSource;    // Nguồn phát âm thanh của SFX

    void Start()
    {
        // Lấy AudioSource cho Music và SFX (có thể set trong Inspector)
        // musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        // sfxSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();

        UpdateMusicVolumeText();
        UpdateSFXVolumeText();
    }

    // Hàm tăng âm lượng Music lên 5%
    public void IncreaseMusicVolume()
    {
        if (musicVolume < 1.0f)
        {
            musicVolume = Mathf.Min(musicVolume + 0.05f, 1.0f); // Không vượt quá 100%
            //SetMusicVolume();
            UpdateMusicVolumeText();
        }
    }

    // Hàm giảm âm lượng Music xuống 5%
    public void DecreaseMusicVolume()
    {
        if (musicVolume > 0.0f)
        {
            musicVolume = Mathf.Max(musicVolume - 0.05f, 0.0f); // Không xuống dưới 0%
            //SetMusicVolume();
            UpdateMusicVolumeText();
        }
    }

    // Hàm tăng âm lượng SFX lên 5%
    public void IncreaseSFXVolume()
    {
        if (sfxVolume < 1.0f)
        {
            sfxVolume = Mathf.Min(sfxVolume + 0.05f, 1.0f); // Không vượt quá 100%
            //SetSFXVolume();
            UpdateSFXVolumeText();
        }
    }

    // Hàm giảm âm lượng SFX xuống 5%
    public void DecreaseSFXVolume()
    {
        if (sfxVolume > 0.0f)
        {
            sfxVolume = Mathf.Max(sfxVolume - 0.05f, 0.0f); // Không xuống dưới 0%
            //SetSFXVolume();
            UpdateSFXVolumeText();
        }
    }

    // Hàm cài đặt âm lượng cho Music
    private void SetMusicVolume()
    {
        musicSource.volume = musicVolume;
    }

    // Hàm cài đặt âm lượng cho SFX
    private void SetSFXVolume()
    {
        sfxSource.volume = sfxVolume;
    }

    // Cập nhật TextMeshPro hiển thị âm lượng Music
    private void UpdateMusicVolumeText()
    {
        int musicPercentage = Mathf.RoundToInt(musicVolume * 100);
        musicVolumeText.text = $"{musicPercentage} %";
    }

    // Cập nhật TextMeshPro hiển thị âm lượng SFX
    private void UpdateSFXVolumeText()
    {
        int sfxPercentage = Mathf.RoundToInt(sfxVolume * 100);
        sfxVolumeText.text = $"{sfxPercentage} %";
    }
}
