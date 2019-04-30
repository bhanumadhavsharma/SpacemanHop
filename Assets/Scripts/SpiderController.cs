using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public float moveSpeed; //how fast does it move?
    private bool canMove; //is it in window of game?

    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            myRigidbody2D.velocity = new Vector3(-moveSpeed, myRigidbody2D.velocity.y, 0f);
        }
    }

    void OnBecameVisible()
    {
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "KillPlane")
        {
            other.gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        canMove = false;
    }
}
