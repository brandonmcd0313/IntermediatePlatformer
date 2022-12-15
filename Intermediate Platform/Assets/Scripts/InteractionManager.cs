using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    //manages how the player pickups items and has a score at all
    //also works to keep the player playing!
    Rigidbody2D rb2d; Vector3 init;[SerializeField] GameObject scorePop;
    [SerializeField] int deathCost;
    [SerializeField] int hitCost;
    bool dying = false, hit, spiked;
    public bool bonkOverlap;

    Vector3 pos1, pos2;
    AudioSource aud;
    public AudioClip fallSound, hitSound;

    // Use this for initialization 
    void Start()
    {
        aud = gameObject.GetComponent<AudioSource>();
        init = this.transform.position;
        rb2d = this.GetComponent<Rigidbody2D>();
        if (this.gameObject.name == "Player1")
        {
            pos1 = transform.position;
        }
        if (this.gameObject.name == "Player2")
        {
            pos2 = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "Player1")
        {
            pos1 = transform.position;
        }
        if (this.gameObject.name == "Player2")
        {
            pos2 = transform.position;
        }
        //keep this player on the screen!
        {

            //player bounds are -28 to 28 on x axis
            //-18 to 18 on y axis

            //while player is above the bounds of the screen
            if (this.transform.position.y > 18)
            {
                //increase/decrease their gravity scale exponentially until they are below top of screen
                rb2d.gravityScale = (2 * this.transform.position.y - 18);
            }
            else
            {
                rb2d.gravityScale = 3;
            }


            //if player goes below the screen
            if (this.transform.position.y < -18 && !dying)
            {
                dying = true;
                aud.PlayOneShot(fallSound);
                Invoke("respawn", 1f);
                
            }



        }


    }

    void respawn()
    {
        this.transform.localPosition = init;
        
        //respawn them but take away points
        GameObject pop = Instantiate(scorePop, transform.position, Quaternion.identity);
        pop.GetComponent<ScorePopUp>().setVal(deathCost);
        if (this.gameObject.name == "Player1")
        {
            ScoreManager.score1 += deathCost;
        }
        else if (this.gameObject.name == "Player2")
        {
            ScoreManager.score2 += deathCost;
        }
        dying = false;
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon" && !other.transform.GetComponent<objectThrow>().grabbed && !other.transform.GetComponent<objectThrow>().idle)
        {

            GameObject pop = Instantiate(scorePop, transform.position, Quaternion.identity);
            pop.GetComponent<ScorePopUp>().setVal(hitCost);

            if (this.gameObject.name == "Player1" && !hit)
            {

                ScoreManager.score1 += hitCost;
                ScoreManager.score2 -= hitCost * 2;
                StartCoroutine(cooldown());
            }
            else if (this.gameObject.name == "Player2" && !hit)
            {

                ScoreManager.score2 += hitCost;
                ScoreManager.score1 -= hitCost * 2;
                StartCoroutine(cooldown());
            }

            aud.PlayOneShot(hitSound);
        }

        if (other.tag == "spike" && !spiked)
        {
            GameObject pop = Instantiate(scorePop, transform.position, Quaternion.identity);
            pop.GetComponent<ScorePopUp>().setVal(hitCost);

            if (this.gameObject.name == "Player1" && !hit)
            {

                ScoreManager.score1 += hitCost;
                StartCoroutine(spikeCooldown());
            }
            if (this.gameObject.name == "Player2" && !hit)
            {

                ScoreManager.score2 += hitCost;
                StartCoroutine(spikeCooldown());
            }
            aud.PlayOneShot(hitSound);
        }
    }
    IEnumerator cooldown()
    {
        hit = true;
        yield return new WaitForSeconds(0.1f);
        hit = false;
    }
    IEnumerator spikeCooldown()
    {
        spiked = true;
        yield return new WaitForSeconds(0.25f);
        spiked = false;
    }
}
