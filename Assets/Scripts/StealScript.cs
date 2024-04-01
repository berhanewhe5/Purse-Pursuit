using System;
using System.Collections;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StealScript : MonoBehaviour
{
    [SerializeField] float stealColliderRadius;
    [SerializeField] float rayLength;
    [SerializeField] GameObject stealSlider;
    [SerializeField] GameObject stealSliderComponent;
    [SerializeField] LayerMask civillianColliderMask;

    public float slowedDownTime;
    public bool inStealingZone;

    public RectTransform greenArea;

    public bool gameActive;

    public int minToSteal;
    public int maxToSteal;
    public int moneyIncrement;
    public float minToLose;
    public float maxToLose;

    public int Money;
    public float multiplier=1;
    public float clothesMultiplier;
    [SerializeField] private TMP_Text moneyText;
    public TMP_Text adMoneyText;

    [SerializeField] private GameManagerScript gameManagerScript;
    [SerializeField] SliderController sliderController;
    [SerializeField] StealGoalScript stealGoalScript;

    //The highest position the handle can be and still be in the green area.
    public float topHandlePos;
    //The lowest position the handle can be and still be in the green area.
    public float bottomHandlePos;


    public float maxGreenAreaSize;
    public float minGreenAreaSize;

    public float greenAreaMaxReductionRate;
    public float greenAreaMinReductionRate;
    public float greenAreaReduction;

    public bool instantSteaalPowerUpActivated;
    public bool invisibleCloakPowerUpActivated;

    public SoundEffectsPlayer soundEffectsPlayer;
    public AudioSource playerWalkingAudioSource;
    public Animator increaseMoneyAlertAnimator;
    public TMP_Text increaseMoneyAlertText;
    public GameManagerScript gameManager;

    public int stealAttempts;
    public bool canSteal;
    public bool firstStealCompleted = false;

    bool sfxPlayed;

    public GameObject newHighScoreTextInGame;
    bool newHighscoreComplete;

    private void Start()
    {
        newHighscoreComplete = false;
        moneyIncrement = 0;
        ChooseRandomGreenArea();
        gameActive = false;

        Money = 0;
        moneyText.text = "$" + Money.ToString();

        if(PlayerPrefs.GetInt("GamePlayedBefore") == 1)
        {
            canSteal = true;
        }
        else
        {
            canSteal = false;
        }
    }

    void Update()
    {
        //Enable and Disable Steal Slider
        if (gameActive)
        {
            if ((Physics.CheckSphere(this.transform.position, stealColliderRadius, civillianColliderMask) == true) && (Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), this.transform.forward * rayLength, rayLength, civillianColliderMask) == false))
            {
                inStealingZone = true;
                Collider[] civillian = Physics.OverlapSphere(this.transform.position, stealColliderRadius, civillianColliderMask);
                
                if (civillian[0].transform.parent.gameObject.GetComponent<CivillianCreation>().hasGlasses && civillian[0].transform.parent.gameObject.GetComponent<CivillianCreation>().hasSuit)
                {
                    clothesMultiplier = 2f;
                }
                else if (civillian[0].transform.parent.gameObject.GetComponent<CivillianCreation>().hasGlasses || civillian[0].transform.parent.gameObject.GetComponent<CivillianCreation>().hasSuit)
                {
                    clothesMultiplier = 1.5f;
                }
                else {
                    clothesMultiplier = 1f;
                }
                
                if (canSteal)
                {
                    if (!instantSteaalPowerUpActivated)
                    {
                        stealSlider.SetActive(true);
                        if (PlayerPrefs.GetInt("GamePlayedBefore") == 1)
                        {
                            if (GetComponent<PlayerMovement>().speedPowerUpActive == true)
                            {
                                Time.timeScale = slowedDownTime / GetComponent<PlayerMovement>().speedPowerUpMultiplier;
                            }
                            else
                            {
                                Time.timeScale = slowedDownTime;
                            }
                        }
                        else {
                            if (firstStealCompleted)
                            {
                                if (GetComponent<PlayerMovement>().speedPowerUpActive == true)
                                {
                                    Time.timeScale = slowedDownTime/GetComponent<PlayerMovement>().speedPowerUpMultiplier;
                                }
                                else { 
                                    Time.timeScale = slowedDownTime;

                                }

                            }
                            else {
                                if (sfxPlayed == false)
                                {
                                    soundEffectsPlayer.playTutorialSFX();
                                    sfxPlayed = true;
                                }
                                Time.timeScale = 0.01f;
                                
                            }
                        }
                        playerWalkingAudioSource.pitch = slowedDownTime;
                        soundEffectsPlayer.VariablePitchSFXAudioSource.pitch = slowedDownTime;
                        if (inStealingZone == true)
                        {
                            stealSliderComponent.GetComponent<SliderController>().canSliderMove = true;
                        }
                    }
                    else
                    {
                        StealMoney();
                        Destroy(civillian[0]);
                    }
                }

            }
            else
            {
                inStealingZone = false;

                if (!inStealingZone)
                {
                    ChooseRandomGreenArea();
                }
                stealSlider.SetActive(false);
                playerWalkingAudioSource.pitch = 1;
                soundEffectsPlayer.VariablePitchSFXAudioSource.pitch = 1;
                Time.timeScale = 1;
            }
        }
        else {
            if (gameManagerScript.gamePaused == false && gameManagerScript.gameOver == false)
            {
                Time.timeScale = 1;
                playerWalkingAudioSource.pitch = 1;
                soundEffectsPlayer.VariablePitchSFXAudioSource.pitch = 1;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(this.transform.position, stealColliderRadius);
        Gizmos.DrawRay(new Vector3(this.transform.position.x,this.transform.position.y+1,this.transform.position.z), this.transform.forward * rayLength);   
    }

    void ChooseRandomGreenArea()
    {
        /*
        float yPos = ;
        greenArea.anchoredPosition = new Vector2(greenArea.anchoredPosition.x, yPos);
        */

        //Choose random green size
        float maximumPossibleGreenAreaSize = 160;
        float greenAreaSize = maxGreenAreaSize - greenAreaReduction;

        float maxGreenAreaHeight = (maximumPossibleGreenAreaSize - greenAreaSize) / 2;
        float minGreenAreaHeight = -maxGreenAreaHeight;
        float yPos = UnityEngine.Random.Range(-maxGreenAreaHeight, maxGreenAreaHeight);
        greenArea.anchoredPosition = new Vector2(greenArea.anchoredPosition.x, yPos);
        greenArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, greenAreaSize);

        float rangeDeduction = ((maximumPossibleGreenAreaSize-greenAreaSize) / maximumPossibleGreenAreaSize)/2;
        topHandlePos = (1f - rangeDeduction) + (yPos/maxGreenAreaHeight) * rangeDeduction;
        bottomHandlePos = (rangeDeduction) + (yPos / maxGreenAreaHeight) * rangeDeduction;
    }
    public void Steal()
    {
        if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
        {
            GetComponent<PlayerMovement>().gameManager.GetComponent<Tutorial>().TutorialPart4();
            if (!firstStealCompleted)
            {
                gameManager.StartCoroutine("GenerateFirstGoal");
            }
            firstStealCompleted = true;
            
        }

        GetComponent<PlayerMovement>().StartCoroutine("SwipeTimeout");
        sliderController.canSliderMove = false;

        inStealingZone = false;
        if ((sliderController.pickPocketSlider.value > bottomHandlePos) && (sliderController.pickPocketSlider.value < topHandlePos))
        {
            StealMoney();
            Collider[] civillian = Physics.OverlapSphere(this.transform.position, stealColliderRadius, civillianColliderMask);
            Destroy(civillian[0]);

        }
        else
        {
            if (!invisibleCloakPowerUpActivated)
            {
                gameManagerScript.Lose();
            }
            else {
                Collider[] civillian = Physics.OverlapSphere(this.transform.position, stealColliderRadius, civillianColliderMask);
                Destroy(civillian[0]);
            }
        }

    }

    public void BumpCivillian()
    {
        Collider[] civillian = Physics.OverlapSphere(this.transform.position, stealColliderRadius, civillianColliderMask);
        Destroy(civillian[0]);
        if (!invisibleCloakPowerUpActivated)
        {
            LoseMoney();
        }
    }

    IEnumerator newHighscoreAlert()
    {
        PlayerPrefs.SetInt("Highscore", Money);
        newHighScoreTextInGame.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        newHighScoreTextInGame.SetActive(false);
        newHighscoreComplete = true;
    }
    public void StealMoney()
    {
        if (Money > PlayerPrefs.GetInt("Highscore"))
        {
            if (PlayerPrefs.GetInt("GamePlayedBefore") == 1 && !newHighscoreComplete)
            {
                StartCoroutine(newHighscoreAlert());
            }
        }
        int newMoney = UnityEngine.Random.Range(minToSteal+moneyIncrement, maxToSteal+moneyIncrement);
        increaseMoneyAlertAnimator.SetTrigger("IncreaseMoney");
        increaseMoneyAlertText.text = "+" + newMoney.ToString();
        increaseMoneyAlertText.color = Color.green;
        if (stealGoalScript.goalActivated == true)
        {
            stealGoalScript.UpdateGoal((int)Math.Round(((float)newMoney * clothesMultiplier)));
            soundEffectsPlayer.playStealMoneySFX();
            if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
            {
                stealAttempts++;
                if (stealAttempts == 2)
                {
                    GetComponent<PlayerMovement>().gameManager.GetComponent<Tutorial>().HideTutorialParts();
                }
            }
        }
        else {
            Money += (int)Math.Round((float)newMoney * multiplier);
            increaseMoneyAlertText.text = "+" + (int)Math.Round((float)newMoney * multiplier * clothesMultiplier);
            moneyText.text = "$" + Money.ToString();
            increaseMoneyAlertText.color = Color.green;
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + (int)Math.Round((float)newMoney * multiplier * clothesMultiplier));
            if (multiplier > 1)
            {
                soundEffectsPlayer.playStealMoneySFX();
            }
            else
            {
                soundEffectsPlayer.playStealMoneySFX();
            }
        }

        if (minGreenAreaSize < (maxGreenAreaSize - greenAreaReduction))
        {
            ReduceGreenAreaSize();
        }
    }

    void ReduceGreenAreaSize()
    {
        greenAreaReduction += UnityEngine.Random.Range(greenAreaMinReductionRate, greenAreaMaxReductionRate);
    }

    public void DoubleMoneyReward()
    {
        Money *= 2;
        adMoneyText.text = "$" + Money.ToString();
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + (int)Math.Round((float)Money/2));
        soundEffectsPlayer.playMultiplierStealMoneySFX();
        gameManager.BackAdButton();
    }
    public void LoseMoney()
    {
        if (stealGoalScript.goalActivated == true)
        {
            float newMoney = UnityEngine.Random.Range((minToLose / 100) * stealGoalScript.goalProgress, (maxToLose / 100) * stealGoalScript.goalProgress);
            stealGoalScript.UpdateGoal(-(int)Math.Round(newMoney));

            increaseMoneyAlertText.text = "" + -(int)Math.Round(newMoney);
            increaseMoneyAlertText.color = Color.red;
            increaseMoneyAlertAnimator.SetTrigger("IncreaseMoney");

        }
        else {
            float newMoney = UnityEngine.Random.Range((minToLose / 100) * Money, (maxToLose / 100) * Money);
            Money -= (int)Math.Round(newMoney);
            moneyText.text = "$" + Money.ToString();
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - (int)Math.Round((float)newMoney * multiplier));
            increaseMoneyAlertText.text = "" + -(int)Math.Round(newMoney);
            increaseMoneyAlertText.color = Color.red;
            increaseMoneyAlertAnimator.SetTrigger("IncreaseMoney");
        }
    }

    public void callInstantStealPowerUp(float powerUpTime)
    {
        float t = powerUpTime;
        StartCoroutine(instantStealPowerUp(t));
    }


    public IEnumerator instantStealPowerUp(float powerUpTime)
    {
        instantSteaalPowerUpActivated = true;
        yield return new WaitForSeconds(powerUpTime);
        instantSteaalPowerUpActivated = false;
    }

    public void callInvisibleCloakPowerUpPowerUp(float powerUpTime)
    {
        float t = powerUpTime;
        StartCoroutine(invisibleCloakPowerUp(t));
    }


    public IEnumerator invisibleCloakPowerUp(float powerUpTime)
    {
        invisibleCloakPowerUpActivated = true;
        yield return new WaitForSeconds(powerUpTime);
        invisibleCloakPowerUpActivated = false;
    }
}
