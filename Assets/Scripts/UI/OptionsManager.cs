/*
    This script populates the drop downs in the option menu and sets the variable optionsMenu in UI Manager to the object this is attached to.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [Header("Sound Objects")]
    public AudioMixer audioMixer;
    [Header("Menu Components")]
    [SerializeField]
    private Dropdown resolutionDropDown;
    private int currentResolutionIndex;
    private int selectedResolution;
    [SerializeField]
    private Dropdown qualityDropDown;
    [SerializeField]
    private Slider masterSlider;
    private float currentMasterVolume;
    [SerializeField]
    private Slider soundSlider;
    private float currentSoundVolume;
    [SerializeField]
    private Slider musicSlider;
    private float currentMusicVolume;
    [SerializeField]
    private Toggle fullScreenToggle;
    [SerializeField]
    private Button applyButton;

    private void Awake()
    {
        //clear list options
        resolutionDropDown.ClearOptions();
        //create a new list of strings to hold resolutions
        List<string> resolutions = new List<string>();
        //loop through all the resolutions Unity knows of
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            //add the width and height to our resolutions list as a concatenated string
            resolutions.Add(string.Format("{0} x {1}", Screen.resolutions[i].width, Screen.resolutions[i].height));
            //if the screen resolution we are adding is equal in width and height to our current screen height and width
            if (Screen.resolutions[i].width == Screen.currentResolution.width && Screen.resolutions[i].height == Screen.currentResolution.height)
            {
                //that is our current resolution
                currentResolutionIndex = i;
            }
        }
        //add the resolution strings we just created to our list
        resolutionDropDown.AddOptions(resolutions);
        //clear the quality drop down list
        qualityDropDown.ClearOptions();
        //add all the quality setting names to this list
        qualityDropDown.AddOptions(QualitySettings.names.ToList());
    }
    private void Start()
    {
        
    }

    public void OnEnable()
    {
        //load all settings
        LoadSettings();
        //make the apply button not interactable
        applyButton.interactable = false;
    }

    //TODO: Add Options menu functionality, make the sliders actually do stuff and the apply botton actually apply.
    public void ActivateApplyButton()
    {
        applyButton.interactable = true;
    }

    public void SaveSettings()
    {
        //save all our settings using player prefs that we need to
        PlayerPrefs.SetFloat("Master Volume", currentMasterVolume);
        PlayerPrefs.SetFloat("Sound Volume", currentSoundVolume);
        PlayerPrefs.SetFloat("Music Volume", currentMusicVolume);
        PlayerPrefs.SetInt("Resolution Preference", resolutionDropDown.value);
        PlayerPrefs.SetInt("Fullscreen Preference", Convert.ToInt32(fullScreenToggle));
        PlayerPrefs.SetInt("Quality Preference", qualityDropDown.value);
        //set the screen resolution to the selected resolution
        SetResolution(selectedResolution);
        //set the screen mode according to the toggle
        SetWindowMode();
        //set the quality settings according to the drop down value
        QualitySettings.SetQualityLevel(qualityDropDown.value);
    }

    public void LoadSettings()
    {
        //we first check to see if the key exists for each player pref setting that was saved
        //if it is we load it, if not we default to a set value
        if (PlayerPrefs.HasKey("Master Volume"))
        {
            PlayerPrefs.GetFloat("Master Volume", masterSlider.maxValue);
        }
        else
        {
            masterSlider.value = 0;
        }
        if (PlayerPrefs.HasKey("Sound Volume"))
        {
            PlayerPrefs.GetFloat("Sound Volume", soundSlider.maxValue);
        }
        else
        {
            soundSlider.value = 0;
        }
        if (PlayerPrefs.HasKey("Music Volume"))
        {
            PlayerPrefs.GetFloat("Music Volume", musicSlider.maxValue);
        }
        else
        {
            musicSlider.value = 0;
        }
        if (PlayerPrefs.HasKey("Resolution Preference"))
        {
            PlayerPrefs.GetInt("Resolution Preference", currentResolutionIndex);
        }
        else
        {
            resolutionDropDown.value = currentResolutionIndex;
        }
        if (PlayerPrefs.HasKey("Fullscreen Preference"))
        {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen Preference"));
        }
        else
        {
            Screen.fullScreen = true;
        }
        if (PlayerPrefs.HasKey("Quality Preference"))
        {
            PlayerPrefs.GetInt("Quality Preference", qualityDropDown.value);
        }
        else
        {
            qualityDropDown.value = QualitySettings.GetQualityLevel();
        }
    }

    public void SetMasterVolume(float volume)
    {
        //set the float labelled Master Volume in the audio mixer with the value of volume
        audioMixer.SetFloat("Master Volume", volume);
        //set current volume also equal to this value
        currentMasterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        //set the float labelled Music Volume in audio mixer with the value of volume
        audioMixer.SetFloat("Music Volume", volume);
        //set current volume also equal to this value
        currentMusicVolume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        //set the float labelled Sound Volume in audio mixer with the value of volume
        audioMixer.SetFloat("Sound Volume", volume);
        //set current volume also equal to this value
        currentSoundVolume = volume;
    }

    public void SelectResolution()
    {
        //set our selected resolution variable equal to the value of drop down for later use
        //in setting the resolution when the user hits the apply button
        selectedResolution = resolutionDropDown.value;
    }

    public void SetResolution(int resolutionIndex)
    {
        //create a new resolution equal to the resolution index passed in from selectedIndex
        Resolution resolution = Screen.resolutions[resolutionIndex];
        //set our user's screen resolution to it
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //set our own current resolution index var to the one we just set our screen to
        currentResolutionIndex = resolutionIndex;
    }

    public void SetWindowMode()
    {
        //if the toggle is toggled on
        if (fullScreenToggle.isOn == true)
        {
            //set the screen mode to full screen
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        //else if it is off
        else if (fullScreenToggle.isOn == false)
        {
            //set it to windowed mode
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
