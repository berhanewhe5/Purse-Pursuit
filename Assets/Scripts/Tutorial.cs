using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject TutorialPanel;
    public GameObject TutorialPanelPart1;
    public GameObject TutorialPanelPart2;
    public GameObject TutorialPanelPart3;
    public GameObject TutorialPanelPart4;
    public GameObject TutorialPanelPart5;
    public GameObject TutorialPanelPart6;

    int tutorialPart;
    void Start()
    {
        
    }
    public void StartTutorial()
    {
        TutorialPanel.SetActive(true);
        tutorialPart = 0;
    }
    public void EndTutorial()
    {
        TutorialPanel.SetActive(false);
        PlayerPrefs.SetInt("GamePlayedBefore", 1);
    }

    public void TutorialPart1()
    {
        if (tutorialPart < 1)
        {
            HideTutorialParts();
            TutorialPanelPart1.SetActive(true);
            tutorialPart = 1;
        }
    }
    public void TutorialPart2()
    {
        if (tutorialPart == 1)
        {
            HideTutorialParts();
            TutorialPanelPart2.SetActive(true);
            tutorialPart = 2;
        }
    }
    public void TutorialPart3()
    {
        if (tutorialPart == 2)
        {
            GetComponent<GameManagerScript>().player.GetComponent<StealScript>().canSteal = true;
            HideTutorialParts();
            TutorialPanelPart3.SetActive(true);
            tutorialPart = 3;
        }
    }
    public void TutorialPart4()
    {
        if (tutorialPart == 3)
        {
            HideTutorialParts();
            tutorialPart = 4;
        }
    }
    public void TutorialPart5()
    {
        if (tutorialPart == 4)
        {
            HideTutorialParts();
            TutorialPanelPart4.SetActive(true);
            tutorialPart = 5;
        }
    }

    public void TutorialPart6()
    {
        if (tutorialPart == 5)
        {
            HideTutorialParts();
            TutorialPanelPart5.SetActive(true);
            tutorialPart = 6;
        }
    }

    public void TutorialPart7()
    {
        if (tutorialPart == 6)
        {
            HideTutorialParts();
            TutorialPanelPart6.SetActive(true);
            tutorialPart = 7;
        }
    }

    public void HideTutorialParts()
    {
        TutorialPanelPart1.SetActive(false);
        TutorialPanelPart2.SetActive(false);
        TutorialPanelPart3.SetActive(false);
        TutorialPanelPart4.SetActive(false);
        TutorialPanelPart5.SetActive(false);
        TutorialPanelPart6.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
