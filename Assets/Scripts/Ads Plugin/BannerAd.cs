using System;
using UnityEngine;
using GoogleMobileAds.Api;
public class BannerAd : MonoBehaviour
{
    [SerializeField] string iosAdUnit;
    [SerializeField] string androidAdUnit;
    public bool testingAds;
    private string _adUnitId;

    public BannerView _bannerView;

    public void Start()
    {
        if (!testingAds)
        {
#if UNITY_ANDROID
            _adUnitId = androidAdUnit;
#elif UNITY_IOS
        _adUnitId = iosAdUnit;
#else
        _adUnitId = "unused";
#endif
        }
        else
        {
#if UNITY_ANDROID
            _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IOS
        _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        _adUnitId = "unused";
#endif
        }

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

    }

    public void CreateBannerView()
    {
         Debug.Log("Creating banner view");
         // Create a 320x50 banner at top of the screen
         _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);

        LoadAd();
    }

    public void LoadAd()
    {
        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }
}
