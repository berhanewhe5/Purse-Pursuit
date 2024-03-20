using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class civillianSpawner : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private GameObject civillianMan;
    [SerializeField] private GameObject civillianWoman;
    public int civillianLimit;
    public int civillianCount;
    public int initialCivillianCount;

    public float minWaitTime;
    public float maxWaitTime;

    public float[] spawnPositions;
    [SerializeField] private float civillianFowardOffset = 100;

    [SerializeField]private float civillianSpawnY;
    private float civillianSpawnZ;

    public StealScript stealScript;

    public float minInitialZ=3;
    public float maxInitialZ=5;
    public float zPosition=0;

    public SoundEffectsPlayer soundEffectsPlayer;
    void Start()
    {
        civillianCount = 0;
        float initialZ = maxInitialZ*2;
        while(zPosition < civillianFowardOffset) {
            zPosition = Random.Range(minInitialZ, maxInitialZ);

            minInitialZ += initialZ;
            maxInitialZ += initialZ;
            
            civillianSpawnZ = player.transform.position.z + zPosition;

            int num = Random.Range(0, spawnPositions.Length);
            while (num == 2)
            {
                num = Random.Range(0, spawnPositions.Length);
            }
            Vector3 spawnPosition = new Vector3(spawnPositions[num], civillianSpawnY, civillianSpawnZ);
            int civillianType = Random.Range(0, 2);
            if (civillianType == 0)
            {
                GameObject currentCivillian = Instantiate(civillianMan, spawnPosition, transform.rotation);
                currentCivillian.GetComponent<CivillianMovement>().player = player;
                currentCivillian.GetComponent<CivillianMovement>().civillianSpawner = this.gameObject.GetComponent<civillianSpawner>();
                currentCivillian.GetComponentInChildren<BumpPlayerScript>().soundEffectsPlayer = soundEffectsPlayer;
            }
            else{
                GameObject currentCivillian = Instantiate(civillianWoman, spawnPosition, transform.rotation);
                currentCivillian.GetComponent<CivillianMovement>().player = player;
                currentCivillian.GetComponent<CivillianMovement>().civillianSpawner = this.gameObject.GetComponent<civillianSpawner>();
                currentCivillian.GetComponentInChildren<BumpPlayerScript>().soundEffectsPlayer = soundEffectsPlayer;
            }

            civillianCount += 1;
        }
        StartCoroutine(SpawnCivillian(civillianFowardOffset));
    }

    void Update()
    {
        
    }

    IEnumerator SpawnCivillian(float position)
    {
        while (true) { 
            while (civillianCount < civillianLimit)
            {
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

                civillianSpawnZ = player.transform.position.z + position;
                if (!stealScript.gameActive)
                {
                    int num = Random.Range(0, spawnPositions.Length);

                    while (num == 2)
                    {
                        num = Random.Range(0, spawnPositions.Length);
                    }
                    Vector3 spawnPosition = new Vector3(spawnPositions[num], civillianSpawnY, civillianSpawnZ);
                    int civillianType = Random.Range(0, 2);
                    if (civillianType == 0)
                    {
                        GameObject currentCivillian = Instantiate(civillianMan, spawnPosition, transform.rotation);
                        currentCivillian.GetComponent<CivillianMovement>().player = player;
                        currentCivillian.GetComponent<CivillianMovement>().civillianSpawner = this.gameObject.GetComponent<civillianSpawner>();
                        currentCivillian.GetComponentInChildren<BumpPlayerScript>().soundEffectsPlayer = soundEffectsPlayer;
                    }
                    else
                    {
                        GameObject currentCivillian = Instantiate(civillianWoman, spawnPosition, transform.rotation);
                        currentCivillian.GetComponent<CivillianMovement>().player = player;
                        currentCivillian.GetComponent<CivillianMovement>().civillianSpawner = this.gameObject.GetComponent<civillianSpawner>();
                        currentCivillian.GetComponentInChildren<BumpPlayerScript>().soundEffectsPlayer = soundEffectsPlayer;
                    }

                }
                else {
                    int num = Random.Range(0, spawnPositions.Length);
                    Vector3 spawnPosition = new Vector3(spawnPositions[num], civillianSpawnY, civillianSpawnZ);
                    int civillianType = Random.Range(0, 2);
                    if (civillianType == 0)
                    {
                        GameObject currentCivillian = Instantiate(civillianMan, spawnPosition, transform.rotation);
                        currentCivillian.GetComponent<CivillianMovement>().player = player;
                        currentCivillian.GetComponent<CivillianMovement>().civillianSpawner = this.gameObject.GetComponent<civillianSpawner>();
                        currentCivillian.GetComponentInChildren<BumpPlayerScript>().soundEffectsPlayer = soundEffectsPlayer;
                    }
                    else
                    {
                        GameObject currentCivillian = Instantiate(civillianWoman, spawnPosition, transform.rotation);
                        currentCivillian.GetComponent<CivillianMovement>().player = player;
                        currentCivillian.GetComponent<CivillianMovement>().civillianSpawner = this.gameObject.GetComponent<civillianSpawner>();
                        currentCivillian.GetComponentInChildren<BumpPlayerScript>().soundEffectsPlayer = soundEffectsPlayer;
                    }
                }

                civillianCount += 1;
            }
        }
    }

}