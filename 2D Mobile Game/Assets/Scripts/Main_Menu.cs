using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    public Game_Manager gm;

    // Start is called before the first frame update
    void Start()
    {
        //Game is paused on Begin
        Time.timeScale = 0;
    }

    public void PlayButton()
    {
        //Game Begin
        gm.StartGame();

        gameObject.SetActive(false);
    }

    public void PauseButton()
    {
        gm.Paused();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
