using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI hiScore;

    public float pointsPerSecond = 10.5f;

    private float currentScore = 0f;

    // Start is called before the first frame update
    void Start()
    {
        hiScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentScore += pointsPerSecond * Time.deltaTime;

        score.text = Mathf.Round(currentScore).ToString();

        //HighScore
        if(currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (int)currentScore);

            hiScore.text = score.text;
        }
    }

    public void ResetCurrScore()
    {
        //set teh current score back to zero to reset it
        currentScore = 0f;
    }
}
