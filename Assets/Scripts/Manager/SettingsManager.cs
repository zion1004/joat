using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;

    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private Resolution[] resolutions;

    private const string QUALITY_KEY = "Graphics_Quality";
    private const string RESOLUTION_KEY = "Graphics_Resolution";

    private const string MASTER_VOL_KEY = "Audio_Master";
    private const string MUSIC_VOL_KEY = "Audio_Music";
    private const string SFX_VOL_KEY = "Audio_SFX";


    void Start() {
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));

        int savedQuality = PlayerPrefs.GetInt(QUALITY_KEY, QualitySettings.GetQualityLevel());
        qualityDropdown.value = savedQuality;
        QualitySettings.SetQualityLevel(savedQuality);

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string>();
        int savedResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_KEY, resolutions.Length - 1);

        for(int i = 0; i < resolutions.Length; i++) {
            Resolution res = resolutions[i];
            string option = res.width + " x " + res.height + " @ " + Mathf.RoundToInt((float)res.refreshRateRatio.value) + "Hz";
            options.Add(option);
        }

        float masterVol = PlayerPrefs.GetFloat(MASTER_VOL_KEY, 20f);
        float musicVol = PlayerPrefs.GetFloat(MUSIC_VOL_KEY, 20f);
        float sfxVol = PlayerPrefs.GetFloat(SFX_VOL_KEY, 20f);

        audioMixer.SetFloat("MasterVolume", masterVol);
        audioMixer.SetFloat("MusicVolume", musicVol);
        audioMixer.SetFloat("SFXVolume", sfxVol);

        masterSlider.value = Mathf.Pow(10f, masterVol / 20f);
        musicSlider.value = Mathf.Pow(10f, musicVol / 20f);
        sfxSlider.value = Mathf.Pow(10f, sfxVol / 20f);

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        ApplySettings();

        qualityDropdown.onValueChanged.AddListener(SetQuality);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetQuality(int index) {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt(QUALITY_KEY, index);
        PlayerPrefs.Save();
    }

    public void SetResolution(int index) {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRateRatio);
        PlayerPrefs.SetInt(RESOLUTION_KEY, index);
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value) {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f;
        audioMixer.SetFloat("MasterVolume", dB);
        PlayerPrefs.SetFloat(MASTER_VOL_KEY, dB);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value) {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f;
        audioMixer.SetFloat("MusicVolume", dB);
        PlayerPrefs.SetFloat(MUSIC_VOL_KEY, dB);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value) {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f;
        audioMixer.SetFloat("SFXVolume", dB);
        PlayerPrefs.SetFloat(SFX_VOL_KEY, dB);
        PlayerPrefs.Save();
    }

    void ApplySettings() {
        SetQuality(qualityDropdown.value);
        SetResolution(resolutionDropdown.value);

        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }
}
