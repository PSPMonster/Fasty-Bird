using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    public GameObject score;
    public GameObject gameOverCanvas;
    public GameObject getReadyImage;
    public GameObject pauseButton;
    public static Vector2 bottomLeft;

    public Text panelScore;

    //game states
    public static bool gameOver;
    public static bool waitingToPlay;
    public static bool gamePaused;

    int drawScore;
    public static int gameScore;
    private string shareMessage;

    private void Awake()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject transition = GameObject.Find("birdTransition");
        Object.Destroy(transition, 1.6f);
        gameOver = false;
        waitingToPlay = false;
        gamePaused = false;
    }
    public void GameOver()
    {
        gameOver = true;
        gameScore = score.GetComponent<Score>().GetScore();
        score.SetActive(false);
        Invoke("ActiveGameOverCanvas", 0.5f);
        pauseButton.SetActive(false);
    }

    void ActiveGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
    }

    public void OnOkButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.audiomanager.Play("select");
    }

    public void MenuBtnPressed()
    {
        AudioManager.audiomanager.Play("select");
        fader.gameObject.SetActive(true);

        LeanTween.scale (fader, Vector3.zero, 0f);
        LeanTween.scale (fader, new Vector3 (1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete (() => {
            SceneManager.LoadScene("Menu");
        });
    }

    public void ShareButtonPressed()
    {
        print(gameScore.ToString());
        if (gameScore > 1) {
            print("Sheeesh! I bet you ain't faster than me cuz I just scored " + gameScore.ToString() + " points in FastyBird!!!");
            shareMessage = "Sheeesh! I bet you ain't faster than me cuz I just scored " + gameScore.ToString() + " points in FastyBird!!!";
        }
        else if (gameScore == 0) {
            print("Sheeesh! I bet you are faster than me cuz I just scored " + gameScore.ToString() + " point in FastyBird xDD");
            shareMessage = "Sheeesh! I bet you are faster than me cuz I just scored " + gameScore.ToString() + " point in FastyBird xDD";
        }

        StartCoroutine(TakeScreenshotAndShare());
    }

    public void GameStarted()
    {
        waitingToPlay = true;
        score.SetActive(true);
        getReadyImage.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void DrawScore()
    {
        if (drawScore <= gameScore)
        {
            panelScore.text = drawScore.ToString();
            drawScore++;
            Invoke("DrawScore", 0.05f);
        }
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject("Fasty Bird").SetText(shareMessage).Share();
    }
}
