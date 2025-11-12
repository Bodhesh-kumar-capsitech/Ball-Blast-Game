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
    private string level = "HighScore_Default";
    private StartGame game;
   
    private void Awake()
    {
        instance = this;
        game = StartGame.instance;

        if (game.isBeginner == true)
        {
            level = "HighScore_Beginner";
        }

        if (game.isIntermediate == true)
        {
            level = "HighScore_Intermediate";
        }

        highScore = PlayerPrefs.GetInt(level, 0);
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        currentScore += points;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(level, highScore);
            PlayerPrefs.Save();
        }
        UpdateScoreText();

    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreText();
    }

    //Update score in the UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;

        if (yourscoreText != null)
            yourscoreText.text = "Your Score : " + currentScore;

        highscoreText.text = "High Score : " + highScore;

    }

    //Reset the highscore
    public void ResetHighScore()
    {
        PlayerPrefs.SetInt(level, 0);
        highScore = 0;
        UpdateScoreText();
    }

}
