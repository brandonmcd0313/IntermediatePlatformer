using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour {
	private bool avalible; private GameObject current;
	[SerializeField] GameObject pickup; bool waitin;
	// Use this for initialization
	void Start () {
		//spawn the pickup 
		current = Instantiate(pickup, this.transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
        //check if pickup still exists
        //if not start cooldown for ANTOHER
        if (current == null & !waitin)
		{
			StartCoroutine(newSpawn());
            waitin = true;
        }
		
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
