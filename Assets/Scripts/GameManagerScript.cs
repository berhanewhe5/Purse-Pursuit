using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject losePanel;
    public GameObject gameUIPanel;
    public GameObject mainMenuPanel;
    public GameObject pausedPanel;
    public GameObject settingsPanel;
    public GameObject storePanel;
    public StealScript stealScript;
    public bool gamePaused;
    public GameObject police;
    public GameObject player;
    public bool gameOver;
    public TMP_Text menuMoneyText;

    static bool restarted;

    [SerializeField] float policeWaitTime;

    public Slider soundEffectsSlider;
    public Slider musicSlider;

    public InterstitialAd interstitialAd;

    public GameObject adPanel;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        losePanel.SetActive(false);
        gameUIPanel.SetActive(false);
        settingsPanel.SetActive(false);
        adPanel.SetActive(false);
        gamePaused = true;
        gameOver = false;
        menuMoneyText.text = "$"+PlayerPrefs.GetInt("Money").ToString();
        Time.timeScale = 1f;

        if (restarted)
        {
            Play();
        }
    }

    public void Play()
    {
        gameOver = false;

        gameUIPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        stealScript.gameActive = true;
        gamePaused = false;
        GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
        GetComponent<StealGoalScript>().GenerateGoal();
    }

    public void MainMenuButton() { 
        restarted = false;
        GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        
    }

    public void Lose()
    {
        stealScript.gameActive = false;


        if (PlayerPrefs.GetInt("AdNum") > 1)
        {
            PlayerPrefs.SetInt("AdNum", 0);
        }
        else
        {
            PlayerPrefs.SetInt("AdNum", PlayerPrefs.GetInt("AdNum") + 1);
        }

        if (PlayerPrefs.GetInt("AdNum") <= 1 && (stealScript.Money>300))
        {
            stealScript.adMoneyText.text = "$" + stealScript.Money.ToString();
            gamePaused = true;
            Time.timeScale = 0;
            adPanel.SetActive(true);
        }
        else {
            police.SetActive(true);
        }

        player.GetComponent<PlayerMovement>().canMove = false;
    }

    public void LoseScreen()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (PlayerPrefs.GetInt("AdNum") > 1)
            {
                interstitialAd.ShowAd();
            }
        }

        gameOver = true;
        GetComponent<SoundEffectsPlayer>().playArrestSFX();
        losePanel.SetActive(true);
        gameUIPanel.SetActive(false);
        StartCoroutine("HidePolice");
        //GetComponent<SoundEffectsPlayer>().sfxAudioSource.volume = 0;
    }

    
    public void BackAdButton()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        adPanel.SetActive(false);
        police.SetActive(true);
    }
    IEnumerator HidePolice() {
        yield return new WaitForSeconds(policeWaitTime);
        police.SetActive(false);
        Time.timeScale = 0f;

    }
    public void RestartButton() {
        GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        restarted = true;

    }

    public void PauseButton() {

        if (gamePaused)
        {
            gameUIPanel.SetActive(true);
            player.GetComponent<PlayerMovement>().StartCoroutine("SwipeTimeout");
            stealScript.gameActive = true;
            pausedPanel.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;
        }
        else {
            GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
            gameUIPanel.SetActive(false);
            stealScript.gameActive = false;
            pausedPanel.SetActive(true);
            Time.timeScale = 0f;
            gamePaused = true;
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Guy" + "Purchased", 1);
    }

    public void SettingsButton()
    {
        GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
        settingsPanel.SetActive(true);
    }

    public void ExitSettingsButton()
    {
        GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
        settingsPanel.SetActive(false);
    }

    public void StoreButton()
    {
        storePanel.SetActive(true);
        GetComponent<StoreScript>().CostumeButton();
    }

    public void ExitStoreButton()
    {
        GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
        storePanel.SetActive(false);
    }
}
