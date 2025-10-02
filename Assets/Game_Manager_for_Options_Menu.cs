using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Localization.Settings;
using System;
using FMOD;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.EventSystems;
using UnityEngine.Localization.SmartFormat.Utilities;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Game_Manager_for_Options_Menu : MonoBehaviour
{
    public GameObject OptionsMenu, OtherMenu, VideoMenu, AudioMenu, GeneralMenu, ControlsMenu, Buttons, KeyboardControlsMenu, GamepadControlsMenu;
    public Button Back, VideoBack, AudioBack, GeneralBack, ControlsBack, Video, Audio, General, Controls, GamepadControls, KeyboardControls, GamepadControlsBack, KeyboardControlsBack;
    public TMP_Dropdown ResolutionDropdown, QualityDropdown, OutDevice, LanguageDropdown;
    public Toggle VsyncToggle, WindowToggle;
    public Slider MasterSlider, MusicSlider, SFXSlider;
    //public GameObject OtherManager;
    Resolution[] Resolutions;

    public IntegerData Language, Resolution, Quality, OutputDevice;
    public FloatData MaV, MuV, SV;
    public BooleanData Fullscreen, Vsync;

    private Bus Master, Music, SFX;
    private float MasterVolume, MusicVolume, SFXVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //Adding Listener for each button.
        Back.onClick.AddListener(OnBackButtonClick);
        VideoBack.onClick.AddListener(OnVideoBackButtonClick);
        AudioBack.onClick.AddListener(OnAudioBackButtonClick);
        GeneralBack.onClick.AddListener(OnGeneralBackButtonClick);
        ControlsBack.onClick.AddListener(OnControlsBackButtonClick);
        Video.onClick.AddListener(OnVideoButtonClick);
        Audio.onClick.AddListener(OnAudioButtonClick);
        General.onClick.AddListener(OnGeneralButtonClick);
        Controls.onClick.AddListener(OnControlsButtonClick);
        GamepadControls.onClick.AddListener(OnGamepadControlsButtonClick);
        KeyboardControls.onClick.AddListener(OnKeyboardControlsButtonClick);
        GamepadControlsBack.onClick.AddListener(OnGamepadControlsBackButtonClick);
        KeyboardControlsBack.onClick.AddListener(OnKeyboardControlsBackButtonClick);
        EventSystem.current.SetSelectedGameObject(null);


        //General Options

        //Language
        LanguageDropdown.value = Language.IntgerValue;
        LanguageDropdown.onValueChanged.AddListener(
            delegate
            {
                DropdownValueChanged(LanguageDropdown);
            }
        );

        //Video Options

        //Resolutions
        Resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < Resolutions.Length; i++)
        {
            string option = Resolutions[i].width + "x" + Resolutions[i].height;
            options.Add(option);

            if (Resolutions[i].width == Screen.currentResolution.width && Resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        ResolutionDropdown.value = Resolution.IntgerValue;
        QualityDropdown.value = Quality.IntgerValue;
        WindowToggle.isOn = Fullscreen.BooleanValue;
        VsyncToggle.isOn = Vsync.BooleanValue;


        Screen.fullScreenMode = Fullscreen.BooleanValue == true ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
        QualitySettings.vSyncCount = Vsync.BooleanValue == true ? 1 : 0;

        ResolutionDropdown.onValueChanged.AddListener(OnResolutionDropdownValueChanged);
        QualityDropdown.onValueChanged.AddListener(OnQualityDropdownValueChanged);
        WindowToggle.onValueChanged.AddListener(OnWindowToggleChanged);
        VsyncToggle.onValueChanged.AddListener(OnVSyncToggleChanged);


        //AudioOptions

        //Output Device

        //FMOD.Factory.System_Create(out System);
        String DeviceName;
        int DeviceNameLen = 64;
        Guid guid;
        int systemrate;
        SPEAKERMODE speakermode;
        int speakermodechannels;
        //RESULT temp = System.getNumDrivers(out int numdrivers);
        RESULT temp = RuntimeManager.CoreSystem.getNumDrivers(out int numdrivers);
        OutDevice.ClearOptions();
        List<String> OutDevice_Options = new List<String> { };
        //UnityEngine.Debug.Log(numdrivers - 1);
        //UnityEngine.Debug.Log(temp);

        for (int i = 0; i < numdrivers - 1; i++)
        {
            RuntimeManager.CoreSystem.getDriverInfo(i, out DeviceName, DeviceNameLen, out guid, out systemrate, out speakermode, out speakermodechannels);
            OutDevice_Options.Add(DeviceName);
        }
        OutDevice.AddOptions(OutDevice_Options);

        OutDevice.value = OutputDevice.IntgerValue;
        OutDevice.onValueChanged.AddListener(
            delegate
            {
                OutputDeviceDropdownValueChanged(OutDevice);
            }
        );


        //Sliders

        Master = RuntimeManager.GetBus("bus:/Master");
        Music = RuntimeManager.GetBus("bus:/Master/Music");
        SFX = RuntimeManager.GetBus("bus:/Master/SFX");

        Master.getVolume(out float MasterVolume);
        Music.getVolume(out float MusicVolume);
        SFX.getVolume(out float SFXVolume);

        MasterSlider.onValueChanged.AddListener(delegate { MasterVolumeLevel(); });
        MusicSlider.onValueChanged.AddListener(delegate { MusicVolumeLevel(); });
        SFXSlider.onValueChanged.AddListener(delegate { SFXVolumeLevel(); });

        MasterSlider.value = MasterVolume / MasterSlider.maxValue;
        MusicSlider.value = MusicVolume / MusicSlider.maxValue;
        SFXSlider.value = SFXVolume / SFXSlider.maxValue;

        MaV.FloatValue = MasterVolume;
        MuV.FloatValue = MusicVolume;
        SV.FloatValue = SFXVolume;

        MasterSlider.value = MasterVolume;
        MusicSlider.value = MusicVolume;
        SFXSlider.value = SFXVolume;

        //ControlsOptions
    }

    void OnBackButtonClick()
    {
        OptionsMenu.SetActive(false);
        OtherMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        //OtherManager.SetActive(true);
    }
    void OnVideoButtonClick()
    {
        VideoMenu.SetActive(true);
        Buttons.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnVideoBackButtonClick()
    {
        VideoMenu.SetActive(false);
        Buttons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);

    }
    void OnAudioButtonClick()
    {
        AudioMenu.SetActive(true);
        Buttons.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnAudioBackButtonClick()
    {
        AudioMenu.SetActive(false);
        Buttons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnGeneralButtonClick()
    {
        GeneralMenu.SetActive(true);
        Buttons.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnGeneralBackButtonClick()
    {
        GeneralMenu.SetActive(false);
        Buttons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }
    void OnControlsButtonClick()
    {
        ControlsMenu.SetActive(true);
        Buttons.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnControlsBackButtonClick()
    {
        ControlsMenu.SetActive(false);
        Buttons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }


    void OnGamepadControlsButtonClick()
    {
        ControlsMenu.SetActive(false);
        GamepadControlsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnKeyboardControlsButtonClick()
    {
        ControlsMenu.SetActive(false);
        KeyboardControlsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnGamepadControlsBackButtonClick()
    {
        ControlsMenu.SetActive(true);
        GamepadControlsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnKeyboardControlsBackButtonClick()
    {
        ControlsMenu.SetActive(true);
        KeyboardControlsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnVSyncToggleChanged(bool isOn)
    {
        if (isOn)
        {
            //Debug.Log("Toggle is ON!");
            QualitySettings.vSyncCount = 1;
            Vsync.BooleanValue = true;
        }
        else
        {
            //Debug.Log("Toggle is OFF!");
            QualitySettings.vSyncCount = 0;
            Vsync.BooleanValue = false;
        }

    }
    void OnWindowToggleChanged(bool isOn)
    {
        //Debug.Log("Selected option: " + index);
        if (isOn)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Fullscreen.BooleanValue = true;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Fullscreen.BooleanValue = false;
        }
    }

    void OnResolutionDropdownValueChanged(int index)
    {
        //Debug.Log("Selected option: " + index);
        Resolution reso = Resolutions[index];
        Screen.SetResolution(reso.width, reso.height, Screen.fullScreen);
        Resolution.IntgerValue = index;
    }

    void OnQualityDropdownValueChanged(int index)
    {
        //Debug.Log("Selected option: " + index);
        QualitySettings.SetQualityLevel(index, true);
        Quality.IntgerValue = index;
    }


    IEnumerator SetLocale(int _localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
    }

    void DropdownValueChanged(TMP_Dropdown change)
    {
        //Debug.Log(change.value);
        StartCoroutine(SetLocale(change.value));
        Language.IntgerValue = change.value;
    }

    void OutputDeviceDropdownValueChanged(TMP_Dropdown change)
    {
        //UnityEngine.Debug.Log(change.value);
        RuntimeManager.CoreSystem.setDriver(change.value);
        OutputDevice.IntgerValue = change.value;
    }

    public void MasterVolumeLevel()
    {
        MasterVolume = MasterSlider.value;
        Master.setVolume(MasterSlider.value / MasterSlider.maxValue);
        MaV.FloatValue = MasterVolume;
        //Debug.LogError("MasterVolume: " + MasterVolume + "\n");
    }

    public void MusicVolumeLevel()
    {
        MusicVolume = MusicSlider.value;
        Music.setVolume(MusicSlider.value / MusicSlider.maxValue);
        MuV.FloatValue = MusicVolume;
        //Debug.LogError("MusicVolume: " + MusicVolume + "\n");
    }

    public void SFXVolumeLevel()
    {
        SFXVolume = SFXSlider.value;
        SFX.setVolume(SFXSlider.value / SFXSlider.maxValue);
        SV.FloatValue = SFXVolume;
        //Debug.LogError("SFXVolume: " + SFXVolume + "\n");
    }


    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Debug.Log("MasterVolume:\t" + MasterVolume);
        //UnityEngine.Debug.Log("MusicVolume:\t" + MusicVolume);
        //UnityEngine.Debug.Log("SFXVolume:\t" + SFXVolume);
        //Master.setVolume(MasterVolume / MasterSlider.maxValue);
        //Music.setVolume(MusicVolume / MusicSlider.maxValue);
        //SFX.setVolume(SFXVolume / SFXSlider.maxValue);
        if (Buttons.activeSelf)
        {
            if ((InputManager.instance.UpButton || InputManager.instance.DownButton) && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(General.gameObject);
            }
        }
        else if (GeneralMenu.activeSelf)
        {
            if ((InputManager.instance.UpButton || InputManager.instance.DownButton) && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(LanguageDropdown.gameObject);
            }
        }
        else if (VideoMenu.activeSelf)
        {
            if ((InputManager.instance.UpButton || InputManager.instance.DownButton) && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(ResolutionDropdown.gameObject);
            }
        }
        else if (AudioMenu.activeSelf)
        {
            if ((InputManager.instance.UpButton || InputManager.instance.DownButton) && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(OutDevice.gameObject);
            }
        }
        else if (ControlsMenu.activeSelf)
        {
            if ((InputManager.instance.UpButton || InputManager.instance.DownButton) && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(GamepadControls.gameObject);
            }
        }
        else if (GamepadControlsMenu.activeSelf)
        {

        }
        else if (KeyboardControlsMenu.activeSelf)
        {

        }
    }
}