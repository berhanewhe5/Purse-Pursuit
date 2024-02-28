using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PowerUpSpawner : MonoBehaviour
{
    public float[] spawnPositions;
    public float minWaitTime;
    public float maxWaitTime;
    public GameObject powerUp;
    public GameObject player;
    public float powerUpSpawnY;
    public float powerUpSpawnZ;
    public StealScript stealScript;
    public float zOffset;


    public bool PowerUpActivated;
    public bool SpeedBoostActivated;
    public bool InvisibleCloakActivated;
    public bool InstantStealActivated;

    public GameObject powerUpTimerGameObject;
    public GameObject powerUpContainer;
    public TMP_Text powerUpTimerText;
    public float powerUpTimer;

    public GameObject powerUpButton;
    public int numOfPowerUps;
    public int maxPowerUps;

    // Start is called before the first frame update
    void Start()
    {
        powerUpTimerGameObject.SetActive(false);
        StartCoroutine(SpawnPowerUps(zOffset));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnPowerUps(float position)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

            powerUpSpawnZ = player.transform.position.z + position;
            if (stealScript.gameActive)
            {
                int num = Random.Range(0, spawnPositions.Length);

                Vector3 spawnPosition = new Vector3(spawnPositions[num], powerUpSpawnY, powerUpSpawnZ);

                GameObject currentPowerUp = Instantiate(powerUp, spawnPosition, transform.rotation);
                currentPowerUp.GetComponent<PowerUpScript>().powerUpSpawner = this;
                currentPowerUp.GetComponent<PowerUpScript>().player = player;
            }
        }
    }

    public void callTimer(float time, GameObject powerUpButton)
    {
        float t = time;
        StartCoroutine(PowerUpTimer(t, powerUpButton));
    }

    public IEnumerator PowerUpTimer(float time, GameObject powerUpButton)
    {
        powerUpTimerGameObject.SetActive(true);
        GetComponent<SoundEffectsPlayer>().playPowerUpActivatedSFX();
        float t = time;
        while (t > 0)
        {
            powerUpTimerText.text = t.ToString();
            yield return new WaitForSeconds(1);
            t--;
        }
        Destroy(powerUpButton);

        powerUpTimerGameObject.SetActive(false);
    }

    public void AddPowerUp(int powerUp)
    {
        if (numOfPowerUps < maxPowerUps)
        {
            GameObject currentPowerUpButton = Instantiate(powerUpButton, powerUpContainer.transform);
            currentPowerUpButton.GetComponent<PowerUpButton>().powerUp = powerUp;
            currentPowerUpButton.GetComponent<PowerUpButton>().powerUpSpawner = this;
            currentPowerUpButton.GetComponent<PowerUpButton>().player = player;
            currentPowerUpButton.GetComponent<PowerUpButton>().SetPowerUpImage();

            numOfPowerUps++;
            GetComponent<SoundEffectsPlayer>().playPowerUpCollectedSFX();
        }
    }
}
