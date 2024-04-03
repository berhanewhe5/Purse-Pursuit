using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public AudioSource ConstantPitchSFXAudioSource;
    public AudioSource VariablePitchSFXAudioSource;
    public AudioSource policeSirenSource;
    public AudioClip[] stealMoneySFX;
    public AudioClip multiplierStealMoneySFX;
    public AudioClip[] carDrivingSFX;
    public AudioClip pressButtonSFX;
    public AudioClip purchaseItemSFX;
    public AudioClip BillPopsUpSFX;
    public AudioClip CompleteTutorialSFX;
    public AudioClip MissedAStealSFX;
    public AudioClip IncreaseUpgradeSFX;
    public AudioClip GoalMissedSFX;
    public AudioClip SwipeSFX;
    public AudioClip TutorialSFX;
    public AudioClip MultiplierActivatedSFX;
    public AudioClip playerWalkSFX;
    public AudioClip policeSirenSFX;
    public AudioClip powerUpActivatedSFX;
    public AudioClip powerUpCollectedSFX;
    public AudioClip bumpedManSFX;
    public AudioClip bumpedWomanSFX;
    public AudioClip arrestSFX;
    public AudioClip lowFunds;
    public AudioClip newHighscoreSFX;
    public AudioClip adRewardSFX;

    [Range(0,1)]
    public float stealMoneySFXVolume;
    [Range(0,1)]
    public float carDrivingSFXVolume;
    [Range(0, 1)]
    public float swipeSFXVolume;
    [Range(0, 1)]
    public float multiplierSFXVolume;
    [Range(0, 1)]
    public float collectPowerUpSFXVolume;
    public void playStealMoneySFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(stealMoneySFX[Random.Range(0, stealMoneySFX.Length)], stealMoneySFXVolume);
    }
    public void playNewHighscoreSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(newHighscoreSFX);
    }
    public void playAdRewardSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(adRewardSFX);
    }
    public void playLowFundsSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(lowFunds);
    }
    public void playMultiplierStealMoneySFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(multiplierStealMoneySFX, stealMoneySFXVolume);
    }

    public void playCarDrivingSFX()
    {
        VariablePitchSFXAudioSource.PlayOneShot(carDrivingSFX[Random.Range(0, carDrivingSFX.Length)], carDrivingSFXVolume);
    }

    public void playPressButtonSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(pressButtonSFX);
    }

    public void playPowerUpActivatedSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(powerUpActivatedSFX, collectPowerUpSFXVolume);
    }

    public void playPowerUpCollectedSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(powerUpCollectedSFX, collectPowerUpSFXVolume);
    }

    public void playGoalMissedSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(GoalMissedSFX);
    }

    public void playMultiplierActivatedSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(MultiplierActivatedSFX,multiplierSFXVolume);
    }

    public void playPlayerWalkSFX()
    {
        VariablePitchSFXAudioSource.PlayOneShot(playerWalkSFX);
    }

    public void playPoliceSirenSFX()
    {
        policeSirenSource.Play();
    }

    public void playBumpedManSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(bumpedManSFX);
    }

    public void playBumpedWomanSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(bumpedWomanSFX);
    }

    public void playArrestSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(arrestSFX);
    }

    public void playBillPopsUpSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(BillPopsUpSFX);
    }

    public void playPurchaseItemSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(purchaseItemSFX);
    }

    public void playCompleteTutorialSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(CompleteTutorialSFX);
    }

    public void playSwipeSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(SwipeSFX, swipeSFXVolume);
    }

    public void playTutorialSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(TutorialSFX);
    }

    public void playMissedAStealSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(MissedAStealSFX);
    }

    public void playIncreaseUpgradeSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(IncreaseUpgradeSFX);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
