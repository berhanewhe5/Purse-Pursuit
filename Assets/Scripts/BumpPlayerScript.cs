using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpPlayerScript : MonoBehaviour
{
    public SoundEffectsPlayer soundEffectsPlayer;
    public bool isMan;
    public bool isWoman;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if (isMan)
            {
                Debug.Log("Bumped");
                soundEffectsPlayer.playBumpedManSFX();
            }
            else if (isWoman)
            {
                Debug.Log("Bumped");
                soundEffectsPlayer.playBumpedWomanSFX();
            }
            else {
                Debug.Log("Bumped outside");
            }

            GetComponentInParent<CivillianMovement>().civillianSpawner.civillianCount--;
            GetComponentInParent<CivillianMovement>().player.GetComponent<StealScript>().BumpCivillian();
        }
    }
}
