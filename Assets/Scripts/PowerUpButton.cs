using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(powerUpSpawner.powerUpTimerGameObject.activeInHierarchy == false)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
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

    public void PowerUpButtonClicked()
    {
        powerUpSpawner.PowerUpActivated = true;

        switch (powerUp)
        {
            case 0:
                powerUpSpawner.InstantStealActivated = true;
                player.GetComponent<StealScript>().callInstantStealPowerUp(instantStealPowerUpTime);
                powerUpSpawner.callTimer(instantStealPowerUpTime+(PlayerPrefs.GetInt("InstantStealTier")*3), this.gameObject);
                break;
            case 1:
                powerUpSpawner.InvisibleCloakActivated = true;
                player.GetComponent<StealScript>().callInvisibleCloakPowerUpPowerUp(invisibleCloakTime);
                powerUpSpawner.callTimer(invisibleCloakTime + (PlayerPrefs.GetInt("InvisibleCloakTier") * 3), this.gameObject);
                break;
            case 2:
                powerUpSpawner.SpeedBoostActivated = true;
                player.GetComponent<PlayerMovement>().callSpeedPowerUp(speedPowerUpMultiplier, speedPowerUpTime);
                powerUpSpawner.callTimer(speedPowerUpTime + (PlayerPrefs.GetInt("SpeedBoostTier") * 3), this.gameObject);
                break;
        }
        powerUpSpawner.numOfPowerUps--;
    }
}
