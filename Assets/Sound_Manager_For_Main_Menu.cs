using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;


public class Sound_Manager_For_Main_Menu : MonoBehaviour//,ISelectHandler,IPointerEnterHandler
{

    FMOD.Studio.EventInstance ButtonSound, MainMenuMusic, OptionsMenuMusic, GameObjectSelected;
    public Button[] buttons;
    public Button start, Options,Back;
    private GameObject temp;

    public GameObject a;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ButtonSound = RuntimeManager.CreateInstance("event:/Master/SFX/ButtonPressed");
        RuntimeManager.AttachInstanceToGameObject(ButtonSound, a.transform, false);

        MainMenuMusic = RuntimeManager.CreateInstance("event:/Master/Music/MainMenuMusic");
        RuntimeManager.AttachInstanceToGameObject(MainMenuMusic, a.transform, false);

        OptionsMenuMusic = RuntimeManager.CreateInstance("event:/Master/Music/OptionsPauseMenuMusic");
        RuntimeManager.AttachInstanceToGameObject(OptionsMenuMusic, a.transform, false);

        GameObjectSelected = RuntimeManager.CreateInstance("event:/Master/SFX/GameObjectSelected");
        RuntimeManager.AttachInstanceToGameObject(GameObjectSelected, a.transform, false);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(ButtonListener);
        }
        start.onClick.AddListener(ButtonListenerForStart);
        Options.onClick.AddListener(ButtonListenerForOptions);
        Back.onClick.AddListener(ButtonListenerForBack);
        start.onClick.AddListener(ButtonListenerForStart);
        Options.onClick.AddListener(ButtonListenerForOptions);
        Back.onClick.AddListener(ButtonListenerForBack);
        MainMenuMusic.start();
        temp = null;

    }

    // Update is called once per frame
    void Update()
    {
        ButtonSound.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
        MainMenuMusic.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
        OptionsMenuMusic.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
        /*
        if (Selection.activeGameObject != null && Selection.activeGameObject != temp)
        {
            GameObjectSelected.start();
            temp = Selection.activeGameObject;
        }
        */
        if (EventSystem.current.currentSelectedGameObject == null && temp != EventSystem.current.currentSelectedGameObject)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
            {
                GameObjectSelected.start();
            }
                temp = EventSystem.current.currentSelectedGameObject;
        }
            

    }
    private void ButtonListenerForStart()
    {
        ButtonSound.start();
        MainMenuMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

    }

    private void ButtonListenerForOptions()
    {
        ButtonSound.start();
        MainMenuMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        OptionsMenuMusic.start();
    }
    
    private void ButtonListenerForBack()
    {
        ButtonSound.start();
        OptionsMenuMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        MainMenuMusic.start();
    }
    public void ONSELECTButton(BaseEventData a)
    {
        GameObjectSelected.start();
    }
/*
    public void OnSelect(BaseEventData a)
    {
        GameObjectSelected.start();
    }

        public void OnPointerEnter(PointerEventData  a)
    {
        GameObjectSelected.start();
    }
    */

    private void ButtonListener()
    {
        //RuntimeManager.PlayOneShot(ButtonSound, a.transform.position);
        //ButtonSound.start();
        //Channel channel;
        //corSystem.setOutput()
        ButtonSound.start();
        //RuntimeManager.PlayOneShot(SoundEvent, a.transform.position);
        //ButtonSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        //corSystem.playSound(sound, channelgroup, false, out channel);

    }
}
