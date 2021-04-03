using UnityEngine;
using UnityEngine.Audio;
using RDG;

public class Options_Menu : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    bool vibrateOn;

    float volume = 0f;

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
            //Handheld.Vibrate();
            Custom_Vibration.Vibrate(50, 100, true);
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
            audioMixer.SetFloat("volume", volume);
        }
        else
        {
            audioMixer.SetFloat("volume", -80);
        }
        
    }

    public void VolumeSlider(float deltaVolume)
    {
        volume = deltaVolume;
        audioMixer.SetFloat("volume", volume);
    }
}
