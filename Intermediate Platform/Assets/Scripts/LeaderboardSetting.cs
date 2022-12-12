using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LeaderboardSetting : MonoBehaviour {
	//this script will check if either of these players are eligible for a leaderboard position
	//if so it will allow them to enter their name and become #cool
	int pos = 0; char[] Playername = {'A','A','A'}; bool done = false;
	int newHigh; string playerCode;
	[SerializeField] Text nameInput; int level;
	bool player; //p1 is false, p2 is true
	void Start () {
        ScoreManager.score1 = 900;
        //PlayerPrefs.DeleteAll();
        StartCoroutine(flash());
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
            print(PlayerPrefs.GetString(key));
			if (PlayerPrefs.GetString(key) == "")
			{
				//set value defaults
				if (i == 1)
				{
					PlayerPrefs.SetString(key, "XXX,500");
				}
				else if (i == 2)
				{
                    PlayerPrefs.SetString(key, "YYY,400");
                }
				else if(i == 3)
				{
                    PlayerPrefs.SetString(key, "ZZZ,300");
                }	
				else if(i == 4)

                {
                    PlayerPrefs.SetString(key, "ABC,200");
                }
				else if(i == 5)
				{
                    PlayerPrefs.SetString(key, "POO,100");
                }
			}

        }

        for (int i = 1; i <= 5; i++)
            {
            string key = "LB" + i;
            string[] values = PlayerPrefs.GetString(key).Split(',');
            //check if this score is greater than that one
            if (newHigh > Convert.ToInt32(values[1]))
            {

                level = i;
                return;
            }

        }

        //if this point is hit it isnt a new highscore...
        //load the leaderboard screen after ssetting player to 0
        DisplayLEaderBoard.currentPlayer = 0;
        SceneManager.LoadScene(5);
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
        if (pos < 3)
        {
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
                        Playername[pos] = (char)(Playername[pos] - 1);
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
        if(Input.GetButtonDown("Fire1" + playerCode))
        {
            pos++;
        }
    }

	IEnumerator flash()
	{
		bool a = false, b = false, c = false;
        yield return new WaitForSeconds(0.1f);
        //this coroutine shows the active character
        while (pos == 0)
		{
            nameInput.text = Playername[0] + " " + Playername[1] + " " + Playername[2];
			if(Playername[pos] == 'A' && !a)
			{
                yield return new WaitForSeconds(0.2f);
                nameInput.text = " " + " " + Playername[1] + " " + Playername[2];
                //yield return new WaitForSeconds(0.5f);
            }
			else
			{
				a = true;
			}
            yield return new WaitForSeconds(0.1f);
        }

        while (pos == 1)
        {
            nameInput.text = Playername[0] + " " + Playername[1] + " " + Playername[2];
            if (Playername[pos] == 'A' && !b)
            {
                yield return new WaitForSeconds(0.2f);
                nameInput.text = Playername[0] + " " +" " + " " + Playername[2];
                //yield return new WaitForSeconds(0.5f);
            }
            else
            {
                b = true;
            }
            yield return new WaitForSeconds(0.1f);
        }

        while (pos == 2)
        {
            nameInput.text = Playername[0] + " " + Playername[1] + " " + Playername[2];
            if (Playername[pos] == 'A' && !c)
            {
                yield return new WaitForSeconds(0.2f);
                nameInput.text = Playername[0] + " " + Playername[1] + " " + " ";
                //yield return new WaitForSeconds(0.5f);
            }
            else
            {
                c = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        string playya = new string(Playername);
        //now name is filled!
        //reorder the leaderboard accordingly
        List<String> players = new List<string>();
        for(int i = 1; i <= 5; i++)
        {
            string key = "LB" + i;
            players.Add(PlayerPrefs.GetString(key));
        }
        string input = playya + "," + newHigh;
        players.Insert((level - 1), input.ToString());
        //reassign playerprefs
        for (int i = 1; i <= 5; i++)
        {
            string key = "LB" + i;
            PlayerPrefs.SetString(key, players[i-1]);
        }

        //set player val for the leaderboard display
        DisplayLEaderBoard.currentPlayer = level;

        //goto the leaderboard
        SceneManager.LoadScene(5);
    }
}

