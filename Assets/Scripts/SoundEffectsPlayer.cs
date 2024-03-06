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
        ConstantPitchSFXAudioSource.PlayOneShot(stealMoneySFX[Random.Range(0, stealMoneySFX.Length)], stealMoneySFXVolume);
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
        ConstantPitchSFXAudioSource.PlayOneShot(powerUpActivatedSFX);
    }

    public void playPowerUpCollectedSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(powerUpCollectedSFX);
    }

    public void playGoalMissedSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(GoalMissedSFX);
    }

    public void playMultiplierActivatedSFX()
    {
        ConstantPitchSFXAudioSource.PlayOneShot(MultiplierActivatedSFX);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
