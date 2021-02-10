using UnityEngine;
using UnityEngine.Audio;

public class Options_Menu : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;

    //returns a bool of whether vibration is enabled or not
    public void VibrateToggleButton(bool value)
    {
        //if value true. do something with it.
        if (value)
            Handheld.Vibrate();
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
