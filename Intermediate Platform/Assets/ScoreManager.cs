using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	//beacuse score will be managed in multiple scenes we have a static score manager
	public static int score1; //player one score
	public static int score2; //player two score
	public Text scoreText1, scoreText2; //player score Texts, different for every scene

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreText1.text = "Player 1 : " + score1;
        scoreText2.text = "Player 2 : " + score2;

		//whoever is ahead in green, behind in red
		if(score1>score2)
		{
			scoreText1.color = Color.green;
			scoreText2.color = Color.red;
		}
		else
		{
            scoreText2.color = Color.green;
            scoreText1.color = Color.red;
        }
    }

	//have a particle effect that shows when score increases or decreases?
}
