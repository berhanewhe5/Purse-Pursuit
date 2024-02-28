using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    [SerializeField] float carLifeTime;
    public SoundEffectsPlayer soundEffectsPlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        soundEffectsPlayer.playCarDrivingSFX();

        Destroy(this.gameObject, carLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.eulerAngles.y == 180f)
        {
            rb.AddForce(Vector3.back * speed);
        }
        else {
            rb.AddForce(Vector3.forward * speed);
        }

    }
}
