using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    // Define Player Variables
    private float playerSpeed, playerJumpHeight, playerLives;
    private int jumps = 0;
    private int maxJumps = 2;
    private Rigidbody2D rb;
    private bool isGrounded;
    public GameObject deathText;
    
    // Checkpoints
    public GameObject spawnPoint;
    public GameObject checkpoint;
    public GameObject[] checkpoints;
    public bool isDisabled;
    
    //bullet
    public GameObject bulletPrefab;
    public Transform firingPoint;
    public float bulletSpeed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
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
        if(Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
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

    IEnumerator DeathText()
    {
        deathText.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        deathText.gameObject.SetActive(false);
    }
    
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
}
