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
    bool dying = false, hit;
    // Use this for initialization 
    void Start()
    {
        init = this.transform.position;
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if(other.tag == "Weapon")
        {
            //respawn them but take away points
            GameObject pop = Instantiate(scorePop, transform.position, Quaternion.identity);
            pop.GetComponent<ScorePopUp>().setVal(hitCost);
            if (this.gameObject.name == "Player1" && !hit)
            {
                ScoreManager.score1 += hitCost;
                StartCoroutine(cooldown());
            }
            else if (this.gameObject.name == "Player2" && !hit)
            {
                ScoreManager.score2 += hitCost;
                StartCoroutine(cooldown());
            }
        }
    }
    IEnumerator cooldown()
    {
        hit = true;
        yield return new WaitForSeconds(0.1f);
        hit = false;
    }
}
