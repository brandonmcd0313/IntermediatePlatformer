using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    bool canMove, canJump;
    public float speed, jumpForce;
    public bool facingRight = true;
    public GameObject j1, j2; //jump check objects 
    Rigidbody2D rb2d;
    void Start () {
        canMove = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //left/right movement
            if (Input.GetButton("Horizontal")) //right
            {
                //Time.deltaTime is the amount of time between
                // one frame and the next, used for smoothing
                float dist = Input.GetAxis("Horizontal") * speed;
                transform.position +=
                    new Vector3(dist * Time.deltaTime, 0);

            }

            //flip left/right
            //if facing right and press left, flip
            if (facingRight && (Input.GetAxis("Horizontal") < 0))
            {
                facingRight = false;
                //flip the x, leave the y and z alone
                transform.localScale = new Vector3(
                    -transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z);
            }

            //if not facing right and press right, flip
            else if (!facingRight && (Input.GetAxis("Horizontal") > 0))
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
                canJump = true;


            //the moment they press the space bar, apply up force
            if (Input.GetButton("Jump") && canJump)
            {
                rb2d.AddForce(new Vector3(0, jumpForce));
            }
        }
       }
}
