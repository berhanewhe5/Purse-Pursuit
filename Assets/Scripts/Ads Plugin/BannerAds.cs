using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;

    private string adUnitId;

    private void Awake()
    {
        #if UNITY_IOS
                    adUnitId = iosGameId;
        #elif UNITY_ANDROID
                adUnitId = androidGameId;
        #elif UNITY_EDITOR
                    adUnitId = androidGameId; //Platform must be windows
        #endif

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void LoadBannerAd()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerError
        };
        Advertisement.Banner.Load(adUnitId, options);
    }

    public void ShowBannerAd()
    {
        BannerOptions options= new BannerOptions
        {
            clickCallback = BannerClicked,
            hideCallback = BannerHidden,
            showCallback = BannerShown
        };
        Advertisement.Banner.Show(adUnitId, options);   
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
    #region ShowCallBacks
    private void BannerShown()
    {
    }

    private void BannerHidden()
    {
    }

    private void BannerClicked()
    {
    }
    #endregion

    #region LoadCallBacks
    private void BannerError(string message)
    {
        Debug.Log("Banner Ad Error: " + message);
    }

    private void BannerLoaded()
    {
        Debug.Log("Banner Ad Loaded");
    }
    #endregion
}
