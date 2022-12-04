using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour {
	private bool avalible; private GameObject current;
	[SerializeField] GameObject pickup; bool waitin;
	public static int tutWait;
	// Use this for initialization
	void Start () {
		//if tutorial is happening wait untill it is exited
		if(PlayerPrefs.GetInt("Tutorial") == 1)
		{
			tutWait = 1;
		}
		else
		{

            //spawn the pickup 
            current = Instantiate(pickup, this.transform.position, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
        //check if pickup still exists
        //if not start cooldown for ANTOHER
        if (current == null & !waitin && (tutWait == 0))
		{
			StartCoroutine(newSpawn());
            waitin = true;
        }
		
	}	

	IEnumerator firstWait()
	{
		waitin = true;
		yield return new WaitUntil(() => (tutWait == 0));
        //spawn the pickup 
        current = Instantiate(pickup, this.transform.position, Quaternion.identity);
		waitin = false;
    }
	IEnumerator newSpawn()
	{
		print("begin");
		float wait = UnityEngine.Random.Range(3f, 10f);
		yield return new WaitForSeconds(wait);
        //spawn the pickup 
        current = Instantiate(pickup, this.transform.position, Quaternion.identity);
        waitin = false;
		
	}
}
