using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
	//beacuse score will be managed in multiple scenes we have a static score manager
	public static int score1; //player one score
	public static int score2; //player two score
	public Text scoreText1, scoreText2; //player score Texts, different for every scene
	AudioSource aud;
	public AudioClip win;
	//timer stuff
	public bool timerRunning;
	public Text timer;
	// Use this for initialization
	void Start ()
	{
        aud = GameObject.Find("Player1").GetComponent<AudioSource>();
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
			if (score1 <= 0 || score2 <= 0)
			{
				break;
			}
		}
		aud.PlayOneShot(win);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(4);

	}
}
