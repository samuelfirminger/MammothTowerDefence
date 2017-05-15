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
			pauseText.text = "PLAY";
			PhaseManager.instance.speedButton.SetActive (false); 
		}
		else if(!paused) {
			unpause (); 
			pauseText.text = "PAUSE";
			PhaseManager.instance.speedButton.SetActive (true); 
		}

	}

	public void pause() {
		Time.timeScale = 0; 
	}

	public void unpause() {
		Time.timeScale = SpeedSetter.instance.getSpeed (); 
	}


}
