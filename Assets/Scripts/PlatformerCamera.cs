using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Transform player;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), ref velocity, speed * Time.deltaTime); 
    }
}