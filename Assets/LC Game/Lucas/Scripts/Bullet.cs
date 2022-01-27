using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.transform.tag);
        if(other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<Player>().Die();
        }

        
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
