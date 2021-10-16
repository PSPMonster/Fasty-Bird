using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject birdTransition;

    [SerializeField] RectTransform fader;

    // Start is called before the first frame update
    void Start()
    {
        birdTransition.SetActive(false);
        fader.gameObject.SetActive(true);

        LeanTween.scale (fader, new Vector3 (1, 1, 1), 0);
        LeanTween.scale (fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete (() => {
            fader.gameObject.SetActive(false);
        });
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("FlappyBird");
    }

    public void CallLoadNextScene()
    {
        birdTransition.SetActive(true);
        playSelect();
        Invoke("PlayButtonPressed", 0.8f);
    }

    public void SettingsPressed()
    {
        playSelect();
    }

    public void ShopPressed()
    {
        playSelect();
    }

    public void RatePressed()
    {
        playSelect();
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.WiktorDev.FastyBird");
#endif
    }

    public void FastyBirdPressed()
    {

    }

    public void ResetHighScores()
    {
        PlayerPrefs.DeleteAll();
    }

    public void playSelect()
    {
        AudioManager.audiomanager.Play("select");
    }
}
