using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
    public GameObject deathEffect;
    private Rigidbody2D playerRigidbody;
    public float bounceForce;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bounceForce, 0f);
        }
    }
}
