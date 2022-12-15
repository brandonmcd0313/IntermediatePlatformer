using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pipe : MonoBehaviour {
	[SerializeField] Sprite[] stage;
	[SerializeField] float increment;
    SpriteRenderer sr; int curStage = -1;
	private float value;
	AudioSource aud;
	public AudioClip epic;
	// Use this for initialization
	void Start () {
		aud = GameObject.Find("Player1").GetComponent<AudioSource>();
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

			//epic sound
			aud.PlayOneShot(epic);
			Invoke("nextScene", 4f);
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
    void nextScene()
    {
        SceneManager.LoadScene(2);


    }

}
