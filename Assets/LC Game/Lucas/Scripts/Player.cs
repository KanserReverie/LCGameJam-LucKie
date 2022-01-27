using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    // Define Variables
    private float playerSpeed, playerJumpHeight, playerLives;
    private int jumps = 0;
    private int maxJumps = 2;
    private Rigidbody2D rb;
    private bool isGrounded;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// All player movement controlled here
    /// </summary>
    void Move()
    {
        playerSpeed = 5f;
        playerJumpHeight = 400f;
        // Check if grounded
        // if(isGrounded)
        // {
        //     jumps = 0;
        // }
        // player movement controls
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left*playerSpeed);
            //move left
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right*playerSpeed);
            //move right
        }
        // Jump mechanic
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //if(jumps <= maxJumps)
            
                rb.AddForce(Vector2.up * playerJumpHeight);
                // track number of jumps
                //jumps++;
            

        }
        
    }

    /// <summary>
    /// All player shooting controls go here
    /// </summary>
    void Shoot()
    {
        //shooting mechanic goes here
    }
}
