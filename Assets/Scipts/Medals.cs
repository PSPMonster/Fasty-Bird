using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medals : MonoBehaviour
{
    public Sprite normalMedal, bronzeMedal, silverMedal, goldMedal;
    public Image sparkle1, sparkle2, sparkle3;

    public int currentBalance;
    public int currencyInRun = 0;

    Image img;

    // Start is called before the first frame update

    void Awake()
    {
        currencyInRun = 0;
    }

    void Start()
    {
        img = GetComponent<Image>();
        int gameScore = GameManager.gameScore;

        if (gameScore == 0)
        {
            HideSparkles();
            img.sprite = normalMedal;
        }

        else
        {
            if (gameScore > 0 && gameScore <= 2)
            {
                HideSparkles();
                img.sprite = normalMedal;
            }
            else if (gameScore > 2 && gameScore <= 4)
            {
                HideSparkles();
                img.sprite = bronzeMedal;
                currencyInRun += 1;
                PlayerPrefs.SetInt("bronzeCurrency", PlayerPrefs.GetInt("bronzeCurrency") + currencyInRun);
            }
            else if (gameScore > 4 && gameScore <= 6)
            {
                HideSparkles();
                img.sprite = silverMedal;
                currencyInRun += 1;
                PlayerPrefs.SetInt("silverCurrency", PlayerPrefs.GetInt("silverCurrency") + currencyInRun);
            }
            else
            {
                img.sprite = goldMedal;
                currencyInRun += 1;
                PlayerPrefs.SetInt("goldCurrency", PlayerPrefs.GetInt("goldCurrency") + currencyInRun);
            }
        }
    }

    private void HideSparkles()
    {
        sparkle1.enabled = false;
        sparkle2.enabled = false;
        sparkle3.enabled = false;
    }
}
