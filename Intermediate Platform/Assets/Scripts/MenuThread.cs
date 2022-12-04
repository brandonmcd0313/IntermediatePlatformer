using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuThread : MonoBehaviour {
	[SerializeField] Toggle tut;
	//the entire menu needs to be redone to work with the arcade machine >:(
	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteKey("Tutorial");
		tut.isOn = false;
		//Add listener for when the state of the Toggle changes, to take action
        tut.onValueChanged.AddListener(delegate {
            ToggleValueChanged(tut);
        });
    }
	

    //Output the new state of the Toggle into PlayerPref
    void ToggleValueChanged(Toggle change)
	{
		if(tut.isOn)
		{

            PlayerPrefs.SetInt("Tutorial", 1);
        }
		else
		{

            PlayerPrefs.SetInt("Tutorial", 0);
        }
    }

	public void loadLev1()
	{
		SceneManager.LoadScene(1);
	}

	public void loadLB()
	{
		//placeholder
        SceneManager.LoadScene(10);
    }
}
