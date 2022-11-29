using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    //this is player two, simular to player one but not
    //button maps end with "P2" to indicate difference

    //player1 uses RDFG to move, A,S,W to fire
    //Q to jump
    //other bindings are E, [, ], 6

    // Use this for initialization
    bool canMove, canJump;
    public float speed, jumpForce;
    public bool facingRight = true;
    public GameObject j1, j2; //jump check objects 
    Rigidbody2D rb2d;
    Transform init;
    void Start()
    {
        canMove = true;
        rb2d = this.GetComponent<Rigidbody2D>();
        init = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1P2"))
        {

        }

        //fixed rotation, avoid any "potential" issues
        transform.eulerAngles = new Vector3(0, 0, 0);

        if (canMove)
        {
            //left/right movement
            if (Input.GetButton("HorizontalP2")) //right
            {
                //Time.deltaTime is the amount of time between
                // one frame and the next, used for smoothing
                float dist = Input.GetAxis("HorizontalP2") * speed;
                transform.position +=
                    new Vector3(dist * Time.deltaTime, 0);

            }

            //flip left/right
            //if facing right and press left, flip
            if (facingRight && (Input.GetAxis("HorizontalP2") < 0))
            {
                facingRight = false;
                //flip the x, leave the y and z alone
                transform.localScale = new Vector3(
                    -transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z);
            }

            //if not facing right and press right, flip
            else if (!facingRight && (Input.GetAxis("HorizontalP2") > 0))
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
            if (Physics2D.OverlapArea(j1.transform.position,
                j2.transform.position) != null &&
                Physics2D.OverlapArea(j1.transform.position,
                j2.transform.position).gameObject.tag == "Platform")
            { canJump = true; }


            //the moment they press the space bar, apply up force
            if (Input.GetButton("JumpP2") && canJump)
            {
                rb2d.AddForce(new Vector3(0, jumpForce));
            }
        }
    }
}
