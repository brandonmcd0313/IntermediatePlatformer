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
        tool = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbed)
        {

            tool.transform.position = this.transform.position;
        }
        if (Input.GetButtonDown("Fire3" + playerCode))
        {
            tool = FindClosestGameObjectWithTag(transform.position, "Tool");
            print(tool);
            if (IsWithinOneUnit(tool))
            {
                print("grabbed");
                grabbed = true;
                tool.GetComponent<Tool>().stopVibration();
            }
            else
            {
                tool = null;
            }
        }
        else if(Input.GetButtonUp("Fire3" + playerCode) && tool && grabbed)
        {
            //realese

            grabbed = false;
            print("launch");
            grabbed = false;
            //launch in direction
            facingRight = this.GetComponent<PlayerController>().getDirection();
            past = tool;
            tool = null;
            past.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 0.5f);
            past.GetComponent<Tool>().thrown(this.gameObject, throwForce);
        }
        else if (grabbed && tool)
        {
            tool.transform.position = this.transform.position;
        }

    }
  /*
    void OnTriggerEnter2D(Collider2D col)
    {
        //fuck this im reformating to an overlap....
        print("ONTRIG");
        if(tool == null)
        {

            if (col.gameObject.tag == "Tool")
            {
                tool = col.gameObject;
            }
        }
    }
  */
    public bool IsWithinOneUnit(GameObject otherObject)
    {
        // First, we need to get the position of the current object and the other object.
        Vector3 myPosition = this.transform.position;
        Vector3 otherPosition = otherObject.transform.position;

        // Next, we calculate the distance between the two objects using the Vector3.Distance method.
        float distance = Vector3.Distance(myPosition, otherPosition);

        // Finally, we check if the distance is less than or equal to one unit, and return the result.
        return distance <= 2.75f;
    }

    public GameObject FindClosestGameObjectWithTag(Vector3 position, string tag)
    {
        // Get all the GameObjects in the scene with the given tag
        GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        // Keep track of the closest GameObject found so far
        GameObject closestGameObject = null;
        float closestDistance = 1000f;

        // Loop through all the GameObjects with the given tag
        foreach (GameObject gameObject in gameObjectsWithTag)
        {
            // Calculate the distance between the given position and the current GameObject
            float distance = Vector3.Distance(position, gameObject.transform.position);

            // If this GameObject is closer than the closest GameObject found so far, update the closest GameObject
            if (distance < closestDistance)
            {
                closestGameObject = gameObject;
                closestDistance = distance;
            }
        }

        // Return the closest GameObject
        return closestGameObject;
    }

}
