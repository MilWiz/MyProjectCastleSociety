using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    FMOD.Studio.EventInstance ButtonSound, GameMusic, OptionsMenuMusic;
    public GameObject a;
    public Button[] buttons;
    public Button yes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ButtonSound = RuntimeManager.CreateInstance("event:/Master/SFX/ButtonPressed");
        RuntimeManager.AttachInstanceToGameObject(ButtonSound, a.transform, false);

        OptionsMenuMusic = RuntimeManager.CreateInstance("event:/Master/Music/MainMenuMusic");
        RuntimeManager.AttachInstanceToGameObject(OptionsMenuMusic, a.transform, false);

        GameMusic = RuntimeManager.CreateInstance("event:/Master/Music/OptionsPauseMenuMusic");
        RuntimeManager.AttachInstanceToGameObject(GameMusic, a.transform, false);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(ButtonListener);
        }
        yes.onClick.AddListener(yesButtonListener);

        GameMusic.start();

    }

    private void yesButtonListener()
    {
        ButtonSound.start();
        GameMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
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

    // Update is called once per frame
    void Update()
    {
        ButtonSound.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
        GameMusic.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
        OptionsMenuMusic.set3DAttributes(RuntimeUtils.To3DAttributes(a.transform));
    }
}
