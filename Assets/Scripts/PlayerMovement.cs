using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
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
    public float[] carRowPosition;

    private bool horizontalMovedApplied;

    public StealScript stealScript;

    [SerializeField] Animator playerAnimator;
    public bool notArrested = true;
    public GameObject handCuffs;

    public bool swipeTimeout = false;
    public float timeoutTime;
    public bool setPoliceCarPosition = false;

    public GameObject StopPoliceCarTrigger;
    public CarSpawner carSpawner;

    public GameManagerScript gameManager;
    public SoundEffectsPlayer soundEffectsPlayer;

    public bool speedPowerUpActive = false; 
    public float speedPowerUpMultiplier;
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
                            soundEffectsPlayer.playSwipeSFX();
                            playerVelocity += Vector3.left * horizontalSpeed;
                            horizontalMovedApplied = false;
                            playerRow -= 1;
                            if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
                            {
                                gameManager.GetComponent<Tutorial>().TutorialPart3();
                            }
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (playerRow != 5)
                        {
                            soundEffectsPlayer.playSwipeSFX();
                            playerVelocity += Vector3.right * horizontalSpeed;
                            horizontalMovedApplied = false;
                            playerRow += 1;
                            if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
                            {
                                gameManager.GetComponent<Tutorial>().TutorialPart2();
                            }
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
                                    soundEffectsPlayer.playSwipeSFX();
                                    playerVelocity += Vector3.right * horizontalSpeed;
                                    horizontalMovedApplied = false;
                                    playerRow += 1;
                                    if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
                                    {
                                        gameManager.GetComponent<Tutorial>().TutorialPart2();
                                    }
                                }
                                else if (startTouchPosition.x > endTouchPosition.x)
                                {
                                    if (playerRow != 1)
                                    {
                                        soundEffectsPlayer.playSwipeSFX();
                                        playerVelocity += Vector3.left * horizontalSpeed;
                                        horizontalMovedApplied = false;
                                        playerRow -= 1;
                                        if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
                                        {
                                            gameManager.GetComponent<Tutorial>().TutorialPart3();
                                        }
                                    }
                                }
                            }
                        }
                    }
#endif

#if UNITY_ANDROID
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
                                    soundEffectsPlayer.playSwipeSFX();
                                    playerVelocity += Vector3.right * horizontalSpeed;
                                    horizontalMovedApplied = false;
                                    playerRow += 1;
                                    if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
                                    {
                                        gameManager.GetComponent<Tutorial>().TutorialPart2();
                                    }
                                }
                            }
                            else if (startTouchPosition.x > endTouchPosition.x)
                            {
                                if (playerRow != 1)
                                {
                                    soundEffectsPlayer.playSwipeSFX();
                                    playerVelocity += Vector3.left * horizontalSpeed;
                                    horizontalMovedApplied = false;
                                    playerRow -= 1;
                                    if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
                                    {
                                        gameManager.GetComponent<Tutorial>().TutorialPart3();
                                    }
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
            else
            {
                playerController.Move(playerVelocity);
                playerVelocity.x = 0;
                horizontalMovedApplied = true;
            }
        }
        else
        {
            if (notArrested)
            {
                playerAnimator.SetTrigger("PlayerCaught");
                stealScript.gameActive = false;
                if (!setPoliceCarPosition)
                {
                    StopPoliceCarTrigger.transform.position = new Vector3(StopPoliceCarTrigger.transform.position.x, StopPoliceCarTrigger.transform.position.y, transform.position.z + carRowPosition[playerRow - 1]);
                    carSpawner.isPlayerBusted = true;
                    setPoliceCarPosition = true;
                }
            }
            else
            {
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
        speedPowerUpMultiplier = speedMultiplier;
    }

    public IEnumerator speedPowerUp(float speedMultiplier, float powerUpTime)
    {
        speedPowerUpActive = true;
        fowardSpeed *= speedMultiplier;
        yield return new WaitForSeconds(powerUpTime);
        fowardSpeed /= speedMultiplier;
        speedPowerUpActive = false;
    }

}
