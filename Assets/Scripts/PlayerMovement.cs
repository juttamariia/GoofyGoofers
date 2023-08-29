using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    [Header("Needed References")]
    [SerializeField] private GroundCheck groundCheck;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }

        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
