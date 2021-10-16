using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{   
    AudioSource audioSource;
    private bool isMuted = false;
    public Button musicButton;
    public Sprite mutedSpr, unmutedSpr;


    void Awake()
    {
        audioSource = GameObject.Find("Background Music").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("isMuted"))
        {
            PlayerPrefs.SetInt("isMuted", 0);
            Load();
        }

        else
        {
            Load();
        }

        UpdateButtonIcon();
        AudioListener.pause = isMuted;
    }

    public void OnMusicPress()
    {
        if (isMuted == false)
        {
            isMuted = true;
            audioSource.Stop();

            AudioListener.pause = isMuted;
        }

        else
        {
            isMuted = false;
            audioSource.Play();

            AudioListener.pause = isMuted;
        }

        Save();
        UpdateButtonIcon();
    }

    public void OnSfxPress()
    {
        
    }

    private void UpdateButtonIcon()
    {
        if (isMuted == false)
        {
            musicButton.image.sprite = unmutedSpr;
        }

        else
        {
            musicButton.image.sprite = mutedSpr;
        }
    }

    private void Load()
    {
        isMuted = PlayerPrefs.GetInt("isMuted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
    }
}
