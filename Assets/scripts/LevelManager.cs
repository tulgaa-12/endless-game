
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using System.Collections;

//public class LevelProgression : MonoBehaviour
//{
//    public int totalLevels = 5;
//    private int currentLevel = 1;
//    public float levelDistance = 300f;
//    private float nextLevelThreshold;

//    public float baseSpeed = 20f;
//    public float speedIncreasePerLevel = 2.5f;

//    public NewBehaviourScript player;

//    public GameObject completedUI;
//    public Text completedText;

//    public GameObject levelStartUI;
//    public Text levelStartText;

//    private bool isTransitioning = false;

//    void Start()
//    {

//        if (PlayerPrefs.HasKey("CurrentLevel"))
//        {
//            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
//        }
//        else
//        {
//            currentLevel = 1;
//            PlayerPrefs.SetInt("CurrentLevel", 1);
//        }

//        nextLevelThreshold = levelDistance;
//        completedUI.SetActive(false);

//        // ЧУХАЛ: Хурдыг тохируулах
//        SetSpeedForCurrentLevel();

//        // Level эхлэх харуулах
//        StartCoroutine(ShowLevelStart());
//    }

//    void SetSpeedForCurrentLevel()
//    {
//        float newSpeed = baseSpeed + (speedIncreasePerLevel * (currentLevel - 1));
//        player.forwardSpeed = newSpeed;

//        Debug.Log("=== LEVEL " + currentLevel + " ===");
//        Debug.Log("Хурд: " + newSpeed);
//        Debug.Log("Зай: " + levelDistance + "м");
//    }

//    IEnumerator ShowLevelStart()
//    {
//        if (levelStartUI != null && levelStartText != null)
//        {
//            playerManager.isGameStarted = false;

//            levelStartUI.SetActive(true);
//            levelStartText.text = "LEVEL " + currentLevel;

//            yield return new WaitForSeconds(2f);

//            levelStartUI.SetActive(false);
//        }
//    }

//    void Update()
//    {
//        if (!playerManager.isGameStarted || playerManager.gameover)
//            return;

//        float distance = player.transform.position.z;

//        if (distance >= nextLevelThreshold && !isTransitioning)
//        {
//            StartCoroutine(LevelCompleted());
//        }
//    }

//    IEnumerator LevelCompleted()
//    {
//        isTransitioning = true;
//        playerManager.isGameStarted = false;

//        completedUI.SetActive(true);
//        completedText.text = "LEVEL " + currentLevel + " COMPLETED!";

//        yield return new WaitForSeconds(2f);

//        completedUI.SetActive(false);

//        if (currentLevel < totalLevels)
//        {
//            currentLevel++;

//            // Дараагийн level дугаарыг хадгалах
//            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
//            PlayerPrefs.Save();

//            Debug.Log("Scene дахин ачаалж байна... Next Level: " + currentLevel);

//            // Scene дахин ачаалах
//            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//        }
//        else
//        {
//            completedUI.SetActive(true);
//            completedText.text = "GAME COMPLETED!";
//            playerManager.gameover = true;

//            PlayerPrefs.SetInt("CurrentLevel", 1);
//            PlayerPrefs.Save();
//        }

//        isTransitioning = false;
//    }

//    public void ResetGame()
//    {
//        playerManager.gameover = false;
//        playerManager.score = 0;
//        playerManager.currentScore = 0;
//        PlayerPrefs.SetInt("CurrentLevel", 1);
//        PlayerPrefs.Save();
//        Time.timeScale = 1;
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }
//}


using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UIElements.VisualElement;

public class LevelProgression : MonoBehaviour
{
    public int totalLevels = 5;
    private int currentLevel = 1;

    // Level бүрийн зай
    public float baseLevelDistance = 300f; // Level 1: 300м
    public float distanceIncreasePerLevel = 500f; // Level бүрт +500м
    private float currentLevelDistance;
    private float nextLevelThreshold;

    public float baseSpeed = 20f;
    public float speedIncreasePerLevel = 2.5f;

    public NewBehaviourScript player;

    public GameObject completedUI;
    public Text completedText;

    public GameObject levelStartUI;
    public Text levelStartText;

    // Туулах зай харуулах UI
    public Text distanceText;

    // Bonus Coins
    public int bonusCoinsPerLevel = 100;
    public int finalBonusCoins = 500;

