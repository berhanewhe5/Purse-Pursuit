using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
    public Sprite InstantStealPowerUpImage;
    public Sprite InvisibleCloakPowerUpImage;
    public Sprite SpeedBoostPowerUpImage;

    public int powerUp;

    
    public float speedPowerUpTime;
    public float speedPowerUpMultiplier;

    public float instantStealPowerUpTime;
    public float invisibleCloakTime;

    public GameObject player;
    public PowerUpSpawner powerUpSpawner;

    public TMP_Text powerUpTimerText;
    public float powerUpTimer;
    public SoundEffectsPlayer soundEffectsPlayer;

    public float timeLeft;

    void Start()
    {
        powerUpTimerText.text = "";
        StartCoroutine("PowerUpButtonClicked");
    }

    public void ResetTimerText()
    {
        StopAllCoroutines();
        switch(powerUp)
        {
            case 0:
                timeLeft = instantStealPowerUpTime + (PlayerPrefs.GetInt("InstantStealTier") * 3);
                powerUpTimerText.text = timeLeft.ToString();
                break;
            case 1:
                timeLeft = invisibleCloakTime + (PlayerPrefs.GetInt("InvisibleCloakTier") * 3);
                powerUpTimerText.text = timeLeft.ToString();
                break;
            case 2:
                timeLeft = speedPowerUpTime + (PlayerPrefs.GetInt("SpeedBoostTier") * 3);
                powerUpTimerText.text = timeLeft.ToString();
                break;
        }
        StartCoroutine("PowerUpTimer");
    }
    public void SetPowerUpImage()
    {
        switch (powerUp)
        {
            case 0:
                GetComponent<Image>().sprite = InstantStealPowerUpImage;
                break;
            case 1:
                GetComponent<Image>().sprite = InvisibleCloakPowerUpImage;
                break;
            case 2:
                GetComponent<Image>().sprite = SpeedBoostPowerUpImage;
                break;
        }
    }

    public IEnumerator PowerUpButtonClicked()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        switch (powerUp)
        {
            case 0:
                timeLeft = instantStealPowerUpTime + (PlayerPrefs.GetInt("InstantStealTier") * 3);
                callTimer();
                break;
            case 1:
                timeLeft = invisibleCloakTime + (PlayerPrefs.GetInt("InvisibleCloakTier") * 3);
                callTimer();
                break;
            case 2:
                timeLeft = speedPowerUpTime + (PlayerPrefs.GetInt("SpeedBoostTier") * 3);
                callTimer();
                break;
        }

    }

    public void callTimer()
    {
        StartCoroutine("PowerUpTimer");
    }

    public IEnumerator PowerUpTimer()
    {
        switch (powerUp)
        {
            case 0:
                player.GetComponent<StealScript>().instantSteaalPowerUpActivated = true;
                powerUpSpawner.InstantStealActivated = true;
                break;
            case 1:
                player.GetComponent<StealScript>().invisibleCloakPowerUpActivated = true;
                powerUpSpawner.InvisibleCloakActivated = true;
                break;
            case 2:
                player.GetComponent<PlayerMovement>().speedPowerUpActive = true;
                powerUpSpawner.SpeedBoostActivated = true;
                break;
        }

        soundEffectsPlayer.playPowerUpActivatedSFX();

        while (timeLeft > 0)
        {
            powerUpTimerText.text = timeLeft.ToString();

            yield return new WaitForSeconds(1);

            timeLeft--;
        }

        switch (powerUp)
        {
            case 0:
                player.GetComponent<StealScript>().instantSteaalPowerUpActivated = false;
                powerUpSpawner.InstantStealActivated = false;
                powerUpSpawner.currentInstantStealPowerUp = null;
                break;
            case 1:
                player.GetComponent<StealScript>().invisibleCloakPowerUpActivated = false;
                powerUpSpawner.InvisibleCloakActivated = false;
                powerUpSpawner.currentInvisibleCloakPowerUp = null;
                break;
            case 2:
                player.GetComponent<PlayerMovement>().speedPowerUpActive = false;
                powerUpSpawner.SpeedBoostActivated = false;
                powerUpSpawner.currentSpeedBoostPowerUp = null;
                break;
        }

        Destroy(this.gameObject);
    }
}
