using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovement : MonoBehaviour
{
	public int distance, speed;
	public bool runnin;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!runnin)
        {
			StartCoroutine(move(distance, speed));
        }
	}
	IEnumerator move(int d, int s)
    {
		
		yield return null;
    }
}
