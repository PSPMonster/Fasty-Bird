using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnim : MonoBehaviour
{
    public GameObject medal;
    public GameManager gameManager;

    void OnGameOverAnimEnds()
    {
        medal.SetActive(true);
        gameManager.DrawScore();
    }
}
