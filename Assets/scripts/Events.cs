
using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void  Replaygame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
