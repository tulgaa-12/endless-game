
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class shopManager : MonoBehaviour


{

    public MenuManager menuManager;
    public modelsprint[] modelList;
    public int currentIndex = 0;
    public GameObject[] models;
    public TextMeshProUGUI coinText;
    public Button Buybutton;
    // Start is called before the first frame update
    void Start()
    {
       
        foreach (modelsprint model in modelList)
        {
            if (model.price == 0)
                model.isPurchased = true;
            else
                model.isPurchased = PlayerPrefs.GetInt(model.modelName, 0) == 0 ? false : true;
        }
        currentIndex = PlayerPrefs.GetInt("SelectedModel", 0);
        foreach (GameObject model in models)
        {
            model.SetActive(false);
        }
        models[currentIndex].SetActive(true);
        UpdateUI();
        UpdateCoinDisplay();
    }

    //Update is called once per frame
    void Update()
    {
        //UpdateUI();
        //UpdateCoinDisplay();
    }

    public void NextModel()
    {
        models[currentIndex].SetActive(false);

        currentIndex++;
        if (currentIndex == models.Length)
        {
            currentIndex = 0;
        }
        models[currentIndex].SetActive(true);
        modelsprint c = modelList[currentIndex];
        if (c.isPurchased)
        {
            PlayerPrefs.SetInt("SelectedModel", currentIndex);
        }

        //PlayerPrefs.SetInt("SelectedModel", currentIndex);
        UpdateCoinDisplay();
        UpdateUI();
    }




    public void PreviousModel()
    {
        models[currentIndex].SetActive(false);
        currentIndex--;
        if (currentIndex == -1)
        {
            currentIndex = models.Length - 1;
        }
        models[currentIndex].SetActive(true);
        modelsprint c = modelList[currentIndex];
        if (c.isPurchased)
        {
            PlayerPrefs.SetInt("SelectedModel", currentIndex);
        }
        UpdateCoinDisplay();
        UpdateUI();
    }



    public void unlock()
    {
        modelsprint c = modelList[currentIndex];
        PlayerPrefs.SetInt(c.modelName, 1);
        PlayerPrefs.SetInt("SelectedModel", currentIndex);
        c.isPurchased = true;
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) - c.price);

        UpdateUI();
        UpdateCoinDisplay();

        if (menuManager != null)
        {
            menuManager.UpdateMenuUI();
        }
    }


    void UpdateCoinDisplay()
    {
        if (coinText != null)
        {
            int coins = PlayerPrefs.GetInt("TotalCoins", 0);
            coinText.text = "TotalCoins"  + coins.ToString();
        }
    }

    public void UpdateUI()
    {
        modelsprint c = modelList[currentIndex];
        if (c.isPurchased)
        {
            Buybutton.gameObject.SetActive(false);
        }
        else
        {
            Buybutton.gameObject.SetActive(true);
            Buybutton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy " + c.price;
            if (c.price <= PlayerPrefs.GetInt("TotalCoins", 0))
            {
                Buybutton.interactable = true;
            }
            else
            {
                Buybutton.interactable = false;
            }
        }
        UpdateCoinDisplay();
    }


}

