using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuThread : MonoBehaviour {
	[SerializeField] GameObject[] elements;
	int current = 0;bool toggleState;
	//the entire menu needs to be redone to work with the arcade machine >:(
	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteKey("Tutorial");
		elements[2].transform.parent.gameObject.GetComponent<Toggle>().isOn = false;
			toggleState = false;
    }

	void Update()
	{
		//on press
		if (Input.GetButtonDown("VerticalP1") || Input.GetButtonDown("VerticalP2"))
		{
			//down
			if (current != 2 && (Input.GetAxis("VerticalP1") < 0 || Input.GetAxis("VerticalP2") < 0))
			{
				current++;
			}
			//up
			else if (current != 0 && (Input.GetAxis("VerticalP1") > 0 || Input.GetAxis("VerticalP2") > 0))
			{
				current--;
			}
		}
		//enter
		else if (Input.GetButtonDown("Fire1P1") || Input.GetButtonDown("Fire1P2"))
		{
			print(current);
			switch (current)
			{
				case 0:
					//load level1
					loadLev1();
					break;
				case 1:
					//load lb
					loadLB();
					break;
				case 2:
					//this is the toggle so i need to get the parent
					elements[2].transform.parent.gameObject.GetComponent<Toggle>().isOn = !toggleState;
					toggleState = !toggleState;
					//set tut to approiate value
					ToggleValueChanged();
					break;

			}

		}

		
			

			elements[0].GetComponent<Image>().color = Color.white;
			elements[1].GetComponent<Image>().color = Color.white;
        elements[2].GetComponent<Image>().color = Color.white;
        elements[current].GetComponent<Image>().color = Color.gray;

        

	}

    //Output the new state of the Toggle into PlayerPref
    void ToggleValueChanged()
	{
		if(elements[2].transform.parent.gameObject.GetComponent<Toggle>().isOn)
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
