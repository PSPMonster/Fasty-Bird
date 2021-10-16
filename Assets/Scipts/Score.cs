using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    int highScore;
    public static int playerScore;

    Text scoreText;
    public Text panelScore;
    public Text panelHighScore;
    public GameObject NewImage;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        playerScore = 0;
        scoreText = GetComponent<Text>();
        panelScore.text = playerScore.ToString();
        scoreText.text = playerScore.ToString();

        int buildIndex = currentScene.buildIndex;

        // Check the scene name as a conditional.
        switch (buildIndex)
        {
            case 2:
                highScore = PlayerPrefs.GetInt("highScoreFastyBird");
                panelHighScore.text = highScore.ToString();
                break;
            case 1:
                highScore = PlayerPrefs.GetInt("highScoreFlappyBird");
                panelHighScore.text = highScore.ToString();
                break;
        }
    }

    public void Scored()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();
        // panelScore.text = playerScore.ToString();


        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("FastyBird"))
        {
            if (playerScore > highScore)
            {
                highScore = playerScore;
                panelHighScore.text = highScore.ToString();
                PlayerPrefs.SetInt("highScoreFastyBird", highScore);
                NewImage.SetActive(true);
            }
        }
        else if (playerScore > highScore)
        {
            highScore = playerScore;
            panelHighScore.text = highScore.ToString();
            PlayerPrefs.SetInt("highScoreFlappyBird", highScore);
            NewImage.SetActive(true);
        }
    }
    public int GetScore()
    {
        return playerScore;
    }
}
