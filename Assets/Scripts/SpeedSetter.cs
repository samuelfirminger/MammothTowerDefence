using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 

public class SpeedSetter : MonoBehaviour {

	public static SpeedSetter instance ; 

	public GameObject pauseButton ; 
	private Text speedText ; 
	private int speed = 1; 


	void Awake() { 
		if (instance == null) { 
			instance = this; 
		}

		speedText = pauseButton.GetComponent<Text> (); 
		speedText.text = "1x"; 
	}


    public void changeTime()
    {
		switch (speed) {
		case 1:
			speedText.text = "2x";
			Time.timeScale = 2; 
			speed = 2; 
			break;
		case 2: 
			speedText.text = "3x"; 
			Time.timeScale = 3; 
			speed = 3; 
			break; 
		case 3: 
			speedText.text = "1x"; 
			Time.timeScale = 1; 
			speed = 1; 
			break; 
		}
    }


	public int getSpeed() {
		return speed; 
	}
}
