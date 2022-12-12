using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LeaderboardSetting : MonoBehaviour {
	//this script will check if either of these players are eligible for a leaderboard position
	//if so it will allow them to enter their name and become #cool
	int pos = 0; char[] Playername = {'A','A','A'}; bool done = false;
	int newHigh; string playerCode;
	[SerializeField] Text nameInput;
	bool player; //p1 is false, p2 is true
	void Start () {
		if(ScoreManager.score1 > ScoreManager.score2)
		{
			newHigh = ScoreManager.score1;
			player = false;
		}
		else
		{
            newHigh = ScoreManager.score2;
			player = true;
        }

		//i am hardcoding leaderboard to 5 people, becuase I WANT TOO :P
		for (int i = 1; i <= 5; i++)
		{
            string key = "LB" + i;
			if (PlayerPrefs.GetString(key) == null)
			{
				//set value defaults
				if (i == 1)
				{
					PlayerPrefs.SetString(key, "1st,XXX,000");
				}
				else if (i == 2)
				{
                    PlayerPrefs.SetString(key, "2nd,XXX,000");
                }
				else if(i == 3)
				{
                    PlayerPrefs.SetString(key, "3rd,XXX,000");
                }	
				else if(i == 4)

                {
                    PlayerPrefs.SetString(key, "4th,XXX,000");
                }
				else if(i == 5)
				{
                    PlayerPrefs.SetString(key, "5th,XXX,000");
                }
			}
                string[] values = PlayerPrefs.GetString(key).Split(',');
			//check if this score is greater than that one
			if (newHigh > Convert.ToInt32(values[2]))
			{
				StartCoroutine(newHIGHSCORE(i));
				return;

            }
        }
        }
	
	IEnumerator newHIGHSCORE(int level)
	{
		yield return new WaitForEndOfFrame();
		//prompt for name...

		//wait until name is 3 chars long

		//reorder the leaderboard accordingly

		//set player val for the leaderboard display
		//note TODO: implement that value to make that player yellow

		//goto the leaderboard
	}
	// Update is called once per frame
	void Update () {
		//p1
		if(!player)
		{
			playerCode = "P1";
        }
		else
		{
            playerCode = "P2";
        }

        //on press
        if (Input.GetButtonDown("Vertical" + playerCode))
        {
			//A = 65 Z = 90
            //down
            if ((Input.GetAxis("Vertical" + playerCode)) < 0)
            {
				//char = -1 ie e > d 
				if (Playername[pos] != 65)
				{
					Playername[pos] =  (char)(Playername[pos] - 1);
				}
				else { Playername[pos] = 'Z'; }
            }
            //up
            else if ((Input.GetAxis("Vertical" + playerCode)) > 0)
            {
                //char =1 ie a > b
                if (Playername[pos] != 90)
                {
                    Playername[pos] = (char)(Playername[pos] + 1);
                }
                else { Playername[pos] = 'A'; }

            }
        }
    }
}
