using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCreation : MonoBehaviour
{
    public GameObject[] carModels;
    public Color[] carColors;
    public int[] colorChance;
    [SerializeField] MeshFilter carMesh;
    public Material carColor;
    public Material commonMaterial;
    public Color[] carColorsWithChance;
    void Start()
    {
        carMesh = GetComponent<MeshFilter>();
        int totalColorsNumber = 0;

        for (int k = 0; k < colorChance.Length; k++)
        {
           totalColorsNumber += colorChance[k];
        }
        carColorsWithChance = new Color[totalColorsNumber];

        int l = 0;
        for (int r = 0; r < carColors.Length; r++)
        {
            if (colorChance[r] > 1)
            {
                for (int j = 0; j < colorChance[r]; j++)
                {
                    carColorsWithChance[l] = carColors[r];
                    l++;
                }
            }
            else {
                carColorsWithChance[l] = carColors[r];
                l++;
            }
        }

        int i = Random.Range(0, carModels.Length);
        carMesh.mesh = carModels[i].GetComponent<MeshFilter>().sharedMesh;
        carColor.color = carColorsWithChance[Random.Range(0, carColorsWithChance.Length)];
        if (i == 3 || i == 4)
        {
            carColor.color = carColors[Random.Range(0, carColors.Length)];
            List<Material> carMaterials = new List<Material> { commonMaterial, GetComponent<MeshRenderer>().materials[1] };
            GetComponent<MeshRenderer>().SetMaterials(carMaterials);
        }
        else {
            GetComponent<MeshRenderer>().materials[1] = commonMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
