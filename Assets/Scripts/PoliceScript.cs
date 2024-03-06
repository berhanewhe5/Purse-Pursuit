using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PoliceScript : MonoBehaviour
{
    CharacterController policeController;
    Animator animator;

    public GameObject player;
    public float zOffset;
    [SerializeField] private float policePlayerDistance;
    public float policeSpeed;
    [SerializeField] private GameManagerScript gameManagerScript;
    public SoundEffectsPlayer soundEffectsPlayer;
    public AudioSource playerWalkingAudioSource;
    public GameObject policeSirenVFX;
    public GameObject gamePlayUI;


    void Awake()
    {
        policeController = GetComponent<CharacterController>(); 
        animator = GetComponent<Animator>();
        soundEffectsPlayer.playPoliceSirenSFX();
        playerWalkingAudioSource.volume = 0;
        policeSirenVFX.SetActive(true);
        gamePlayUI.SetActive(false);
    }


    void Update()
    {

        if (this.transform.position.z < player.transform.position.z + zOffset-1)
        {
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + zOffset);
        }
        else if (transform.position.z < player.transform.position.z - policePlayerDistance)
        {
            policeController.Move(Vector3.forward * policeSpeed * Time.deltaTime);
        }
        else {
            if (player.GetComponent<PlayerMovement>().notArrested == true)
            {
                gameManagerScript.LoseScreen();
                player.GetComponent<PlayerMovement>().notArrested = false;
                animator.SetTrigger("ArrestedPlayer");
            }

        }
    }
}
