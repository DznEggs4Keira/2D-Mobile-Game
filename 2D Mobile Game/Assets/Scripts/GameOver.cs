using UnityEngine;

public class GameOver : MonoBehaviour
{
    readonly HighScore currScore;

    public void ReturnToMainButton()
    {
        //reset the score counter
        currScore.ResetCurrScore();
    }

    public void GameOverCalled()
    {
        gameObject.SetActive(true);
    }
}
