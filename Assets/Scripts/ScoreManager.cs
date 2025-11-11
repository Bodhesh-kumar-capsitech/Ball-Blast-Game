using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI yourscoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    private int currentScore = 0;
    private int highScore = 0;
    private StartGame level;
   
    private void Awake()
    {
        instance = this;
        level = StartGame.instance;
        highScore = PlayerPrefs.GetInt("HighScore : ", 0);
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        currentScore += points;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore : ", highScore);
            PlayerPrefs.Save();
        }
        UpdateScoreText();

    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;

        if (yourscoreText != null)
            yourscoreText.text = "Your Score : " + currentScore;

        //if (highscoreText != null && level.isBeginner == true)
        //{
        //    highscoreText.text = "High Score : " + highScore;
        //}


        //if (highscoreText != null && level.isIntermediate == true)
        //{
        //    highscoreText.text = "High Score : " + highScore;
        //}
        highscoreText.text = "High Score : " + highScore;

    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore : ", 0);
        highScore = 0;
        UpdateScoreText();
    }

}
