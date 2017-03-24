using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public bool paused ; 

	public GameObject pauseUI;
	private Text pauseText;

	// Use this for initialization
	void Start () {
		paused = false; 
		pauseText = pauseUI.GetComponent<Text>();
	}

	public void pausePress() { 

		paused = !paused ;

		if(paused) {
			pause ();
			pauseText.text = "Play";
		}
		else if(!paused) {
			unpause (); 
			pauseText.text = "Pause";
		}

	}

	public void pause() {
		Time.timeScale = 0; 
	}

	public void unpause() {
		Time.timeScale = 1; 
	}


}
