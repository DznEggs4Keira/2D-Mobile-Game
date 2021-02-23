using UnityEngine;
using UnityEngine.Audio;

public class Options_Menu : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    bool vibrateOn;

    private void Awake()
    {
        //by default, they should be on
        vibrateOn = true;
        audioMixer.SetFloat("volume", 0);
    }

    public void Vibrate()
    {
        //if value true. do something with it.
        if (vibrateOn)
            Handheld.Vibrate();
    }

    //save a bool based on whether vibrate is on or off
    public void VibrateToggleButton(bool value)
    {
        vibrateOn = value;
    }

    public void VolumeToggleButton(bool value)
    {
        if(value)
        {
            audioMixer.SetFloat("volume", 0);
        }
        else
        {
            audioMixer.SetFloat("volume", -80);
        }
        
    }
}
