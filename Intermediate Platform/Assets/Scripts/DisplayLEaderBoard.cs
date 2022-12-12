using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLEaderBoard : MonoBehaviour {

    // Use this for initialization

    //the way the leaderboard is setup is that the rank,name,score are in a playerpref
    //the playerprefs are named LB1 - end
    //the format is rank,name,score
    //using .spilt i should be able to easily declare it...
    public static int currentPlayer = 0;
    [SerializeField] Text bigText; //for score
    [SerializeField] Text smallText; //for name or rank
	int amt = 5;
	void Start () {
		//FOR TESTING REMOVE LAT
        /*
		PlayerPrefs.SetString("LB1", "1st,AAA,9189");
        PlayerPrefs.SetString("LB2", "2nd,BAM,8734");
        PlayerPrefs.SetString("LB3", "3rd,FFD,5935");
        PlayerPrefs.SetString("LB4", "4th,CGD,4189");
        PlayerPrefs.SetString("LB5", "5th,---,----");
        */
		//add a system to check amount of player prefs available 
		StartCoroutine(displayValues());
    }

    void Update()
    {
        //exit this scene script goes here
    }
	IEnumerator displayValues()
	{
		int currentLevel = 120; // y value of the text
		yield return new WaitForSeconds(0.1f);
		for(int i = 1; i <= amt; i++)
		{
            
			string key = "LB" + i;
			string[] values = PlayerPrefs.GetString(key).ToString().Split(',');
            print(PlayerPrefs.GetString(key));
			//rank object
			Text rank = Instantiate(smallText);
			//set as child of the canvas
			rank.transform.SetParent(GameObject.Find("Canvas").transform);
            if (i == 1)
            {
                rank.text = "1st";
            }
            else if (i == 2)
            {
                rank.text = "2nd";
            }
            else if (i == 3)
            {
                rank.text = "3rd";
            }
            else if (i == 4)

            {
                rank.text = "4th";
            }
            else if (i == 5)
            {
                rank.text = "5th";
            }
            //palce it
            rank.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            rank.GetComponent<RectTransform>().localPosition = new Vector2(-500f, currentLevel);

            //name object
            Text name = Instantiate(smallText);
            //set as child of the canvas
            name.transform.SetParent(GameObject.Find("Canvas").transform);
            name.text = values[0].ToString();
            //palce it
            name.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            name.GetComponent<RectTransform>().localPosition = new Vector2(0f, currentLevel);

            //for score
            Text score = Instantiate(bigText);
            //set as child of the canvas
            score.transform.SetParent(GameObject.Find("Canvas").transform);
            score.text = values[1].ToString();
            //palce it
            score.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            score.GetComponent<RectTransform>().localPosition = new Vector2(540.75f, currentLevel);
            currentLevel -= 120;
            if(i == currentPlayer)
            {
                rank.color = Color.yellow;
                name.color = Color.yellow;
                score.color = Color.yellow;
            }
            yield return new WaitForSeconds(1f);
        }
	}
}
