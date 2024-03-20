using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset; 
    void Start()
    {
        
    }
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
