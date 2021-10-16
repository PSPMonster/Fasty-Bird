using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    Image img;
    public Sprite playSprite;
    public Sprite pauseSprite;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    public void OnPauseGame()
    {
        if (GameManager.gamePaused == false)
        {
            Time.timeScale = 0;
            img.sprite = playSprite;
            GameManager.gamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            img.sprite = pauseSprite;
            GameManager.gamePaused = false;
        }
    }
}
