using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Screenshotter : MonoBehaviour
{
    int count = 0;
    public string ScreenType;


    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
        { 
            ScreenCapture.CaptureScreenshot($"C:\\Users\\wheelerb\\Desktop\\Projects\\My Games\\Screenshots\\screenshot-"+ScreenType+"-Screenshot"+PlayerPrefs.GetInt("screenShotCount").ToString()+".png");
            PlayerPrefs.SetInt("screenShotCount", PlayerPrefs.GetInt("screenShotCount")+1);
        }
        #endif
    }
}
