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
    void Start()
    {
        powerUpTimerText.text = "";
        StartCoroutine("PowerUpButtonClicked");
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
        powerUpSpawner.PowerUpActivated = true;

        switch (powerUp)
        {
            case 0:
                powerUpSpawner.InstantStealActivated = true;
                player.GetComponent<StealScript>().callInstantStealPowerUp(instantStealPowerUpTime + (PlayerPrefs.GetInt("InstantStealTier") * 3));
                callTimer(instantStealPowerUpTime+(PlayerPrefs.GetInt("InstantStealTier")*3));
                break;
            case 1:
                powerUpSpawner.InvisibleCloakActivated = true;
                player.GetComponent<StealScript>().callInvisibleCloakPowerUpPowerUp(invisibleCloakTime + (PlayerPrefs.GetInt("InvisibleCloakTier") * 3));
                callTimer(invisibleCloakTime + (PlayerPrefs.GetInt("InvisibleCloakTier") * 3));
                break;
            case 2:
                powerUpSpawner.SpeedBoostActivated = true;
                player.GetComponent<PlayerMovement>().callSpeedPowerUp(speedPowerUpMultiplier, speedPowerUpTime + (PlayerPrefs.GetInt("SpeedBoostTier") * 3));
                callTimer(speedPowerUpTime + (PlayerPrefs.GetInt("SpeedBoostTier") * 3));
                break;
        }
        powerUpSpawner.numOfPowerUps--;
    }

    public void callTimer(float time)
    {
        float t = time;
        StartCoroutine(PowerUpTimer(t));
    }

    IEnumerator PowerUpTimer(float time)
    {
        soundEffectsPlayer.playPowerUpActivatedSFX();
        float t = time;
        while (t > 0)
        {
            powerUpTimerText.text = t.ToString();
            yield return new WaitForSeconds(1);
            t--;
        }
        if (powerUp == 2)
        {
            player.GetComponent<PlayerMovement>().speedPowerUpMultiplier = 1;
        }
        Destroy(this.gameObject);
    }
}
