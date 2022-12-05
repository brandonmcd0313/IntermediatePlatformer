﻿using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {
    //this is player one, simular to player two but not
    //button maps end with "P1" to indicate difference

    //player1 uses arrow keys to move, left ctrl,alt,shift to fire
    //space to jump
    //other bindings are Z, X, C, 3


    //player2 uses RDFG to move, A,S,W to fire
    //Q to jump
    //other bindings are E, [, ], 6

    // Use this for initialization
    private bool canMove, canJump, jumping;
    [SerializeField] float speed, jumpForce, bonkForce, throwForce;
    public bool facingRight = true;
    public GameObject j1, j2; //jump check objects 
    Rigidbody2D rb2d; Color color;
    Transform init; public static int tutWait;
    public string playerCode; //P1 or P2, determines input mapping
    public bool grabbed; //if the player has grabbed the other player or not
    RaycastHit2D hit;
    public Slider smashMeter;
    public GameObject smashMeterObject;
    public bool running, brokeOut;
    private Vector2 ViewportPos;
    public bool canGrab;

    void Start () {
        setGrab(true);
        canMove = true;
        if(PlayerPrefs.GetInt("Tutorial") == 1)
        {
            canMove = false;
            //make player invisible
            color = this.GetComponent<SpriteRenderer>().color;
                color.a = 0;
            this.GetComponent<SpriteRenderer>().color = color;
            StartCoroutine(tutorialWait());
        }
        rb2d = this.GetComponent<Rigidbody2D>();
        init = this.transform;
        smashMeterObject.SetActive(false);
	}
    IEnumerator tutorialWait()
    {
        yield return new WaitUntil(() => (tutWait == 0));
        canMove = true;
        //make player invisible
        color.a = 255;
        this.GetComponent<SpriteRenderer>().color = color;
    }
    
    public bool getGrab()
    {
        return canGrab;
    }
    
    public void setGrab(bool i)
    {
        canGrab = i;
    }
    // Update is called once per frame
    void Update()
    {
        //error, grabbed player is able to grab the player grabbing them.
        if(hit.transform != null)
        {
            if (Input.GetButtonDown("Fire2" + playerCode) && running)
            {
                smashMeter.value += 50;
            }
        }

        //grabing stuff
        {
            if (facingRight)
            {
                //while the player hasn't grabbed anything check if theres a player infront
                if (!grabbed)
                {
                    hit = Physics2D.Raycast(new Vector3(transform.position.x + 2, transform.position.y), Vector2.right, 2f);
                }

                if (Input.GetButton("Fire1" + playerCode) && getGrab())
                {
                    if (((hit.transform != null && hit.transform.tag == "Player") || grabbed) && !brokeOut)
                    {
                        print("grabbed right");
                        hit.transform.gameObject.GetComponent<PlayerController>().setMove(false);
                        hit.transform.gameObject.GetComponent<PlayerController>().setGrab(false);
                        hit.rigidbody.velocity = Vector2.zero;
                        hit.transform.position = new Vector3(transform.position.x + 3, transform.position.y);
                        ViewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
                        smashMeter.GetComponent<RectTransform>().anchoredPosition = new Vector2(1920 * ViewportPos.x, 1080 * ViewportPos.y);
                        speed = 3.5f;
                        
                        grabbed = true;
                        if (!running)
                        {
                            smashMeter.value = 15;
                            StartCoroutine(smashin());
                        }
                    }
                    else
                    {
                        grabbed = false;
                    }
                }
                else if (grabbed && !brokeOut)
                {
                    hit.transform.gameObject.GetComponent<PlayerController>().setGrab(true);
                    grabbed = false;
                    hit.transform.gameObject.GetComponent<PlayerController>().setMove(true);
                    speed = 10f;
                    hit.rigidbody.velocity += new Vector2(throwForce, 0);
                    StopCoroutine(smashin());
                    smashMeterObject.SetActive(false);
                    running = false;
                }
            }
            else
            {
                //while the player hasn't grabbed anything check if theres a player infront
                if (!grabbed)
                {
                    hit = Physics2D.Raycast(new Vector3(transform.position.x - 2, transform.position.y), Vector2.left, 2f);
                }

                if (Input.GetButton("Fire1" + playerCode) && getGrab())
                {
                    if (((hit.transform != null && hit.transform.tag == "Player") || grabbed) && !brokeOut)
                    {
                        print("grabbed left");
                        hit.transform.gameObject.GetComponent<PlayerController>().setMove(false);
                        hit.transform.gameObject.GetComponent<PlayerController>().setGrab(false);
                        hit.rigidbody.velocity = Vector2.zero;
                        hit.transform.position = new Vector3(transform.position.x - 3, transform.position.y);
                        ViewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
                        smashMeter.GetComponent<RectTransform>().anchoredPosition = new Vector2(1920 * ViewportPos.x, 1080 * ViewportPos.y);
                        speed = 3.5f;
                        grabbed = true;
                        if (!running)
                        {
                            smashMeter.value = 15;
                            StartCoroutine(smashin());
                        }
                    }
                    else
                    {
                        grabbed = false;
                        
                    }
                }
                else if (grabbed && !brokeOut)
                {
                    grabbed = false;
                    hit.transform.gameObject.GetComponent<PlayerController>().setMove(true);
                    hit.transform.gameObject.GetComponent<PlayerController>().setGrab(true);
                    hit.rigidbody.velocity += new Vector2(-throwForce, 0);
                    speed = 10f;
                    StopCoroutine(smashin());
                    smashMeterObject.SetActive(false);
                    running = false;
                }
            }
        }

        //fixed rotation, avoid any "potential" issues
        transform.eulerAngles = new Vector3(0, 0, 0);

        //movement stuff
        if(canMove)
        {
            //left/right movement
            if (Input.GetButton("Horizontal" + playerCode)) //right
            {
                //Time.deltaTime is the amount of time between
                // one frame and the next, used for smoothing
                float dist = Input.GetAxis("Horizontal" + playerCode) * speed;
                transform.position +=
                    new Vector3(dist * Time.deltaTime, 0);

            }

            //flip left/right
            //if facing right and press left, flip
            if (facingRight && (Input.GetAxis("Horizontal" + playerCode) < 0))
            {
                facingRight = false;
                //flip the x, leave the y and z alone
                transform.localScale = new Vector3(
                    -transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z);
            }

            //if not facing right and press right, flip
            else if (!facingRight && (Input.GetAxis("Horizontal" + playerCode) > 0))
            {
                facingRight = true;
                //flip the x, leave the y and z alone
                transform.localScale = new Vector3(
                    -transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z);
            }

            //jump
            canJump = false;
            //check if there is a platform beneath the player
            if (Physics2D.OverlapArea(j1.transform.position, j2.transform.position) != null &&
                Physics2D.OverlapArea(j1.transform.position, j2.transform.position).gameObject.tag == "Platform")
            { canJump = true; }
            //if jumping on onther player
            if (Physics2D.OverlapArea(j1.transform.position, j2.transform.position) != null &&
               (Physics2D.OverlapArea(j1.transform.position, j2.transform.position).gameObject.tag == "Player") &&
                Physics2D.OverlapArea(j1.transform.position, j2.transform.position).gameObject != this.gameObject)
            {
                canJump = true;
            }


                //the moment they press the space bar, apply up force
                if (Input.GetButtonDown("Jump" + playerCode) && canJump)
            {
                rb2d.AddForce(new Vector3(0, jumpForce));
            }
        }

        //keep this player on the screen!
        {

            //player bounds are -28 to 28 on x axis
            //-18 to 18 on y axis

            //while player is above the bounds of the screen
            if (this.transform.position.y > 18)
            {
                //increase/decrease their gravity scale exponentially until they are below top of screen
                rb2d.gravityScale = (2* this.transform.position.y - 18);
            }
            else
            {
                rb2d.gravityScale = 3;
            }
            

            //if player goes left or right or below the screen

            //respawn them but take away points


        }


    }

    public void setMove(bool move)
    {
        canMove = move;
    }
    void OnTriggerExit2D(Collider2D col)
    {

        //if hit the bonk zone of the other player
        if (col.gameObject.tag == "BonkDetect" && col.gameObject.transform.parent != this.gameObject)
        {
            StartCoroutine(BonkConformation(col));
        }
    }

    IEnumerator BonkConformation(Collider2D col)
    {
        //wait till they jump
        yield return new WaitUntil(() => (Input.GetButton("Jump" + playerCode) && canJump));
        //make sure they are still in the area
            if ((col.transform.parent.position.y + 4 <= transform.position.y) && Mathf.Abs(col.transform.parent.position.x - transform.position.x) <= 2)
            {

                GameObject other = col.gameObject.transform.parent.gameObject;

                other.GetComponent<PlayerController>().bonk(facingRight);
            }
        
    }

    IEnumerator smashin()
    {
        smashMeterObject.SetActive(true);
        running = true;
        brokeOut = false;
        while(smashMeter.value >= 0)
        {
            smashMeter.value -= 1.5f;
            yield return new WaitForSeconds(0.05f);
            if(smashMeter.value >= smashMeter.maxValue)
            {
                print("broke out");
                brokeOut = true;
                hit.transform.gameObject.GetComponent<PlayerController>().setGrab(true);
                hit.transform.gameObject.GetComponent<PlayerController>().setMove(true);
                if(facingRight) hit.rigidbody.velocity += new Vector2(5, throwForce);
                else hit.rigidbody.velocity += new Vector2(-5, throwForce);
                speed = 10f;
                yield return new WaitForSeconds(2);
                brokeOut = false;
                break;
            }
        }
        smashMeterObject.SetActive(false);
        running = false;
    }

    public void bonk(bool direction)
    {
        print("BONK");
        //direction is the direction the other player jumped off
        //if player jumped off right push back to the left, else push right
        if (direction)
        {
            rb2d.velocity += new Vector2(-bonkForce, 0);
        }
        else
        {
            rb2d.velocity += new Vector2(bonkForce, 0);
        }
    }
/*
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Pickup")
        {
            col.gameObject.GetComponent<Pickup>().grab(this.gameObject);
        }
    }
    */

}
      
    


