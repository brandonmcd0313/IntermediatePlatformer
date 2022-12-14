using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour {
	private bool avalible; private GameObject current;
	[SerializeField] GameObject pickup; bool waitin;
	public static int tutWait; bool begun;
	// Use this for initialization
	void Start () {
		//if tutorial is happening wait untill it is exited
		if(PlayerPrefs.GetInt("Tutorial") == 1)
		{
			begun = false;
			tutWait = 1;
		}
		else
		{
			begun = true;
            //spawn the pickup 
            current = Instantiate(pickup, this.transform.position, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
        //check if pickup still exists
        //if not start cooldown for ANTOHER
		if(current)
		{
			return;
		}
		if(tutWait == 1)
		{
			return;
		}
		else if(tutWait == 0 && !begun)
		{
           current = (Instantiate(pickup, this.transform.position, Quaternion.identity));
			begun = true;
        }
        if (current == null & !waitin)
		{
			StartCoroutine(newSpawn());
            waitin = true;
        }
		
	}	

	IEnumerator newSpawn()
	{
		//print("begin");
		float wait = UnityEngine.Random.Range(3f, 7f);
		yield return new WaitForSeconds(wait);
        //spawn the pickup 
        current = Instantiate(pickup, this.transform.position, Quaternion.identity);
		if(current.tag == "Tool")
		{
			current.transform.eulerAngles = new Vector3(0, 0, 90);

            //Change the rigidbody to be dynamic
			// current.GetComponent<Rigidbody>().isKinematic = false;
        }
        waitin = false;
		
	}

	
}
