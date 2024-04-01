using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public float minWaitTime, maxWaitTime;

    public int previousLane;
    public int currentLane;
    public float backOfPlayerOffset;
    public float frontOfPlayerOffset;
    public GameObject car;
    [SerializeField] float[] xCarPositions;
    [SerializeField] Transform player;
    public bool isPlayerBusted;
    public GameObject StopPoliceCarTrigger;

    public float carYPosition;
    void Start()
    {
        StartCoroutine("carTimer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator carTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            while (currentLane == previousLane)
            {
                if (isPlayerBusted)
                {
                    currentLane = Random.Range(2, 4);
                }
                else
                {
                    currentLane = Random.Range(1, 4);
                }
            }
            previousLane = currentLane;

            if (currentLane == 1 || currentLane == 2)
            {
                GameObject newCar = Instantiate(car, new Vector3(xCarPositions[currentLane-1], carYPosition, player.position.z - backOfPlayerOffset), transform.rotation);
                newCar.transform.eulerAngles = new Vector3(0, 0, 0);
                newCar.GetComponent<CarMovement>().soundEffectsPlayer = GetComponent<SoundEffectsPlayer>();
                newCar.GetComponent<CarMovement>().row = currentLane;

                if (currentLane == 1)
                {
                    newCar.GetComponent<CarMovement>().StopPoliceCarTrigger = StopPoliceCarTrigger;
                }
            }
            else if (currentLane == 3 || currentLane == 4)
            {
                GameObject newCar = Instantiate(car, new Vector3(xCarPositions[currentLane-1], carYPosition, player.position.z + frontOfPlayerOffset), transform.rotation);
                newCar.transform.eulerAngles = new Vector3(0, 180f, 0);
                newCar.GetComponent<CarMovement>().soundEffectsPlayer = GetComponent<SoundEffectsPlayer>();
                newCar.GetComponent<CarMovement>().row = currentLane;
            }
        }

    }
}
