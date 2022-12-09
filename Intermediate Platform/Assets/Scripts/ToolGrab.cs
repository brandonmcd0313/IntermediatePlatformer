using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class ToolGrab : MonoBehaviour {
    private GameObject tool, past;
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
         
            past = tool;
            StartCoroutine(launch());
            
            
        }

    }
    IEnumerator launch()
    {
        tool.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 0.5f);
        tool = null;
        past.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        past.GetComponent<Rigidbody2D>().gravityScale = -0.2f;
        if (facingRight)
        {
            past.GetComponent<Rigidbody2D>().velocity += new Vector2(throwForce, 0);
        }
        else
        {
            past.GetComponent<Rigidbody2D>().velocity += new Vector2(-throwForce, 0);
        }
        yield return new WaitForSeconds(0.1f);
        //disable trigger so it can hit stuffs
        print("a");
        try
        {

            past.GetComponent<Collider2D>().isTrigger = false;
            past.GetComponent<Tool>().thrown(this.gameObject);
        }
        catch (MissingReferenceException)
        {
            //eat it
        }
        yield return new WaitForSeconds(0.5f);
        try
        {

            past.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        }
        catch (MissingReferenceException)
        {
            //eat it
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
