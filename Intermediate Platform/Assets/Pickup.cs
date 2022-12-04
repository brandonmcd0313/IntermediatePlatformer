using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
	[SerializeField] float size;
	[SerializeField] int value;
	[SerializeField] GameObject scorePop;
	float homeX, homeY; float randDiff;
	bool instating;
	// Use this for initialization
	void Start () {
		randDiff = Random.Range(-100f, 100f);
		homeX = this.transform.position.x;
        homeY = this.transform.position.y;
		Invoke("allowPick", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		//move up and down slightly
		transform.localPosition = new Vector3(homeX, homeY + (size *Mathf.Sin(Time.time+randDiff)));
	}

    void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			if(instating)
			{
				//if just spawned dont allow pickup
                Destroy(this.gameObject);
            }
			//instatiate point thing
			GameObject pop = Instantiate(scorePop, this.transform.position, Quaternion.identity);
			pop.GetComponent<ScorePopUp>().setVal(value);
			//give the player points
			if(col.gameObject.name == "Player1")
			{
				ScoreManager.score1 += value;
			}
            else if (col.gameObject.name == "Player2")
            {
                ScoreManager.score2 += value;
            }
			//destroy this object
			print("rip");
            Destroy(this.gameObject);
        }

    }

	void allowPick()
	{
		instating = false;
	}
}
