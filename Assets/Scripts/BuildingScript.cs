using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject player;
    public float buildingRemovalOffset;
    public BuildingSpawner buildingSpawner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (this.transform.position.z < player.transform.position.z + buildingRemovalOffset)
            {
                buildingSpawner.SpawnNewBuilding();
                Destroy(this.gameObject);
            }
        }
        
    }
}
