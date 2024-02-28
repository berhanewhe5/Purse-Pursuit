using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioClip[] stealMoneySFX;
    public AudioClip multiplierStealMoneySFX;
    public AudioClip[] carDrivingSFX;
    public AudioClip pressButtonSFX;
    public AudioClip GoalMissedSFX;
    public AudioClip MultiplierActivatedSFX;
    public AudioClip playerWalkSFX;
    public AudioClip policeSirenSFX;
    public AudioClip powerUpActivatedSFX;
    public AudioClip powerUpCollectedSFX;
    public AudioClip bumpedManSFX;
    public AudioClip bumpedWomanSFX;
    public AudioClip arrestSFX;
    [Range(0,1)]
    public float stealMoneySFXVolume;
    [Range(0,1)]
    public float carDrivingSFXVolume;

    public void playStealMoneySFX()
    {
        sfxAudioSource.PlayOneShot(stealMoneySFX[Random.Range(0, stealMoneySFX.Length)], stealMoneySFXVolume);
    }

    public void playMultiplierStealMoneySFX()
    {
        sfxAudioSource.PlayOneShot(multiplierStealMoneySFX, stealMoneySFXVolume);
    }

    public void playCarDrivingSFX()
    {
        sfxAudioSource.PlayOneShot(carDrivingSFX[Random.Range(0, carDrivingSFX.Length)], carDrivingSFXVolume);
    }

    public void playPressButtonSFX()
    {
        sfxAudioSource.PlayOneShot(pressButtonSFX);
    }

    public void playPowerUpActivatedSFX()
    {
        sfxAudioSource.PlayOneShot(powerUpActivatedSFX);
    }

    public void playPowerUpCollectedSFX()
    {
        sfxAudioSource.PlayOneShot(powerUpCollectedSFX);
    }

    public void playGoalMissedSFX()
    {
        sfxAudioSource.PlayOneShot(GoalMissedSFX);
    }

    public void playMultiplierActivatedSFX()
    {
        sfxAudioSource.PlayOneShot(MultiplierActivatedSFX);
    }

    public void playPlayerWalkSFX()
    {
        sfxAudioSource.PlayOneShot(playerWalkSFX);
    }

    public void playPoliceSirenSFX()
    {
        sfxAudioSource.PlayOneShot(policeSirenSFX);
    }

    public void playBumpedManSFX()
    {
        sfxAudioSource.PlayOneShot(bumpedManSFX);
    }

    public void playBumpedWomanSFX()
    {
        sfxAudioSource.PlayOneShot(bumpedWomanSFX);
    }

    public void playArrestSFX()
    {
        sfxAudioSource.PlayOneShot(arrestSFX);
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
