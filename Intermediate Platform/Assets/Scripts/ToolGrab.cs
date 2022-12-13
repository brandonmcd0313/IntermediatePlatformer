using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class ToolGrab : MonoBehaviour
{
    private GameObject tool, past;
    bool facingRight; bool grabbed;
    [SerializeField] string playerCode; //P1 or P2, determines input mapping
    [SerializeField] float throwForce; bool running;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "Player1")
        {

            print(tool);
        }
        if (Input.GetButton("Fire3" + playerCode) && tool)
        {
                print("grabbed");
                grabbed = true;
                tool.GetComponent<Tool>().stopVibration();
                tool.transform.position = this.transform.position;
                return;
            }
        
        else if (grabbed && tool)
        {
            print("launch");
            grabbed = false;
            //launch in direction
            facingRight = this.GetComponent<PlayerController>().getDirection();
                past = tool;
                tool = null;
                past.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 0.5f);
            past.GetComponent<Tool>().thrown(this.gameObject, throwForce);
            // StartCoroutine(launch());
            return;

        }
        else if(!grabbed && tool)
        {
            tool = null;
        }

    }
  
    void OnTriggerStay2D(Collider2D col)
    {
        print("ONTRIG");
            if (col.gameObject.tag == "Tool")
            {
                tool = col.gameObject;
            }
            else
        {
            tool = null;
        }
    }
    
}
