using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController playerController;
    [SerializeField] float fowardSpeed;
    [SerializeField] float horizontalSpeed;
    private Vector3 playerVelocity;

    public bool canMove = true;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    //There are 3 rows this value can only be 1,2 or 3.
    public int playerRow;

    private bool horizontalMovedApplied;

    public StealScript stealScript;

    [SerializeField] Animator playerAnimator;
    public bool notArrested = true;
    public GameObject handCuffs;

    public bool swipeTimeout = false;
    public float timeoutTime;
    void Start()
    {
        handCuffs.SetActive(false);
        playerController = GetComponent<CharacterController>();
        horizontalMovedApplied = true;
    }

    public IEnumerator SwipeTimeout() { 
        swipeTimeout = true;
        yield return new WaitForSeconds(timeoutTime);
        swipeTimeout = false;
    }
    //rb.velocity = Vector3.right * horizontalSpeed;
    void Update()
    {
        if (canMove)
        {
            //Movement

            //Default Foward movement
            playerVelocity = Vector3.forward * fowardSpeed;

            if (stealScript.gameActive)
            {
                if (!swipeTimeout)
                {
                    //Swiping to go right and left

#if UNITY_EDITOR_WIN

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (playerRow != 1)
                        {
                            playerVelocity += Vector3.left * horizontalSpeed;
                            horizontalMovedApplied = false;
                            playerRow -= 1;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (playerRow != 5)
                        {
                            playerVelocity += Vector3.right * horizontalSpeed;
                            horizontalMovedApplied = false;
                            playerRow += 1;
                        }
                    }
#endif
#if UNITY_IOS
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == UnityEngine.TouchPhase.Began)
                        {
                            startTouchPosition = touch.position;
                        }
                        if (touch.phase == UnityEngine.TouchPhase.Ended)
                        {
                            endTouchPosition = touch.position;
                            if (startTouchPosition.x < endTouchPosition.x)
                            {
                                if (playerRow != 5)
                                {
                                    playerVelocity += Vector3.right * horizontalSpeed;
                                    horizontalMovedApplied = false;
                                    playerRow += 1;
                                }
                            }
                            else if (startTouchPosition.x > endTouchPosition.x)
                            {
                                if (playerRow != 1)
                                {
                                    playerVelocity += Vector3.left * horizontalSpeed;
                                    horizontalMovedApplied = false;
                                    playerRow -= 1;
                                }
                            }
                        }
                    }
#endif
                }
            }
            if (horizontalMovedApplied)
            {
                playerController.Move(playerVelocity * Time.deltaTime);
            }
            else{
                playerController.Move(playerVelocity);
                playerVelocity.x = 0;
                horizontalMovedApplied = true;
            }
        }
        else {
            if (notArrested)
            {
                playerAnimator.SetTrigger("PlayerCaught");
                stealScript.gameActive = false;
            }
            else{
                playerAnimator.SetTrigger("PlayerUnderArrest");
                handCuffs.SetActive(true);
            }
                
        }
    }



    public void callSpeedPowerUp(float speedMultiplier, float powerUpTime)
    {
        float s = speedMultiplier;
        float t = powerUpTime;

        StartCoroutine(speedPowerUp(s, t));
    }
    public IEnumerator speedPowerUp(float speedMultiplier, float powerUpTime)
    {
        fowardSpeed *= speedMultiplier;
        yield return new WaitForSeconds(powerUpTime);
        fowardSpeed /= speedMultiplier;
    }

}
