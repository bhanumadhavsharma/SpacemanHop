using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; //movement speed (+ fo right, - for left)
    public float jumpSpeed; //jumping speed
    public Transform groundCheck; //the object in player that will be used to check for ground
    public float groundCheckRadius; //how far it should check for ground
    public LayerMask whatIsGround; //sets an object to be in the ground layer (eventually could be set to both ground and enemies)
    public bool isGrounded; //is there ground? returns boolean
    public Vector3 respawnPoint; //set an x,y,z position to respawn at
    
    //creates objects of items
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    public GameObject stompBox;

    public LevelManager theLevelManager;

    public float knockbackForce;
    public float knockbackLength;
    private float knockbackCounter;
    public float invincibilityLength;
    private float invincibilityCounter;

    // Start is called before the first frame update
    void Start()
    {
        //instantiates objects
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        theLevelManager = FindObjectOfType<LevelManager>();

        //set spawn point
        respawnPoint = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //check if there is ground (returns boolean)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (knockbackCounter <= 0)
        {
            //if the input for horizontal is the right one
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                //set the speed of movement and the direction character is facing
                myRigidBody.velocity = new Vector3(moveSpeed, myRigidBody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            //if the input for horizontal is the left one
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                //set the speed of movement and the direction character is facing
                myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            //if there is no input and character is idle
            else
            {
                //take away speed
                myRigidBody.velocity = new Vector3(0f, myRigidBody.velocity.y, 0f);
            }

            //if the input for jump is pressed
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                //set the jump speed and leave x velocity from previous inputs
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpSpeed, 0f);
            }
            theLevelManager.invincible = false;
        }

        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;
            if (transform.localScale.x > 0)
            {
                myRigidBody.velocity = new Vector3(-knockbackForce, knockbackForce, 0);
            }
            else
            {
                myRigidBody.velocity = new Vector3(knockbackForce, knockbackForce, 0);
            }
        }

        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }

        if(invincibilityCounter <= 0)
        {
            theLevelManager.invincible = false;
        }

        //for the animators, set the speed and whether or not character is on groud
        myAnimator.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
        myAnimator.SetBool("Grounded", isGrounded);

        //so that the stomp box game object only shows when we start falling down (so we dont kill someone by jumping into them)
        if(myRigidBody.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;
        invincibilityCounter = invincibilityLength;
        theLevelManager.invincible = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //for killing player
        if(other.tag == "KillPlane")
        {
            //respawn at the respawn point 
            theLevelManager.Respawn();
        }

        //for hitting checkpoint
        if(other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //for landing on moving platform
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //for jumping off moving platform
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }
}
