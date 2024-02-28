using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject Building;
    [SerializeField] GameObject player;
    public float nextBuildingZPosition;
    public float buildingZIncrement;
    public int buildingCount;
    public int numOfBeginningBuildings;

    //-26.63

    void Start()
    {
        for (int i = 0; i < numOfBeginningBuildings; i++)
        {
            GameObject newBuildingRight = Instantiate(Building, new Vector3(1f, 0.24f, nextBuildingZPosition), transform.rotation);

            newBuildingRight.GetComponent<BuildingScript>().player = player;
            newBuildingRight.GetComponent<BuildingScript>().buildingSpawner = this;
            newBuildingRight.transform.eulerAngles = new Vector3(0f, 0f, 0f);

            GameObject newBuildingLeft = Instantiate(Building, new Vector3(-19.78f, 0.24f, nextBuildingZPosition), transform.rotation);
            newBuildingLeft.GetComponent<BuildingScript>().player = player;
            newBuildingLeft.GetComponent<BuildingScript>().buildingSpawner = this;
            newBuildingLeft.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            nextBuildingZPosition += buildingZIncrement;

            buildingCount++;

        }
    }

    public void SpawnNewBuilding()
    {
        GameObject newBuildingRight = Instantiate(Building, new Vector3(1f, 0.24f, nextBuildingZPosition), transform.rotation);

        newBuildingRight.GetComponent<BuildingScript>().player = player;
        newBuildingRight.GetComponent<BuildingScript>().buildingSpawner = this;
        newBuildingRight.transform.eulerAngles = new Vector3(0f, 0f, 0f);

        GameObject newBuildingLeft = Instantiate(Building, new Vector3(-19.78f, 0.24f, nextBuildingZPosition), transform.rotation);
        newBuildingLeft.GetComponent<BuildingScript>().player = player;
        newBuildingLeft.GetComponent<BuildingScript>().buildingSpawner = this;
        newBuildingLeft.transform.eulerAngles = new Vector3(0f, 180f, 0f);

        nextBuildingZPosition += buildingZIncrement;

        buildingCount++;
    }
}
