using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public float waitToRespawnTime;
    public PlayerController thePlayer;
    public GameObject deathEffect;
    private bool respawning;

    public int coinCount;
    public Text coinText;

    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Sprite heartfull;
    public Sprite hearthalf;
    public Sprite heartempty;
    public int maxHealth;
    public int healthCount;
    public bool invincible;

    private ResetOnRespawn[] objectsToReset;
    //public CameraController theCamera;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        coinText.text = "Coins: " + coinCount;
        healthCount = maxHealth;
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        //so that we arent continiously respawning
        if(healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }
    }

    //a function that handles player respawn
    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    public IEnumerator RespawnCoroutine()
    {
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        yield return new WaitForSeconds(waitToRespawnTime);
        thePlayer.transform.position = thePlayer.respawnPoint;
        healthCount = maxHealth;
        respawning = false;
        updateHeartMeter();
        thePlayer.gameObject.SetActive(true);
        coinCount = 0;
        coinText.text = "Coins: " + coinCount;
        //theCamera.transform.position = thePlayer.transform.position;
        for (int k = 0; k < objectsToReset.Length; k++)
        {
            objectsToReset[k].gameObject.SetActive(true);
            objectsToReset[k].ResetObject();
        }
    }

    public void addCoins(int coinsToAdd)
    {
        //add coins to count
        coinCount += coinsToAdd;
        coinText.text = "Coins: " + coinCount;
    }

    public void hurtPlayer(int damageToTake)
    {
        if (!invincible)
        {
            //hurt the player and do damage and update heart ui; this funciton is used in hurtplayer.cs
            healthCount -= damageToTake;
            updateHeartMeter();
            thePlayer.Knockback();
        }
    }

    public void updateHeartMeter()
    {
        //based on the health count, change the health ui
        switch (healthCount)
        {
            case 6:
                heart1.sprite = heartfull;
                heart2.sprite = heartfull;
                heart3.sprite = heartfull;
                return;
            case 5:
                heart1.sprite = heartfull;
                heart2.sprite = heartfull;
                heart3.sprite = hearthalf;
                return;
            case 4:
                heart1.sprite = heartfull;
                heart2.sprite = heartfull;
                heart3.sprite = heartempty;
                return;
            case 3:
                heart1.sprite = heartfull;
                heart2.sprite = hearthalf;
                heart3.sprite = heartempty;
                return;
            case 2:
                heart1.sprite = heartfull;
                heart2.sprite = heartempty;
                heart3.sprite = heartempty;
                return;
            case 1:
                heart1.sprite = hearthalf;
                heart2.sprite = heartempty;
                heart3.sprite = heartempty;
                return;
            case 0:
                heart1.sprite = heartempty;
                heart2.sprite = heartempty;
                heart3.sprite = heartempty;
                return;
            default:
                heart1.sprite = heartempty;
                heart2.sprite = heartempty;
                heart3.sprite = heartempty;
                return;
        }
    }
}
