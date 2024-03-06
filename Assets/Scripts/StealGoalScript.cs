using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class StealGoalScript : MonoBehaviour
{
    [SerializeField] StealScript stealScript;

    public TMP_Text GoalText;
    public TMP_Text TimeLeftText;
    public TMP_Text MultiplerText;
    public TMP_Text GoalDescriptionText;

    public GameObject GoalUI;
    public GameObject MultiplerRewardUI;

    public GameObject goalMissedText;
    public GameObject multiplerActivatedText;

    int goal;
    public int goalProgress;

    int timeLeftMinutes;
    int timeLeftSeconds;

    float multipler;

    public bool goalActivated = false;

    [SerializeField] Scrollbar multiplerRewardTimer;
    [SerializeField] Scrollbar goalProgressBar;
    [SerializeField] GameObject goalProgressHandle;

    public int[] goalTimer;
    public float[] goalMultiplier;

    public int minTime;
    public int maxTime;
    public int timeIntervals;

    public int minMoney;
    public int maxMoney;
    public int moneyBase;

    public float minMultiplier;
    public float maxMultiplier;
    public float multiplierIntervals;

    public float multiplierTime;

    public string[] GoalDescriptionType1;
    public string[] GoalDescriptionType2;
    public string[] GoalDescriptionType3;
    public string[] GoalDescriptionType4;

    public string goalDescriptionText;

    private void Start()
    {
        
    }

    public void GenerateGoal()
    {
        goalProgressHandle.SetActive(false);
        int j = 0;
        for (int i = minTime; i <= maxTime; i += timeIntervals)
        {
            j++;
        }
        goalTimer = new int[j];
        j = 0;
        for (int i = minTime; i <= maxTime; i += timeIntervals)
        {
            goalTimer[j] = i;
            j++;
        }

        j = 0;
        for (float i = minMultiplier; i <= maxMultiplier; i += multiplierIntervals)
        {
            j++;
        }
        goalMultiplier = new float[j];
        j = 0;
        for (float i = minMultiplier; i <= maxMultiplier; i += multiplierIntervals)
        {
            goalMultiplier[j] = (i) / 10;
            j++;
        }

        timeLeftSeconds = 0;
        timeLeftMinutes = 0;
        int time = goalTimer[Random.Range(0,goalTimer.Length-1)];
        GoalDescriptionText.text = GenerateGoalDescription(time);
        int moneyGoal = moneyBase + ((time-minTime+timeIntervals)/timeIntervals) * Random.Range(minMoney,maxMoney);
        EditGoal(moneyGoal);
        
        goalProgress = 0;

        while (time > 59)
        {
            time -= 60;
            timeLeftMinutes += 1;
        }
        timeLeftSeconds = time;

        UpdateTimer();
        multipler = goalMultiplier[Random.Range(0, goalMultiplier.Length - 1)]; ;
        StartCoroutine("Timer");
        goalActivated = true;
        ShowGoalUI();
    }

    string GenerateGoalDescription(int time)
    {
        string goalDescription = "";

        if (time >= 30 && time < 60)
        {
            goalDescription = GoalDescriptionType1[Random.Range(0, GoalDescriptionType1.Length - 1)];
        }
        else if (time >= 60 && time < 100)
        {
            goalDescription = GoalDescriptionType2[Random.Range(0, GoalDescriptionType2.Length - 1)];
        }
        else if (time >= 100 && time < 140)
        {
            goalDescription = GoalDescriptionType3[Random.Range(0, GoalDescriptionType3.Length - 1)];
        }
        else if (time >= 140 && time <= 180)
        {
            goalDescription = GoalDescriptionType4[Random.Range(0, GoalDescriptionType4.Length - 1)];
        }

        return goalDescription;
    }
    void EditGoal(int newGoal)
    {
        goal = newGoal; 
        GoalText.text = $"$ 0/{newGoal.ToString()}";
    }

    IEnumerator goalReached()
    {
        if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
        {
            GetComponent<Tutorial>().TutorialPart6();
        }
        GetComponent<SoundEffectsPlayer>().playMultiplierActivatedSFX();
        HideGoalUI();
        MultiplerRewardUI.SetActive(true);
        MultiplerText.text = multipler.ToString() + "x";
        StartCoroutine(MultiplierTimer(multiplierTime + (3 * PlayerPrefs.GetInt("MultiplierTier"))));
        multiplerActivatedText.SetActive(true);
        yield return new WaitForSeconds(2f);
        multiplerActivatedText.SetActive(false);

        if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
        {
            yield return new WaitForSeconds(4f);
            GetComponent<Tutorial>().TutorialPart7();
            yield return new WaitForSeconds(2f);
            GetComponent<Tutorial>().EndTutorial();
        }
    }
    IEnumerator goalMissed() {
        GetComponent<SoundEffectsPlayer>().playGoalMissedSFX();
        HideGoalUI();
        GetComponent<GameManagerScript>().Lose();
        goalMissedText.SetActive(true);
        yield return new WaitForSeconds(2f);
        goalMissedText.SetActive(false);

    }
    IEnumerator Timer()
    {
        while ((timeLeftSeconds > 0 || timeLeftMinutes>0) && goalProgress<goal) {
            yield return new WaitForSeconds(1f);
            if (timeLeftSeconds != 0)
            {
                timeLeftSeconds -= 1;
            }
            else {
                timeLeftMinutes -= 1;
                timeLeftSeconds = 59;
            }

            UpdateTimer();
        }

        if (goalProgress < goal) {
            StartCoroutine(goalMissed());
        }

    }

    IEnumerator MultiplierTimer(float multiplerTime) {
        goalActivated = false;
        stealScript.multiplier = multipler;
        multiplerRewardTimer.size = 1;
        float time = multiplerTime;
        while (multiplerTime >= 0)
        {
            // fix this -- make it more slower but for now it works.

            yield return new WaitForSecondsRealtime(0.05f);
            multiplerTime -= 0.05f;
            multiplerRewardTimer.size -= 0.05f / time;
        }

        multiplerRewardTimer.size = 0;
        stealScript.multiplier = 1;
        MultiplerRewardUI.SetActive(false);
        GenerateGoal();
        
    }

    void UpdateTimer()
    {
        string secondsString = timeLeftSeconds.ToString();

        if (timeLeftSeconds < 10)
        {
            secondsString = "0" + timeLeftSeconds.ToString();
        }
        string timeLeft = timeLeftMinutes.ToString() + ":" + secondsString;
        TimeLeftText.text =  timeLeft;
    }
    public void UpdateGoal(int amountStolen) {
        goalProgressHandle.SetActive(true);

        goalProgress += amountStolen;
        GoalText.text = GoalText.text.Remove(GoalText.text.LastIndexOf(" ") + 1, GoalText.text.Substring(GoalText.text.LastIndexOf(" ") + 1, GoalText.text.LastIndexOf("/") - GoalText.text.LastIndexOf(" ") - 1).Length);
        GoalText.text = GoalText.text.Insert(GoalText.text.LastIndexOf(" ") + 1, goalProgress.ToString());
        goalProgressBar.size = (float)goalProgress / (float)goal;
        if (goalProgress>=goal)
        {
            StartCoroutine(goalReached());
        }
    }



    public void HideGoalUI()
    {
        GoalUI.SetActive(false);
    }

    public void ShowGoalUI()
    {
        GoalUI.SetActive(true);
    }
    void Update()
    {
        
    }
}
