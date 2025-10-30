using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    public static bool gameover;
    public GameObject gameOverpanel;
    public GameObject starting;
    public static bool isGameStarted;
    public static int score;
    public static int currentScore = 0;
    private int totalCoins;
    private int highScore;
    public Text highscoreText_GO;
    public Text totalCoinsText_GO;
    public Text scoretext;
    public Text scoreReal;

    public Text levelText;

    // LevelProgression-с хурд удирдана, энд устгах
    // public float baseSpeed = 10f;
    // public float maxSpeed = 30f;
    // public float speedIncreaseRate = 0.01f;
    // public int level = 1;

    void Start()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        gameover = false;
        Time.timeScale = 1;
        isGameStarted = false;
        score = 0;
        currentScore = 0;

        // Level текстийг LevelProgression-с авна
        if (levelText != null)
        {
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
            levelText.text = "Level: " + currentLevel;
        }
    }

    void Update()
    {
        if (gameover)
        {
            GameOverLogic();
        }

        scoreReal.text = "Score:" + currentScore;
        scoretext.text = "Coins:" + score;

        if (!isGameStarted && Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            Destroy(starting);
        }

        // UpdateSpeedAndLevel функц устгах - LevelProgression хариуцна
    }

    private void GameOverLogic()
    {
        gameover = false;
        Time.timeScale = 0;
        gameOverpanel.SetActive(true);

        totalCoins += score;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        PlayerPrefs.Save();

        if (highscoreText_GO != null)
        {
            highscoreText_GO.text = "High Score: " + highScore.ToString();
        }
        if (totalCoinsText_GO != null)
        {
            totalCoinsText_GO.text = "Total Coins: " + totalCoins.ToString();
        }
    }
}