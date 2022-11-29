using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {
    //this is player one, simular to player two but not
    //button maps end with "P1" to indicate difference

    //player1 uses arrow keys to move, left ctrl,alt,shift to fire
    //space to jump
    //other bindings are Z, X, C, 3

    // Use this for initialization
    bool canMove, canJump;
    public float speed, jumpForce;
    public bool facingRight = true;
    public GameObject j1, j2; //jump check objects 
    Rigidbody2D rb2d;
    Transform init;
    void Start () {
        canMove = true;
        rb2d = this.GetComponent<Rigidbody2D>();
        init = this.transform;
	}

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButton("Fire1P1"))
        {

        }

        //fixed rotation, avoid any "potential" issues
        transform.eulerAngles = new Vector3(0, 0, 0);
        
        if(canMove)
        {
            //left/right movement
            if (Input.GetButton("HorizontalP1")) //right
            {
                //Time.deltaTime is the amount of time between
                // one frame and the next, used for smoothing
                float dist = Input.GetAxis("HorizontalP1") * speed;
                transform.position +=
                    new Vector3(dist * Time.deltaTime, 0);

            }

            //flip left/right
            //if facing right and press left, flip
            if (facingRight && (Input.GetAxis("HorizontalP1") < 0))
            {
                facingRight = false;
                //flip the x, leave the y and z alone
                transform.localScale = new Vector3(
                    -transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z);
            }

            //if not facing right and press right, flip
            else if (!facingRight && (Input.GetAxis("HorizontalP1") > 0))
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
            if (Input.GetButton("JumpP1") && canJump)
            {
                rb2d.AddForce(new Vector3(0, jumpForce));
            }
        }
       }
}
