using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "OptionsMenuData", menuName = "Scriptable Objects/OptionsMenuData")]
public class OptionsMenuData : ScriptableObject
{
    //General:
    [SerializeField]
    private int LanguageDropdownValue;


    //Video:
    [SerializeField]
    private int ResolutionDropdown;
    [SerializeField]
    private int QualityDropdown;
    [SerializeField]
    private bool FullscreenToggle;
    [SerializeField]
    private bool VsyncToggle;

    //Audio:
    [SerializeField]
    private int OutputDeviceDropdown;
    [SerializeField]
    private float MasterVolumeSlider;
    [SerializeField]
    private float MusicVolumeSlider;
    [SerializeField]
    private float SFXVolumeSlider;

    
}
