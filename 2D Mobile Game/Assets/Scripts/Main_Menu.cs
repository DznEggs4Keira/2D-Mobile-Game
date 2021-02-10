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

    public void PauseButton()
    {
        //Game is paused
        Time.timeScale = 0;

        //maybe do something else here
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
