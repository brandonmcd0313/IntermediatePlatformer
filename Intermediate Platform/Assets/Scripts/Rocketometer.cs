using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocketometer : MonoBehaviour {
	//this script is on the slider
	[SerializeField] int goal;
	private Slider slider;
	AudioSource aud;
	public AudioClip blastOff; bool sound = false;
	// Use this for initialization
	void Start () {
        aud = GameObject.Find("Player1").GetComponent<AudioSource>();
        slider = this.GetComponent<Slider>();
		slider.value = 0;
		slider.minValue = 0;
		slider.maxValue = goal;
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = ScoreManager.score1 + ScoreManager.score2;
		if(slider.value >= goal-10)
		{
			if(!sound)
			{
				aud.PlayOneShot(blastOff);
				sound = true;
			}
			//exit scene and goto next level.
			Invoke("nextScene", 4f);
        }
	}

	void nextScene()
	{
		//fuck level 2
		//more like level boo
		SceneManager.LoadScene(3);
	}
}
