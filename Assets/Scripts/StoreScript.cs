using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Rendering;

public class StoreScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject costumesPanel;
    public GameObject buyMoreCashPanel;
    public GameObject upgradesPanel;
    public GameObject store;
    public Button costumesButton;
    public Button upgradesButton;
    public Button buyMoreCashButton;
    public TMP_Text storeTitle;

    public TMP_Text costumeName;
    public TMP_Text costumePrice;

    string currentCostume;
    public TMP_Text buyCostumeText;
    public RawImage currentCostumeImage;
    public Texture guyTexture;
    public Texture firefighterTexture;
    public Texture princessTexture;
    public Texture nerdTexture;
    public Texture criminalTexture;
    public Texture konitaTexture;

    bool buyButtonState;
    int currentPrice;

    public TMP_Text moneyText;
    public GameObject[] costumes;
    public int costumeKey;

    public Image[] speedBoostTiers;
    public Image[] instantStealTiers;
    public Image[] invisibleCloakTiers;
    public Image[] multiplierTiers;

    public TMP_Text upgradePrice;

    int speedBoostCurrentTier;
    int instantStealCurrentTier;
    int invisibleCloakCurrentTier;
    int multiplierCurrentTier;

    public Button speedBoostButton;
    public Button instantStealButton;
    public Button invisibleCloakButton;
    public Button multiplierButton;


    /*prices of upgrades
     * tier 1 = 7000 +3 seconds
     * tier 2 = 20000 +3 seconds
     * tier 3 = 52000 +3 seconds
     * tier 4 = 160000 +3 seconds
     * tier 5 = 280000 +3 seconds
     */

    public int tier1Price = 7000;
    public int tier2Price = 20000;
    public int tier3Price = 52000;
    public int tier4Price = 160000;
    public int tier5Price = 280000;

    public Button buyCostumeButton;
    public Button buyUpgradeButton;

    public void setCostumeName(string name)
    {
        costumeName.text = name;
        currentCostume = name;


    }

    public void setCostumePrice(int price) {
        if (PlayerPrefs.GetInt(currentCostume+"Purchased") == 0)
        {
            Debug.Log(currentCostume + "Purchased");
            costumePrice.text = "$" + price.ToString();
            if (costumeKey == 6)
            {

                costumePrice.text = "Subscribe to Konita";
                buyCostumeText.text = "Subscribe";
            }
            else { 
                buyCostumeText.text = "Buy";
                
            }
            buyButtonState = true;
            currentPrice = price;

            if (PlayerPrefs.GetInt("Money") >= currentPrice)
            {
                buyCostumeButton.interactable = true;

                if (PlayerPrefs.GetInt(currentCostume + "Selected") == 1)
                {
                    costumePrice.text = "";
                    buyCostumeText.text = "Select";
                    buyCostumeButton.interactable = false;
                }
            }
            else { 
                buyCostumeButton.interactable = false;
            }
        }
        else
        {
            costumePrice.text = "";
            buyCostumeText.text = "Select";
            buyButtonState = false;
            buyCostumeButton.interactable = true;


            if (PlayerPrefs.GetInt(currentCostume + "Selected") == 1)
            {
                costumePrice.text = "";
                buyCostumeText.text = "Select";
                buyCostumeButton.interactable = false;
            }
        }
    }

    public void setCostumeKey(int key)
    {
        costumeKey = key;
    }

    public void setCostumeImage(Texture texture)
    {
        currentCostumeImage.texture = texture;
    }
    public void PurchaseCash(int amount)
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + amount);
    }

    public void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
    }


    void Start()
    {
        if (PlayerPrefs.GetInt("GuySelected") == 1)
        {
            costumeKey = 1;
        }
        else if (PlayerPrefs.GetInt("FirefighterSelected") == 1)
        {
            costumeKey = 2;
        }
        else if (PlayerPrefs.GetInt("PrincessSelected") == 1)
        {
            costumeKey = 3;
        }
        else if (PlayerPrefs.GetInt("NerdSelected") == 1)
        {
            costumeKey = 4;
        }
        else if (PlayerPrefs.GetInt("CriminalSelected") == 1)
        {
            costumeKey = 5;
        }
        else if (PlayerPrefs.GetInt("KonitaSelected") == 1)
        {
            costumeKey = 6;
        }
        else
        {
            costumeKey = 1;
        }

        SetCostume();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + PlayerPrefs.GetInt("Money").ToString();
        GetComponent<GameManagerScript>().menuMoneyText.text = "$"+PlayerPrefs.GetInt("Money").ToString();
        if (costumesPanel.activeInHierarchy)
        {
            costumesButton.Select();
        }
        else if (upgradesPanel.activeInHierarchy)
        {
            upgradesButton.Select();
        }
        else if (buyMoreCashPanel.activeInHierarchy)
        {
            buyMoreCashButton.Select();
        }
    }

    public void CostumeButton()
    {
        ExitAllPanels();

        if (PlayerPrefs.GetInt("GuySelected") == 1)
        {
            setCostumeName("Guy");
            setCostumeImage(guyTexture);
            costumePrice.text = "";
            buyCostumeText.text = "Select";
            buyCostumeButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("FirefighterSelected") == 1)
        {
            setCostumeName("Firefighter");
            setCostumeImage(firefighterTexture);
            costumePrice.text = "";
            buyCostumeText.text = "Select";
            buyCostumeButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("PrincessSelected") == 1)
        {
            setCostumeName("Princess");
            setCostumeImage(princessTexture);
            costumePrice.text = "";
            buyCostumeText.text = "Select";
            buyCostumeButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("NerdSelected") == 1)
        {
            setCostumeName("Nerd");
            setCostumeImage(nerdTexture);
            costumePrice.text = "";
            buyCostumeText.text = "Select";
            buyCostumeButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("CriminalSelected") == 1)
        {
            setCostumeName("Criminal");
            setCostumeImage(criminalTexture);
            costumePrice.text = "";
            buyCostumeText.text = "Select";
            buyCostumeButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("KonitaSelected") == 1)
        {
            setCostumeName("Konita");
            setCostumeImage(konitaTexture);
            costumePrice.text = "";
            buyCostumeText.text = "Select";
            buyCostumeButton.interactable = false;
        }
        else
        {
            setCostumeName("Guy");
            setCostumeImage(guyTexture);
            costumePrice.text = "";
            buyCostumeText.text = "Select";
            buyCostumeButton.interactable = false;
        }

        costumesButton.Select();
        costumesPanel.SetActive(true);
        storeTitle.text = "Costumes";
        currentPrice = 0;

    }

    public void BuyMoreCashButton()
    {
        ExitAllPanels();
        buyMoreCashPanel.SetActive(true);
        storeTitle.text = "Buy More Cash";
        currentPrice = 0;
    }

    public void UpgradesButton()
    {
        ExitAllPanels();
        upgradesPanel.SetActive(true);
        ReCheckTierValues();
        storeTitle.text = "Power Ups";
        currentPrice = 0;
    }

    public void ExitStore()
    {
        store.SetActive(false); 
    }
    public void ExitAllPanels()
    {
        costumesPanel.SetActive(false);
        buyMoreCashPanel.SetActive(false);
        upgradesPanel.SetActive(false);
    }

    public void BuyCostumeButton()
    {
        if (buyButtonState)
        {
            if (PlayerPrefs.GetInt("Money") >= currentPrice)
            {
                if (costumeKey == 6)
                {
                    GetComponent<GameManagerScript>().SubscribeToKonita();
                }
                else { 
                    PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - currentPrice);
                }
                PlayerPrefs.SetInt(currentCostume + "Purchased", 1);
                costumePrice.text = "";
                buyCostumeText.text = "Select";
                buyButtonState = false;
                GetComponent<SoundEffectsPlayer>().playPurchaseItemSFX();
            }
            SetCostume();
        }
        else {
            SetCostume();
        }
    }

    void SetCostume()
    {
        int tempCostumeKey = costumeKey - 1;
        for (int i = 0; i < costumes.Length; i++)
        {
            if (i == tempCostumeKey)
            {
                costumes[i].SetActive(true);
            }
            else
            {
                costumes[i].SetActive(false);
            }
        }

        resetSelectedCostume();
        PlayerPrefs.SetInt(currentCostume + "Selected", 1);

        if (PlayerPrefs.GetInt(currentCostume + "Selected") == 1)
        {
            costumePrice.text = "";
            buyCostumeText.text = "Select";
            buyCostumeButton.interactable = false;
        }
    }

    public void resetSelectedCostume()
    {
        PlayerPrefs.SetInt("GuySelected", 0);
        PlayerPrefs.SetInt("FirefighterSelected", 0);
        PlayerPrefs.SetInt("PrincessSelected", 0);
        PlayerPrefs.SetInt("NerdSelected", 0);
        PlayerPrefs.SetInt("CriminalSelected", 0);
        PlayerPrefs.SetInt("KonitaSelected", 0);
    }

    public void ReCheckTierValues()
    {
        //speedBoostTiers
        for (int i = 0; i < speedBoostTiers.Length; i++)
        {
            speedBoostTiers[i].color = Color.green;
        }

        for (int i = PlayerPrefs.GetInt("SpeedBoostTier"); i < speedBoostTiers.Length; i++)
        {
            speedBoostTiers[i].color = Color.white;
        }

        //instantStealTiers
        for (int i = 0; i < instantStealTiers.Length; i++)
        {
            instantStealTiers[i].color = Color.green;
        }

        for (int i = PlayerPrefs.GetInt("InstantStealTier"); i < instantStealTiers.Length; i++)
        {
            instantStealTiers[i].color = Color.white;
        }

        //invisibleCloakTiers
        for (int i = 0; i < invisibleCloakTiers.Length; i++)
        {
            invisibleCloakTiers[i].color = Color.green;
        }

        for (int i = PlayerPrefs.GetInt("InvisibleCloakTier"); i < invisibleCloakTiers.Length; i++)
        {
            invisibleCloakTiers[i].color = Color.white;
        }

        //multiplierTiers
        for (int i = 0; i < multiplierTiers.Length; i++)
        {
            multiplierTiers[i].color = Color.green;
        }
        for (int i = PlayerPrefs.GetInt("MultiplierTier"); i < multiplierTiers.Length; i++)
        {
            multiplierTiers[i].color = Color.white;
        }

        if ((PlayerPrefs.GetInt("SpeedBoostTier") == 5))
        {
            speedBoostButton.interactable = false;
        }
        else
        {
            speedBoostButton.interactable = true;
        }
        if ((PlayerPrefs.GetInt("InstantStealTier") == 5))
        {
            instantStealButton.interactable = false;
        }
        else
        {
            instantStealButton.interactable = true;
        }
        if ((PlayerPrefs.GetInt("InvisibleCloakTier") == 5))
        {
            invisibleCloakButton.interactable = false;
        }
        else
        {
            invisibleCloakButton.interactable = true;
        }
        if ((PlayerPrefs.GetInt("MultiplierTier") == 5))
        {
            multiplierButton.interactable = false;
        }
        else
        {
            multiplierButton.interactable = true;
        }
        speedBoostCurrentTier = 0;
        invisibleCloakCurrentTier = 0;
        instantStealCurrentTier = 0;
        multiplierCurrentTier = 0;
        currentPrice = 0;
        upgradePrice.text = "Price:";
    }
    public void IncreaseSpeedBoostButton()
    {
        if ((PlayerPrefs.GetInt("SpeedBoostTier") + speedBoostCurrentTier) != 5)
        {
            speedBoostTiers[PlayerPrefs.GetInt("SpeedBoostTier") + speedBoostCurrentTier].color = new Color32(161, 255, 161, 255);

            upgradePrice.text = "Price: " + DetermineCurrentUpgradePrice(PlayerPrefs.GetInt("SpeedBoostTier") +speedBoostCurrentTier).ToString();
            speedBoostCurrentTier++;
            GetComponent<SoundEffectsPlayer>().playIncreaseUpgradeSFX();
            if ((PlayerPrefs.GetInt("SpeedBoostTier") + speedBoostCurrentTier) == 5)
            {
                speedBoostButton.interactable = false;
            }
        }
    }

    public void IncreaseInstantStealButton()
    {
        if ((PlayerPrefs.GetInt("InstantStealTier") + instantStealCurrentTier) != 5)
        {
            instantStealTiers[PlayerPrefs.GetInt("InstantStealTier") + instantStealCurrentTier].color = new Color32(161, 255, 161, 255);

            upgradePrice.text = "Price: " + DetermineCurrentUpgradePrice(PlayerPrefs.GetInt("InstantStealTier") + instantStealCurrentTier).ToString();
            instantStealCurrentTier++;
            GetComponent<SoundEffectsPlayer>().playIncreaseUpgradeSFX();
            if ((PlayerPrefs.GetInt("InstantStealTier") + instantStealCurrentTier) == 5)
            {
                instantStealButton.interactable = false;
            }
        }
    }

    public void IncreaseInvisibleCloak()
    {
        if ((PlayerPrefs.GetInt("InvisibleCloakTier") + invisibleCloakCurrentTier) != 5)
        {
            invisibleCloakTiers[PlayerPrefs.GetInt("InvisibleCloakTier") + invisibleCloakCurrentTier].color = new Color32(161, 255, 161, 255);

            upgradePrice.text = "Price: " + DetermineCurrentUpgradePrice(PlayerPrefs.GetInt("InvisibleCloakTier") + invisibleCloakCurrentTier).ToString();
            invisibleCloakCurrentTier++;
            GetComponent<SoundEffectsPlayer>().playIncreaseUpgradeSFX();
            if ((PlayerPrefs.GetInt("InvisibleCloakTier") + invisibleCloakCurrentTier) == 5)
            {
                invisibleCloakButton.interactable = false;
            }
        }
    }

    public void IncreaseMultiplier()
    {
        if ((PlayerPrefs.GetInt("MultiplierTier") + multiplierCurrentTier) != 5)
        {
            multiplierTiers[PlayerPrefs.GetInt("MultiplierTier") + multiplierCurrentTier].color = new Color32(161, 255, 161, 255);

            upgradePrice.text = "Price: " + DetermineCurrentUpgradePrice(PlayerPrefs.GetInt("MultiplierTier") + multiplierCurrentTier).ToString();
            multiplierCurrentTier++;
            GetComponent<SoundEffectsPlayer>().playIncreaseUpgradeSFX();
            if ((PlayerPrefs.GetInt("MultiplierTier") + multiplierCurrentTier) == 5)
            {
                multiplierButton.interactable = false;
            }
        }
    }
    public int DetermineCurrentUpgradePrice(int currentTier)
    {
       switch (currentTier+1)
        {
            case 1:
                currentPrice += tier1Price;
                break;
            case 2:
                currentPrice += tier2Price;
                break;
            case 3:
                currentPrice += tier3Price;
                break;
            case 4:
                currentPrice += tier4Price;
                break;
            case 5:
                currentPrice += tier5Price;
                break;
        }

        return currentPrice;
    }

    public void BuyUpgradeButton()
    {
        if (currentPrice != 0)
        {
            if (PlayerPrefs.GetInt("Money") >= currentPrice)
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - currentPrice);
                PlayerPrefs.SetInt("SpeedBoostTier", PlayerPrefs.GetInt("SpeedBoostTier") + speedBoostCurrentTier);
                PlayerPrefs.SetInt("InstantStealTier", PlayerPrefs.GetInt("InstantStealTier") + instantStealCurrentTier);
                PlayerPrefs.SetInt("InvisibleCloakTier", PlayerPrefs.GetInt("InvisibleCloakTier") + invisibleCloakCurrentTier);
                PlayerPrefs.SetInt("MultiplierTier", PlayerPrefs.GetInt("MultiplierTier") + multiplierCurrentTier);
                Debug.Log("SpeedBoostTier: " + PlayerPrefs.GetInt("SpeedBoostTier"));
                ReCheckTierValues();
                GetComponent<SoundEffectsPlayer>().playPurchaseItemSFX();
            }
        }
    }
}
