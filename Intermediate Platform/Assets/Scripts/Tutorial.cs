﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
	[SerializeField] GameObject TutorialImage, blur;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("Tutorial") == 1)
		{

            TutorialImage = Instantiate(TutorialImage);
        }
		else
		{
			//destroy the tutorial manager
			Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//both players submit
		if(Input.GetButton("SubmitP1") && Input.GetButton("SubmitP2"))
		{
			//start spawnzones
			SpawnZone.tutWait = 0;
			//allow playermovements
			PlayerController.tutWait = 0;
		}
	}
}
