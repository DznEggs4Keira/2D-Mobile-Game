using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public HighScore currScore;
    public GameObject player;
    public GameObject gameOverScreen;
    public GameObject pauseButton;

    //Game Start
    public void StartGame()
    {
        //play game
        Time.timeScale = 1;

        //player controller enabled
        player.GetComponent<Player_Controller>().enabled = true;

    }
    
    //on Death Reset Game
    public void ResetGame()
    {
        //play game
        Time.timeScale = 0;

        //reset the score counter
        currScore.ResetCurrScore();

        //reset deaths
        player.GetComponent<Health>().ResetPlayer();

        //player controller enabled
        player.GetComponent<Player_Controller>().enabled = false;
    }

    //Game Paused
    //Game Over
    public void Paused()
    {
        //pause game
        Time.timeScale = 0;

        //player controller enabled
        player.GetComponent<Player_Controller>().enabled = false;
    }

    public void GameOverCalled()
    {
        Time.timeScale = 0;

        gameOverScreen.gameObject.SetActive(true);

        pauseButton.GetComponent<Button>().enabled = false;
    }

    public void Credits_Graphics()
    {
        string url = "https://pixelfrog-assets.itch.io/pixel-adventure-1";

        Application.OpenURL(url);
    }

    public void Credits_Sounds()
    {
        string url = "https://freesound.org/";

        Application.OpenURL(url);
    }

    public void Credits_ME()
    {
        string url = "https://www.gameinventureden.com/";

        Application.OpenURL(url);
    }
}
