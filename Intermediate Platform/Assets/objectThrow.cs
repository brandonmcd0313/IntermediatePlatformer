using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectThrow : MonoBehaviour
{
	public bool rightPush;
	public bool grabbed;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if(transform.position.y <= -18)
        {
			Destroy(gameObject);
        }
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != null)
		{
			if (other.tag == "Player" && !grabbed)
			{
				
				if (rightPush)
				{
					other.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(9, 0);
				}
				else
				{
					other.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-9, 0);
				}
			}

			if(other.tag != "BonkDetect" && !grabbed)
            {
				Destroy(gameObject);
			}
			

		}
		

	}

}
