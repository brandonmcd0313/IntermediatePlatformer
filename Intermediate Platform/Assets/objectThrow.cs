using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectThrow : MonoBehaviour
{
	public bool rightPush;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag != null)
        {
			if(other.tag == "Player")
            {
				if(rightPush)
                {
					other.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
				}
				else
                {
					other.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
				}

				
            }


			if(other.tag != "BonkDetect")
            {
				Destroy(gameObject);
			}
			
        }
	}
}
