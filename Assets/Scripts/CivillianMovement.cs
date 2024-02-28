using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class CivillianMovement : MonoBehaviour
{
    private CharacterController civillianController;
    public float movementSpeed;

    public GameObject player;
    [SerializeField] private float playerOffset;

    public civillianSpawner civillianSpawner;

    void Start()
    {
        civillianController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        civillianController.Move(Vector3.back * movementSpeed * Time.deltaTime);

        if (transform.position.z < player.transform.position.z + playerOffset)
        {
            civillianSpawner.civillianCount--;
            Destroy(this.gameObject);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Player")
        {
            player.GetComponent<StealScript>().LoseMoney();
            Destroy(this.gameObject);
        }
    }

}
