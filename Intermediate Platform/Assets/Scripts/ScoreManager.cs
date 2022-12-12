using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
	//beacuse score will be managed in multiple scenes we have a static score manager
	public static int score1; //player one score
	public static int score2; //player two score
	public Text scoreText1, scoreText2; //player score Texts, different for every scene

	//timer stuff
	public bool timerRunning;
	public Text timer;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!timerRunning && timer != null)
		{
			StartCoroutine(timeSet(180));
		}

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
	IEnumerator timeSet(float time)
	{
		while (time > 0)
		{
			timerRunning = true;
			time -= Time.deltaTime;
			float minutes = Mathf.FloorToInt(time / 60);
			float seconds = Mathf.FloorToInt(time % 60);
			timer.text = "" + minutes + ":" + seconds;
			yield return new WaitForSeconds(0);
		}

	}
}
