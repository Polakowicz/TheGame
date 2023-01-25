using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;
    // public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public Slider volumeSlider;
    public Slider effectsVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider voiceVolumeSlider;
    public AudioMixer mainMixer;
    public AudioMixer effectsMixer;
    public AudioMixer musicMixer;
    public AudioMixer voiceMixer;

    void Start()
    {
        initiateResolution();
        initiateVolumes();
    }

    public void SetVolume(float decimalVolume)
    {
        var dbVolume = decimalToDecibel(decimalVolume);
        mainMixer.SetFloat("volume", dbVolume);
    }

    public void SetEffectsVolume(float decimalVolume)
    {
        var dbVolume = decimalToDecibel(decimalVolume);
        effectsMixer.SetFloat("effectsVolume", dbVolume);
    }

    public void SetMusicVolume(float decimalVolume)
    {
        var dbVolume = decimalToDecibel(decimalVolume);
        musicMixer.SetFloat("musicVolume", dbVolume);
    }

    public void SetVoiceVolume(float decimalVolume)
    {
        var dbVolume = decimalToDecibel(decimalVolume);
        voiceMixer.SetFloat("voiceVolume", dbVolume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("I just changed resolution!");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("I just switched fullscreen!");
    }

    void initiateResolution()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + "@" + resolutions[i].refreshRate + " Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void initiateVolumes()
    {
        // TO DO
    }

    float decimalToDecibel(float decimalVolume)
    {
        var dbVolume = Mathf.Log10(decimalVolume) * 20;
        if (decimalVolume == 0.0f)
        {
            dbVolume = -80.0f;
        }
        return dbVolume;
    }

}