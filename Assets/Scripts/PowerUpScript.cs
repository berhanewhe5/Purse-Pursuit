using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public GameObject InstantStealPowerUp;
    public GameObject InvisibleCloakPowerUp;
    public GameObject SpeedBoostPowerUp;
    public GameObject player;
    [SerializeField] private float playerOffset;
    int powerUp;
    public PowerUpSpawner powerUpSpawner;
    ParticleSystem particleSystem;
    public Color[] powerUpPariclesColors;

    // Start is called before the first frame update
    void Start()
    {
        powerUp = Random.Range(0, 3);
        particleSystem = GetComponentInChildren<ParticleSystem>();
        var main = particleSystem.main;


        switch (powerUp)
        {
            case 0:
                InstantStealPowerUp.SetActive(true);
                main.startColor = powerUpPariclesColors[0];
                break;
            case 1:
                InvisibleCloakPowerUp.SetActive(true);
                main.startColor = powerUpPariclesColors[1];
                break;
            case 2:
                SpeedBoostPowerUp.SetActive(true);
                main.startColor = powerUpPariclesColors[2];
                break;
        }


    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            
            powerUpSpawner.AddPowerUp(powerUp);            
            
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < player.transform.position.z + playerOffset)
        {
            Destroy(this.gameObject);
        }
    }
}
