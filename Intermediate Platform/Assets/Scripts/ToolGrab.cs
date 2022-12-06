using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class ToolGrab : MonoBehaviour {
    private GameObject tool;
    bool facingRight; bool grabbed;
    [SerializeField] string playerCode; //P1 or P2, determines input mapping
    [SerializeField] float throwForce;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire3" + playerCode) && (tool!= null))
        {
            grabbed = true;
            tool.GetComponent<Tool>().stopVibration();
            tool.transform.position = this.transform.position;
        }
        else if(grabbed)
        {
            grabbed = false;
            //launch in direction
            facingRight = this.GetComponent<PlayerController>().getDirection();
            if(facingRight)
            {
                tool.GetComponent<Rigidbody2D>().velocity += new Vector2(throwForce, 0);
            }
           else
            {
                tool.GetComponent<Rigidbody2D>().velocity += new Vector2(-throwForce, 0);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Tool")
        {
            tool = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(!grabbed)
        {
            tool = null;
        }
    }
}
