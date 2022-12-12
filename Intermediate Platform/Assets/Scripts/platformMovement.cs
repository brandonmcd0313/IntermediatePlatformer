using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovement : MonoBehaviour
{
	public float distance, speed;
	public bool runnin, up = true, down;
	public bool vertical, horizontal;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

		if(!runnin && vertical)
        {
			runnin = true;
			StartCoroutine(move(distance, speed));
        }

		if(!runnin && horizontal)
        {
			runnin = true;
			StartCoroutine(move2(distance, speed));
		}

	}
	IEnumerator move(float d, float s) //moves the platform up and down
    {
		while (d >= 0)
		{
			if (up) //move platform upwards
			{
				d -= speed;
				transform.position = new Vector3(transform.position.x, transform.position.y + speed);
				yield return new WaitForSeconds(speed / 10);
			}
			else //moves the platform downwards
			{
				d -= speed;
				transform.position = new Vector3(transform.position.x, transform.position.y - speed);
				yield return new WaitForSeconds(speed / 10);
			}
		}


		if(up) //changes up to down
        {
			up = false;
			down = true;
        }
		else if(down) //changes down to up
        {
			up = true;
			down = false;
        }

		runnin = false;
		yield return null;

    }

	IEnumerator move2(float d, float s) //move the platform left and right
	{
		while (d >= 0)
		{
			if (up)//right
			{
				d -= speed;
				transform.position = new Vector3(transform.position.x + speed, transform.position.y );
				yield return new WaitForSeconds(speed / 10);
			}
			else //left
			{
				d -= speed;
				transform.position = new Vector3(transform.position.x - speed, transform.position.y);
				yield return new WaitForSeconds(speed / 10);
			}
		}


		if (up)//changes from right to left
		{
			up = false;
			down = true;
		}
		else if (down) //changes from left to right
		{
			up = true;
			down = false;
		}

		runnin = false;
		yield return null;

	}
}
