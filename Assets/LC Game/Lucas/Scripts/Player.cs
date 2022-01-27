using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    // Define Player Variables

#region Variables
    private float playerSpeed, playerJumpHeight;
    //private int jumps = 0;
    //private int maxJumps = 2;
    private Rigidbody2D rb;
    //private bool isGrounded;
    public GameObject deathText;
    public GameObject pauseMenu;
    public bool isPaused;
    
    // Checkpoints
    public GameObject spawnPoint;
    public GameObject checkpoint;
    public bool isDisabled;
    
    //bullet
    public GameObject bulletPrefab;
    public Transform firingPoint;
    public float bulletSpeed = 10;
    
#endregion

#region Start and Update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
    }
#endregion

    /// <summary>
    /// All player movement controlled here
    /// </summary>
    void Move()
    {
        playerSpeed = 5f;
        playerJumpHeight = 200f;
        // Check if grounded
        // if(isGrounded)
        // {
        //     jumps = 0;
        // }
        // player movement controls
        if(Input.GetKey(KeyCode.A))
        {
            if(isPaused)
                return;
            
            rb.AddForce(Vector2.left*playerSpeed);
            //move left
        }
        if(Input.GetKey(KeyCode.D))
        {
            if(isPaused)
                return;
            rb.AddForce(Vector2.right*playerSpeed);
            //move right
        }
        // Jump mechanic
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isPaused)
                return;
            //if(jumps <= maxJumps)
            
                rb.AddForce(Vector2.up * playerJumpHeight);
                // track number of jumps
                //jumps++;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(isPaused)
                return;
            FireBullet();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                Pause();
                isPaused = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                isPaused = false;
                Time.timeScale = 1;
            }
        }
    }

#region Player Mechanics

    /// <summary>
    /// Pauses Game, Sets timescale to 0
    /// </summary>
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Unpauses Game, Sets timescale to 1
    /// </summary>
    public void UnPause()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Controls what happens when player shoots
    /// </summary>
    public Bullet FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = firingPoint.position;
        Vector2 playerForce = rb.velocity;
        Vector2 bulletForce = Vector2.right * bulletSpeed;
        bullet.GetComponent<Rigidbody2D>().velocity = playerForce + bulletForce;
        Destroy(bullet,5f);
        return bullet.GetComponent<Bullet>();
    }

    /// <summary>
    /// when player dies
    /// </summary>
    public void Die()
    {
        StartCoroutine(DeathText());
        //player die
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        
        // TODO Disable character model
        
        transform.position = checkpoint != null
            ? checkpoint.transform.position
            : spawnPoint.transform.position;
        
    }

    /// <summary>
    /// Makes Death Text appear
    /// </summary>
    /// <returns></returns>
    IEnumerator DeathText()
    {
        deathText.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        deathText.gameObject.SetActive(false);
    }
    
#endregion

#region Collisions
    private void OnCollisionEnter2D(Collision2D other)
    {

        if(other.transform.CompareTag("Enemy"))
        {
            Die();
        }
        
	    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            checkpoint = other.gameObject;
        }
    }
#endregion
    
}
