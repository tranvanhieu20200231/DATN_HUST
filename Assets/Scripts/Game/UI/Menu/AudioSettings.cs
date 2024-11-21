using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public TextMeshProUGUI musicVolumeText;
    public TextMeshProUGUI sfxVolumeText;

    private float musicVolume = 1.0f;
    private float sfxVolume = 1.0f;

    [SerializeField] private AudioMixer myMixer;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Start()
    {
        LoadAudioSettings();
        UpdateMusicVolumeText();
        UpdateSFXVolumeText();
    }

    public void IncreaseMusicVolume()
    {
        if (musicVolume < 1.0f)
        {
            musicVolume = Mathf.Min(musicVolume + 0.05f, 1.0f);
            SetMusicVolume();
            UpdateMusicVolumeText();
            SaveAudioSettings();
        }
    }

    public void DecreaseMusicVolume()
    {
        if (musicVolume > 0.0f)
        {
            musicVolume = Mathf.Max(musicVolume - 0.05f, 0.0f);
            SetMusicVolume();
            UpdateMusicVolumeText();
            SaveAudioSettings();
        }
    }

    public void IncreaseSFXVolume()
    {
        if (sfxVolume < 1.0f)
        {
            sfxVolume = Mathf.Min(sfxVolume + 0.05f, 1.0f);
            SetSFXVolume();
            UpdateSFXVolumeText();
            SaveAudioSettings();
        }
    }

    public void DecreaseSFXVolume()
    {
        if (sfxVolume > 0.0f)
        {
            sfxVolume = Mathf.Max(sfxVolume - 0.05f, 0.0f);
            SetSFXVolume();
            UpdateSFXVolumeText();
            SaveAudioSettings();
        }
    }

    private void SetMusicVolume()
    {
        float volume = musicVolume > 0 ? Mathf.Log10(musicVolume) * 20 : -80;
        myMixer.SetFloat("music", volume);
    }

    private void SetSFXVolume()
    {
        float volume = sfxVolume > 0 ? Mathf.Log10(sfxVolume) * 20 : -80;
        myMixer.SetFloat("vfx", volume);
    }

    private void UpdateMusicVolumeText()
    {
        int musicPercentage = Mathf.RoundToInt(musicVolume * 100);
        musicVolumeText.text = $"{musicPercentage} %";
    }

    private void UpdateSFXVolumeText()
    {
        int sfxPercentage = Mathf.RoundToInt(sfxVolume * 100);
        sfxVolumeText.text = $"{sfxPercentage} %";
    }

    private void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, sfxVolume);
        PlayerPrefs.Save();
    }

    private void LoadAudioSettings()
    {
        musicVolume = PlayerPrefs.HasKey(MusicVolumeKey) ? PlayerPrefs.GetFloat(MusicVolumeKey) : 0.5f;
        sfxVolume = PlayerPrefs.HasKey(SFXVolumeKey) ? PlayerPrefs.GetFloat(SFXVolumeKey) : 0.5f;

        SetMusicVolume();
        SetSFXVolume();
    }

}
