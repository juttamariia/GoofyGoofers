using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerCamera : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float camMaxHeight;
    [SerializeField] private float camMinHeight;
    [SerializeField] private float camMaxRight;
    [SerializeField] private float camMaxLeft;

    [SerializeField] private float currentX;
    [SerializeField] private float currentY;

    [SerializeField] private Transform player;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        GetCurrentX();
        GetCurrentY();

        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentX, currentY, transform.position.z), ref velocity, speed * Time.deltaTime); ; 
    }

    private float GetCurrentX()
    {
        if (player.transform.position.x < camMaxLeft)
        {
            currentX = camMaxLeft;
        }

        else if (player.transform.position.x > camMaxRight)
        {
            currentX = camMaxRight;
        }

        else
        {
            currentX = player.transform.position.x;
        }

        return currentX;
    }

    private float GetCurrentY()
    {
        if(player.transform.position.y > camMaxHeight)
        {
            currentY = camMaxHeight;
        }

        else if(player.transform.position.y < camMinHeight)
        {
            currentY = camMinHeight;
        }

        else
        {
            currentY = player.transform.position.y;
        }

        return currentY;
    }
}
