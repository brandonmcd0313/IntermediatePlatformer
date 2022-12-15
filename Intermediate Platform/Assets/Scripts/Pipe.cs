using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {
	[SerializeField] Sprite[] stage;
	[SerializeField] float increment;
    SpriteRenderer sr; int curStage = -1;
	private float value;
	// Use this for initialization
	void Start () {
		value = 1;
		sr = gameObject.GetComponent<SpriteRenderer>();
		stageUp();

    }
	
	// Update is called once per frame
	void Update () {
		if(value % increment == 0)
		{
			value++;
			stageUp();
		}
	}

	void stageUp()
	{
		if(curStage + 1 < stage.Length)
		{
			curStage++;
			sr.sprite = stage[curStage];
			//reset collider
			Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
			this.gameObject.AddComponent<PolygonCollider2D>();
		}
		else { 
		}
	}
    void OnCollisionEnter2D(Collision2D colsion)
	{
		GameObject other = colsion.collider.gameObject;
		if(other.tag == "Tool")
		{
			value++;
		}
	}
    
    }
