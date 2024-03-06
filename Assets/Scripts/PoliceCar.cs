using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public bool canMove = true;
    Rigidbody rb;
    public Vector3 startPosition;   
    public float zOffset;
    public GameObject player;


    void Start()
    {
        transform.position = new Vector3 (startPosition.x,startPosition.y,player.transform.position.z + zOffset);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StopPoliceCarTrigger")
        {
            canMove = false;
        }
    }
}
