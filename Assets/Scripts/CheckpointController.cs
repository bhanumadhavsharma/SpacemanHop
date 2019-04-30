using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    public Sprite flagClosed; //the sprite for flag closed
    public Sprite flagOpen; //the sprite for an open flag
    public bool checkpointActive; //checkpoint is active; used in other scripts

    private SpriteRenderer mySpriteRenderer; //sprite renderer component
    
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>(); //sprite renderer component being instantiated
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //for a checkpoint
        if(other.tag == "Player")
        {
            //set sprite to open flag, and checkpoint active to true
            mySpriteRenderer.sprite = flagOpen;
            checkpointActive = true;
        }
    }
}
