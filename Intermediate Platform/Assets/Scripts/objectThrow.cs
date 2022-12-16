using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectThrow : MonoBehaviour
{
	public bool rightPush; //the direction the player is pushed towards when hit with this object
	public bool grabbed; //when the player grabs this item
	public bool idle = true; //cannot hit player while idle 
	public AudioClip objectBurst;
	AudioSource aud;
	public bool playerHit;
	// Use this for initialization
	void Start()
	{
		aud = GetComponent<AudioSource>();
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
				other.GetComponent<InteractionManager>().WeaponHit();
				
				if (rightPush)
				{
					other.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(9, 0);
				}
				else
				{
					other.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-9, 0);
				}
				Destroy(gameObject);
			}

			


		}

		if (other.tag != "BonkDetect" && !grabbed)
		{
			Destroy(gameObject);
		}
	}

}
