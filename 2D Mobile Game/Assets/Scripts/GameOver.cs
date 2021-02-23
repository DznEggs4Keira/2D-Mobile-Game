using UnityEngine;

public class GameOver : MonoBehaviour
{
    public HighScore currScore;
    public GameObject gameOverScreen;

    public void ReturnToMainButton()
    {
        //reset the score counter
        currScore.ResetCurrScore();
    }

    public void GameOverCalled()
    {
        gameOverScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
