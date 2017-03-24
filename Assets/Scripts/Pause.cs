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
			Time.timeScale = 0 ; 
			pauseText.text = "Play";
		}
		else if(!paused) {
			Time.timeScale = 1 ;
			pauseText.text = "Pause";
		}

	}

}
