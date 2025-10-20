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

    public Text scoretext;
    void Start()
    {
        gameover = false;
        Time.timeScale = 1;
        isGameStarted = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameover)
        {
            Time.timeScale = 0;
            gameOverpanel.SetActive(true);
        }

        scoretext.text = "Coins:" + score;

        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            isGameStarted = true;
            Destroy(starting);
        }

    }
}
