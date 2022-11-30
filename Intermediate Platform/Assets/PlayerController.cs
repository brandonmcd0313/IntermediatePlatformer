﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool canMove, canJump, jumping;
    public float speed, jumpForce, bonkForce;
    public bool facingRight = true;
    public GameObject j1, j2; //jump check objects 
    Rigidbody2D rb2d;
    Transform init;
    public string playerCode; //P1 or P2, determines input mapping
    public bool grabbed; //if the player has grabbed the other player or not
    RaycastHit2D hit;
    void Start () {
        canMove = true;
        rb2d = this.GetComponent<Rigidbody2D>();
        init = this.transform;
	}

    // Update is called once per frame
    void Update()
    {

        if(facingRight)
        {
            if(!grabbed)
            {
                hit = Physics2D.Raycast(new Vector3(transform.position.x + 2, transform.position.y), Vector2.right, 2f);
            }
            
            if (Input.GetButton("Fire1" + playerCode))
            {
                if ((hit.transform != null && hit.transform.tag == "Player") || grabbed)
                {
                    print("grabbed right");
                    hit.transform.position = new Vector3(transform.position.x + 3, transform.position.y);
                    grabbed = true;
                }
            }
            else if(grabbed)
            {
                grabbed = false;
                hit.rigidbody.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
            }
        }
        else
        {
            if(!grabbed)
            {
                hit = Physics2D.Raycast(new Vector3(transform.position.x - 2, transform.position.y), Vector2.left, 2f);
            }

            if (Input.GetButton("Fire1" + playerCode))
            {
                if ((hit.transform != null && hit.transform.tag == "Player") || grabbed)
                {
                    print("grabbed left");
                    hit.transform.position = new Vector3(transform.position.x - 3, transform.position.y);
                    grabbed = true;
                }
            }
            else if (grabbed)
            {
                grabbed = false;
                hit.rigidbody.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
            }
        }
        
        

        //fixed rotation, avoid any "potential" issues
        transform.eulerAngles = new Vector3(0, 0, 0);
        
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
                if (Input.GetButton("Jump" + playerCode) && canJump)
            {
                rb2d.AddForce(new Vector3(0, jumpForce));
            }
        }
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


    public void bonk(bool direction)
    {
        print("BONK");
        //direction is the direction the other player jumped off
        //if player jumped off right push back to the left, else push right
        if (direction)
        {
            rb2d.AddForce(new Vector3(-bonkForce, 0));
        }
        else
        {
            rb2d.AddForce(new Vector3(bonkForce, 0));
        }
    }
}
      
    


