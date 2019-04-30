using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private LevelManager theLevelManager; //instantiate the level manager 
    public int damageToGive;

    // Start is called before the first frame update
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>(); //instantiate the level manager
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //theLevelManager.Respawn();
            theLevelManager.hurtPlayer(damageToGive); //instead of killing the player, just hurt them; this function is in level manager
        }
    }
}
