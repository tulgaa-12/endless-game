using UnityEngine;
using TMPro; 

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI totalCoinsText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
    
        UpdateMenuUI();
    }

    public void UpdateMenuUI()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoinsText.text = "TotalCoins" + totalCoins.ToString();

        highScoreText.text = "High Score " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}

