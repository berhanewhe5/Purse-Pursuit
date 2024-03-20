using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivillianCreation : MonoBehaviour
{
    public bool isManNPC;
    public bool isWomanNPC;
    public GameObject[] hairModels;
    public GameObject glasses;
    public GameObject suit;
    public GameObject belt;
    public GameObject dress;
    public GameObject skirt;
    public GameObject tie;
    public Color[] skinColors;
    public Color[] hairColors;
    public Color[] shirtColors;
    public Color[] suitColors;
    public Color[] pantsColors;
    public Color[] beltColors;
    public Color[] tieColors;
    public Color[] shoeColors;
    public Color[] glassesColor;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    public int skinMat;
    public int vestMat;
    public int tShirtMat;
    public int longSleeveShirtMat;
    public int shortMat;
    public int pantsMat;
    public int shoesMat;

    public bool hasGlasses;
    public bool hasSuit;

    int hairChance;
    int glassesChance;
    int shirtChance; // 0 - vest, 1 - tshirt, 2 - longsleeve
    int pantsChance; // 0 - pants, 1 - shorts, 2 - skirt, 3 - dress
    int beltChance;
    int tieChance;
    int suitChance;



    // Start is called before the first frame update
    void Start()
    {
        if (isManNPC)
        {
            hairChance = Random.Range(0, hairModels.Length+1);
            pantsChance = Random.Range(0, 2);
            suitChance = Random.Range(0, 9);
        }
        else if (isWomanNPC)
        {
            hairChance = Random.Range(0, hairModels.Length);
            pantsChance = Random.Range(0, 4);
        }
        Color skinColor = skinColors[Random.Range(0, skinColors.Length)];
        Color shirtColor = shirtColors[Random.Range(0, shirtColors.Length)];
        Color pantsColor = pantsColors[Random.Range(0, pantsColors.Length)];

        glassesChance = Random.Range(0, 5);
        shirtChance = Random.Range(0, 3);
        skinnedMeshRenderer.materials[skinMat].color = skinColor;
        
        if (hairChance != hairModels.Length)
        {
            hairModels[hairChance].SetActive(true);
            hairModels[hairChance].GetComponent<SkinnedMeshRenderer>().material.color = hairColors[Random.Range(0, hairColors.Length)];
        }
        if (glassesChance == 4)
        {
            glasses.SetActive(true);
            glasses.GetComponent<SkinnedMeshRenderer>().material.color = glassesColor[Random.Range(0, glassesColor.Length)];
            hasGlasses = true;
        }
        if (suitChance == 8)
        {
            suit.SetActive(true);
            Color suitColor = suitColors[Random.Range(0, suitColors.Length)];
            suit.GetComponent<SkinnedMeshRenderer>().material.color = suitColor;
            skinnedMeshRenderer.materials[vestMat].color = Color.white;
            skinnedMeshRenderer.materials[tShirtMat].color = Color.white;
            skinnedMeshRenderer.materials[shortMat].color = suitColor;
            skinnedMeshRenderer.materials[pantsMat].color = suitColor;
            hasSuit = true;
            tieChance = Random.Range(0, 2);
            if (tieChance == 1)
            {
                tie.SetActive(true);
                tie.GetComponent<SkinnedMeshRenderer>().material.color = tieColors[Random.Range(0, tieColors.Length)];
            }

        }
        else {
            if (shirtChance == 0)
            {
                skinnedMeshRenderer.materials[vestMat].color = shirtColor;
                skinnedMeshRenderer.materials[tShirtMat].color = skinColor;
                skinnedMeshRenderer.materials[longSleeveShirtMat].color = skinColor;
            }
            else if (shirtChance == 1)
            {
                skinnedMeshRenderer.materials[vestMat].color = shirtColor;
                skinnedMeshRenderer.materials[tShirtMat].color = shirtColor;
                skinnedMeshRenderer.materials[longSleeveShirtMat].color = skinColor;
            }
            else if (shirtChance == 2)
            {
                skinnedMeshRenderer.materials[vestMat].color = shirtColor;
                skinnedMeshRenderer.materials[tShirtMat].color = shirtColor;
                skinnedMeshRenderer.materials[longSleeveShirtMat].color = shirtColor;
            }

            if (pantsChance == 0)
            {
                skinnedMeshRenderer.materials[shortMat].color = pantsColor;
                skinnedMeshRenderer.materials[pantsMat].color = skinColor;
            }
            else if (pantsChance == 1)
            {
                skinnedMeshRenderer.materials[shortMat].color = pantsColor;
                skinnedMeshRenderer.materials[pantsMat].color = pantsColor;
                if (isManNPC)
                {
                    beltChance = Random.Range(0, 2);
                    if (beltChance == 1)
                    {
                        belt.SetActive(true);
                        belt.GetComponent<SkinnedMeshRenderer>().material.color = beltColors[Random.Range(0, beltColors.Length)];
                    }
                }
            }
            else if (pantsChance == 2)
            {
                dress.SetActive(true);
                dress.GetComponent<SkinnedMeshRenderer>().material.color = shirtColor;
            }
            else if (pantsChance == 3)
            {
                skirt.SetActive(true);
                skirt.GetComponent<SkinnedMeshRenderer>().material.color = pantsColors[Random.Range(0, pantsColors.Length)];
                skinnedMeshRenderer.materials[pantsMat].color = skinColor;
            }
        }

        skinnedMeshRenderer.materials[shoesMat].color = shoeColors[Random.Range(0, shoeColors.Length)];



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
