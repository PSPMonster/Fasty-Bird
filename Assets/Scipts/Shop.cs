using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text bronzeCurrency, silverCurrency, goldCurrency;

    private void Awake()
    {
        if(!PlayerPrefs.HasKey("bronzeCurrency"))
        {
            PlayerPrefs.SetInt("bronzeCurrency", 0);
            bronzeCurrency.text = '$' + PlayerPrefs.GetInt("bronzeCurrency").ToString("");
        }
        if(!PlayerPrefs.HasKey("silverCurrency"))
        {
            PlayerPrefs.SetInt("silverCurrency", 0);
            silverCurrency.text = '$' + PlayerPrefs.GetInt("silverCurrency").ToString("");
        }
        if (!PlayerPrefs.HasKey("goldCurrency"))
        {
            PlayerPrefs.SetInt("goldCurrency", 0);
            goldCurrency.text = '$' + PlayerPrefs.GetInt("goldCurrency").ToString("");
        }
        
        else 
        {
            Load();
        }
    }

    public void Load()
    {
        bronzeCurrency.text = '$' + PlayerPrefs.GetInt("bronzeCurrency").ToString("");
        silverCurrency.text = '$' + PlayerPrefs.GetInt("silverCurrency").ToString("");
        goldCurrency.text = '$' + PlayerPrefs.GetInt("goldCurrency").ToString("");
    }
}
