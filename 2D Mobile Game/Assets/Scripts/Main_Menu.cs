using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Game is paused on Begin
        Time.timeScale = 0;
    }

    public void PlayButton()
    {
        //Game Begin
        Time.timeScale = 1;

        gameObject.SetActive(false);
    }

    public void OptionsButton()
    {
        //done in unity
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
