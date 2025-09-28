using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using TMPro;
public class Sound_Manager_For_Options_Menu : MonoBehaviour
{
    FMOD.Studio.EventInstance
                            ButtonSound,
                            DropdownSound,
                            ToggleSound,
                            SliderSound;
    public Button[] buttons;
    public TMP_Dropdown[] dropdowns;
    public Toggle[] toggles;
    public Slider[] sliders;
    public GameObject a;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ButtonSound = RuntimeManager.CreateInstance("event:/Master/SFX/ButtonPressed");
        RuntimeManager.AttachInstanceToGameObject(ButtonSound, a.transform, false);

        DropdownSound = RuntimeManager.CreateInstance("event:/Master/SFX/ButtonPressed");
        RuntimeManager.AttachInstanceToGameObject(DropdownSound, a.transform, false);

        ToggleSound = RuntimeManager.CreateInstance("event:/Master/SFX/ButtonPressed");
        RuntimeManager.AttachInstanceToGameObject(ToggleSound, a.transform, false);

        SliderSound = RuntimeManager.CreateInstance("event:/Master/SFX/ButtonPressed");
        RuntimeManager.AttachInstanceToGameObject(SliderSound, a.transform, false);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(ButtonListener);
        }

        for (int i = 0; i < dropdowns.Length; i++)
        {
            dropdowns[i].onValueChanged.AddListener(DropdownListener);
        }

        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].onValueChanged.AddListener(ToggleListener);
        }

        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].onValueChanged.AddListener(SliderListener);
        }

    }

    // Update is called once per frame
    void Update()
    {
        ButtonSound.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
        DropdownSound.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
        ToggleSound.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
        SliderSound.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
        //OptionsMenuMusic.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
    }

    private void ButtonListener()
    {
        ButtonSound.start();
    }

    private void DropdownListener(int index)
    {
        DropdownSound.start();
    }


    private void ToggleListener(bool index)
    {
        ToggleSound.start();
    }


    private void SliderListener(float index)
    {
        SliderSound.start();
    }

}
