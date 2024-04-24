using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject losePanel;
    public GameObject gameUIPanel;
    public GameObject tutorialPanel;
    public GameObject mainMenuPanel;
    public GameObject pausedPanel;
    public GameObject settingsPanel;
    public GameObject storePanel;
    public GameObject infoPanel;
    public StealScript stealScript;
    public bool gamePaused;
    public GameObject police;
    public GameObject policeCar;
    public GameObject player;
    public bool gameOver;
    public TMP_Text menuMoneyText;
    public TMP_Text mostEarnedMoneyInfo;

    public GameObject newHighScoreTextInLosePanel;

    static bool restarted;

    [SerializeField] float policeWaitTime;

    public Slider soundEffectsSlider;
    public Slider musicSlider;


    public GameObject adPanel;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public AudioSource music;
    public AudioMixer audioMixer;


    [SerializeField] AudioClip PursePursuitSong1;
    [SerializeField] AudioClip PursePursuitSong2;
    public TMPro.TMP_Text songTitleText;
    public int currentSong;

    public Button previousSongButton;
    public Button nextSongButton;

    public GameObject moneyRewardText;
    public TMPro.TMP_Text loseScreenMoney;

    public GameObject replayTutorialPanel;

    public BannerAd bannerAd;
    public InterstialAd interstialAd;

    public bool moneyDoubled;

    void Start()
    {
        moneyDoubled = false;
        if (PlayerPrefs.GetInt("SongNumber") == 1)
        {
            currentSong = 1;
        }
        else if (PlayerPrefs.GetInt("SongNumber") == 2)
        {
            currentSong = 2;
        }

        UpdateSong();



        mainMenuPanel.SetActive(true);
        losePanel.SetActive(false);
        gameUIPanel.SetActive(false);
        settingsPanel.SetActive(false);
        adPanel.SetActive(false);
        infoPanel.SetActive(false);

        gamePaused = true;
        gameOver = false;
        menuMoneyText.text = "$"+PlayerPrefs.GetInt("Money").ToString();
        Time.timeScale = 1f;
        audioMixer.SetFloat("GeneralSoundEffectsVolume", 0f);
        if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
        {
            PlayerPrefs.SetFloat("SoundEffectsVolume", 0.5f);
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
        }
        GetComponent<VolumeSettings>().LoadMusicVolume();
        GetComponent<VolumeSettings>().LoadSoundEffectsVolume();

        if (restarted)
        {
            Play();
        }


        GetComponent<SoundEffectsPlayer>().ConstantPitchSFXAudioSource.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");
        GetComponent<SoundEffectsPlayer>().VariablePitchSFXAudioSource.volume = PlayerPrefs.GetFloat("SoundEffectsVolume");

    }

    public void Play()
    {
        if (PlayerPrefs.GetInt("NewPlayer") == 0)
        {
            if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
            {
                PlayerPrefs.SetInt("gamesPlayed", 1);
                GetComponent<Tutorial>().StartTutorial();
                GetComponent<Tutorial>().TutorialPart1();
                stealScript.gameActive = true;
                gameUIPanel.SetActive(true);

            }
            else
            {
                StartCoroutine("GenerateFirstGoal");
                stealScript.gameActive = true;
                gameUIPanel.SetActive(true);

            }
            PlayerPrefs.SetInt("NewPlayer", 1);
        }
        else {
            if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
            {
                tutorialPanel.SetActive(true);
                replayTutorialPanel.SetActive(true);
            }
            else
            {
                StartCoroutine("GenerateFirstGoal");
                stealScript.gameActive = true;
                gameUIPanel.SetActive(true);

            }
        }


        gameOver = false;

        mainMenuPanel.SetActive(false);

        gamePaused = false;
        GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
        
    }

    public void ReplayTutorial() {

        replayTutorialPanel.SetActive(false);
        PlayerPrefs.SetInt("gamesPlayed", 1);
        GetComponent<Tutorial>().StartTutorial();
        GetComponent<Tutorial>().TutorialPart1();
        gameUIPanel.SetActive(true);
        stealScript.gameActive = true;

    }

    public void DontReplayTutorial()
    {
        replayTutorialPanel.SetActive(false);
        StartCoroutine("GenerateFirstGoal");
        stealScript.gameActive = true;

        tutorialPanel.SetActive(false);
        PlayerPrefs.SetInt("GamePlayedBefore", 1);
        gameUIPanel.SetActive(true);
        player.GetComponent<StealScript>().canSteal = true;
    }
    IEnumerator GenerateFirstGoal()
    {
        if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
        {
            yield return new WaitForSeconds(3.5f);
        }
        else { 
            yield return new WaitForSeconds(5f);
        }
        GetComponent<StealGoalScript>().GenerateGoal();

        if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
        {
            GetComponent<Tutorial>().TutorialPart5();
        }
    }
    public void MainMenuButton() { 
        restarted = false;
        GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (PlayerPrefs.GetInt("gamesPlayed") % 3 == 0 && PlayerPrefs.GetInt("GamePlayedBefore") == 1)
            {
                Debug.Log("Show Ad");
                PlayerPrefs.SetInt("gamesPlayed", PlayerPrefs.GetInt("gamesPlayed") + 1);
                interstialAd.ShowInterstitialAd();
            }
        }
    }

    public void PlayTutorial()
    {
        PlayerPrefs.SetInt("GamePlayedBefore", 0);
        Play();
    }

    public void Lose()
    {
        GetComponent<SoundEffectsPlayer>().playMissedAStealSFX();
        stealScript.gameActive = false;
        tutorialPanel.SetActive(false);

        if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
        {
            GetComponent<Tutorial>().HideTutorialParts();
        }

        if (stealScript.Money > PlayerPrefs.GetInt("Highscore"))
        {
            newHighScoreTextInLosePanel.SetActive(true);
            PlayerPrefs.SetInt("Highscore", stealScript.Money);
        }


        if (stealScript.Money>0)
        {
            stealScript.adMoneyText.text = "$" + stealScript.Money.ToString();
            gamePaused = true;
            Time.timeScale = 0;
            adPanel.SetActive(true);
        }
        else {
            police.SetActive(true);
            policeCar.SetActive(true);
        }
        music.volume = 0f;
        player.GetComponent<PlayerMovement>().canMove = false;
    }

    public void LoseScreen()
    {
        /*
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            interstialAd.ShowInterstitialAd();
        }
        */
        if (PlayerPrefs.GetInt("GamePlayedBefore") == 1)
        {
            PlayerPrefs.SetInt("gamesPlayed", PlayerPrefs.GetInt("gamesPlayed") + 1);
        }


        

        gameOver = true;
        GetComponent<SoundEffectsPlayer>().playArrestSFX();
        losePanel.SetActive(true);
        gameUIPanel.SetActive(false);
        StartCoroutine("HidePolice");
        if (!moneyDoubled)
        {
            scoreText.text = "Score:\n$" + stealScript.Money.ToString();
            highscoreText.text = "HIghscore:\n$" + PlayerPrefs.GetInt("Highscore").ToString();
            loseScreenMoney.text = "$" + stealScript.Money.ToString();
        }
        else {
            scoreText.text = "Score:\n$" + (stealScript.Money/2).ToString();
            highscoreText.text = "HIghscore:\n$" + PlayerPrefs.GetInt("Highscore").ToString();
            loseScreenMoney.text = "$" + (stealScript.Money/2).ToString();
        }
        //GetComponent<SoundEffectsPlayer>().sfxAudioSource.volume = 0;
    }

    public void NextSong()
    {
        currentSong++;
        UpdateSong();
    }

    public void PreviousSong()
    {
        currentSong--;
        UpdateSong();
    }

    public void UpdateSong()
    {
        if (currentSong == 1)
        {
            music.clip = PursePursuitSong1;
            songTitleText.text = "Thief in the Shadows";
            PlayerPrefs.SetInt("SongNumber", 1);
            previousSongButton.interactable = false;
            nextSongButton.interactable = true;
        }
        else if (currentSong == 2)
        {
            music.clip = PursePursuitSong2;
            songTitleText.text = "Smooth Criminal";
            PlayerPrefs.SetInt("SongNumber", 2);
            nextSongButton.interactable = false;
            previousSongButton.interactable = true;
        }

        music.Play();
    }

    public void BackAdButton()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        adPanel.SetActive(false);
        police.SetActive(true);
        policeCar.SetActive(true);
    }
    IEnumerator HidePolice() {
        yield return new WaitForSeconds(policeWaitTime);
        police.SetActive(false);
        Time.timeScale = 0f;
        audioMixer.SetFloat("GeneralSoundEffectsVolume", -80f);


    }
    public void RestartButton() {
        GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        restarted = true;
    }

    public void PauseButton() {

        if (gamePaused)
        {
            if (PlayerPrefs.GetInt("RemoveAds") == 0)
            {
                bannerAd._bannerView.Destroy();
            }
            gameUIPanel.SetActive(true);
            player.GetComponent<PlayerMovement>().StartCoroutine("SwipeTimeout");
            stealScript.gameActive = true;
            pausedPanel.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;

            audioMixer.SetFloat("SoundEffectsVolume", PlayerPrefs.GetFloat("SoundEffectsVolume"));
        }
        else {
            if (PlayerPrefs.GetInt("RemoveAds") == 0)
            {
                bannerAd.CreateBannerView();
            }
            GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
            gameUIPanel.SetActive(false);
            stealScript.gameActive = false;
            pausedPanel.SetActive(true);

            Time.timeScale = 0f;
            gamePaused = true;

            audioMixer.SetFloat("SoundEffectsVolume", -80f);
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
        GetComponent<VolumeSettings>().LoadMusicVolume();
        GetComponent<VolumeSettings>().LoadSoundEffectsVolume();

        if (PlayerPrefs.GetInt("SongNumber") == 1)
        {
            songTitleText.text = "Thief in the Shadows";
            previousSongButton.interactable = false;
            nextSongButton.interactable = true;
        }
        else if (PlayerPrefs.GetInt("SongNumber") == 2)
        {
            songTitleText.text = "Smooth Criminal";
            nextSongButton.interactable = false;
            previousSongButton.interactable = true;
        }

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

    public void InfoButton()
    {
        infoPanel.SetActive(true);
        mostEarnedMoneyInfo.text = "Most Earned Money: $" + PlayerPrefs.GetInt("Highscore").ToString();
    }

    public void ExitInfoButton()
    {
        infoPanel.SetActive(false);
    }
    public void ExitStoreButton()
    {
        GetComponent<SoundEffectsPlayer>().playPressButtonSFX();
        storePanel.SetActive(false);
    }

    public void SubscribeToKonita()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCFyZBN6YExccgHAw3hSBfdg?sub_confirmation=1");
    }
}
