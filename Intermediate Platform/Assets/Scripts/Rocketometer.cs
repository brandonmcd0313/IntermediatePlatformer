using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocketometer : MonoBehaviour {
	//this script is on the slider
	[SerializeField] int goal;
	private Slider slider; 
	// Use this for initialization
	void Start () {
		slider = this.GetComponent<Slider>();
		slider.value = 0;
		slider.minValue = 0;
		slider.maxValue = goal;
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = ScoreManager.score1 + ScoreManager.score2;
		if(slider.value <= goal-10)
		{
			//exit scene and goto next level.
		
		}
	}
}