    private bool isTransitioning = false;

    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            currentLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }

        // Одоогийн level-ийн зай тооцоолох
        CalculateLevelDistance();

        nextLevelThreshold = currentLevelDistance;
        completedUI.SetActive(false);

        SetSpeedForCurrentLevel();
        StartCoroutine(ShowLevelStart());
    }

    void CalculateLevelDistance()
    {
        // Level 1: 300м
        // Level 2: 800м (300 + 500)
        // Level 3: 1300м (800 + 500)
        // Level 4: 1800м (1300 + 500)
        // Level 5: 2300м (1800 + 500)
        currentLevelDistance = baseLevelDistance + (distanceIncreasePerLevel * (currentLevel - 1));

        Debug.Log("Level " + currentLevel + " зай: " + currentLevelDistance + "м");
    }

    void SetSpeedForCurrentLevel()
    {
        float newSpeed = baseSpeed + (speedIncreasePerLevel * (currentLevel - 1));
        player.forwardSpeed = newSpeed;

        Debug.Log("=== LEVEL " + currentLevel + " ===");
        Debug.Log("Хурд: " + newSpeed);
        Debug.Log("Зай: " + currentLevelDistance + "м");
    }

    IEnumerator ShowLevelStart()
    {
        if (levelStartUI != null && levelStartText != null)
        {
            playerManager.isGameStarted = false;

            levelStartUI.SetActive(true);
            levelStartText.text = "LEVEL " + currentLevel + "\n\nDistance: " + currentLevelDistance + "m";

            yield return new WaitForSeconds(2f);

            levelStartUI.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerManager.isGameStarted || playerManager.gameover)
            return;

        float distance = player.transform.position.z;

        // Туулах зай харуулах (үлдсэн зай)
        if (distanceText != null)
        {
            float remainingDistance = nextLevelThreshold - distance;
            if (remainingDistance < 0) remainingDistance = 0;
            distanceText.text = "Distance: " + Mathf.RoundToInt(remainingDistance) + "m / " + currentLevelDistance + "m";
        }

        if (distance >= nextLevelThreshold && !isTransitioning)
        {
            StartCoroutine(LevelCompleted());
        }
    }

    IEnumerator LevelCompleted()
    {
        isTransitioning = true;
        playerManager.isGameStarted = false;

        // Level Up дуу
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlaySound("LevelUp");
        }

        completedUI.SetActive(true);
        completedText.text = "LEVEL " + currentLevel + " COMPLETED!\n\n" +
                           "Distance: " + currentLevelDistance + "m";

        yield return new WaitForSeconds(2f);

        completedUI.SetActive(false);

        if (currentLevel < totalLevels)
        {
            // Level дууссаны Bonus Coins
            playerManager.score += bonusCoinsPerLevel;

            currentLevel++;

            // Дараагийн level дугаарыг хадгалах
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            PlayerPrefs.Save();

            

            // Scene дахин ачаалах
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // БҮХ LEVEL ДУУССАН - Шагнал өгөх
            int totalBonusCoins = finalBonusCoins;
            playerManager.score += totalBonusCoins;

            // Total Coins-д нэмэх
            int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
            totalCoins += playerManager.score;
            PlayerPrefs.SetInt("TotalCoins", totalCoins);

            // High Score шалгах
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (playerManager.currentScore > highScore)
            {
                highScore = playerManager.currentScore;
                PlayerPrefs.SetInt("HighScore", highScore);
            }

            PlayerPrefs.Save();

            // Victory дуу
            if (audioManager != null)
            {
                audioManager.PlaySound("Victory");
            }

            // Game Completed мэдээлэл харуулах
            completedUI.SetActive(true);
            completedText.text = "🎉 GAME COMPLETED! \n\n" +
                               "Total Score: " + playerManager.currentScore + "\n" +
                               "Coins Collected: " + (playerManager.score - totalBonusCoins) + "\n" +
                               "Bonus Coins: +" + totalBonusCoins + "\n" +
                               "━━━━━━━━━━━━━━━━\n" +
                               "Total Coins: " + totalCoins + "\n\n" +
                               "Restarting...";

            // Level-ийг reset хийх
            PlayerPrefs.SetInt("CurrentLevel", 1);
            PlayerPrefs.Save();

            // 5 секунд хүлээх
            yield return new WaitForSeconds(5f);

            // Тоглоомыг дахин эхлүүлэх
            playerManager.gameover = false;
            playerManager.score = 0;
            playerManager.currentScore = 0;
            Time.timeScale = 1;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        isTransitioning = false;
    }

    public void ResetGame()
    {
        playerManager.gameover = false;
        playerManager.score = 0;
        playerManager.currentScore = 0;
        PlayerPrefs.SetInt("CurrentLevel", 1);
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

