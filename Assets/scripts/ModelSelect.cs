using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelect : MonoBehaviour
{
    public int currentIndex = 0;
    public GameObject[] realmodels;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("SelectedModel", 0);
        foreach (GameObject model in realmodels)
        {
            model.SetActive(false);
        }
        realmodels[currentIndex].SetActive(true);
    }
}
