using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public BannerAds bannerAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;

    private void Awake()
    {
        interstitialAds.LoadInterstialAd();
        bannerAds.LoadBannerAd();
        rewardedAds.LoadRewardedAd();
    }
}
