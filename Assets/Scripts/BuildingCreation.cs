using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreation : MonoBehaviour
{
    public GameObject[] buildingModels;
    public Color[] buildingColors;
    public Material commonMat;
    MeshRenderer buildingMesh;
    public int[] colorChance;
    public Color[] buildingColorsWithChance;
    // Start is called before the first frame update
    void Start()
    {
        int totalColorsNumber = 0;

        for (int k = 0; k < colorChance.Length; k++)
        {
            totalColorsNumber += colorChance[k];
        }
        buildingColorsWithChance = new Color[totalColorsNumber];

        int l = 0;
        for (int r = 0; r < buildingColors.Length; r++)
        {
            if (colorChance[r] > 1)
            {
                for (int j = 0; j < colorChance[r]; j++)
                {
                    buildingColorsWithChance[l] = buildingColors[r];
                    l++;
                }
            }
            else
            {
                buildingColorsWithChance[l] = buildingColors[r];
                l++;
            }
        }

        int randomIndex = Random.Range(0, buildingModels.Length);
        GetComponent<MeshFilter>().mesh = buildingModels[randomIndex].GetComponent<MeshFilter>().sharedMesh;
        buildingMesh = this.gameObject.GetComponent<MeshRenderer>();

        if (randomIndex != 6)
        {
            buildingMesh.materials[0].color = buildingColorsWithChance[Random.Range(0, buildingColorsWithChance.Length)];


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
