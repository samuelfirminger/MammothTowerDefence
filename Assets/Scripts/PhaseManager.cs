using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 

public class PhaseManager : MonoBehaviour {

	public static PhaseManager instance ; 

	public GameObject phaseUI;
	public GameObject pauseButton; 
	public bool start = false ;

	void Awake() {
		if (instance != null) {
			Debug.Log ("More than one PlayerStats in scene."); 
			return; 
		}  
		instance = this;
	}
		
	// Use this for initialization
	void Start () {
		phaseUI.SetActive(false); 
	}

	public void enablePhase() {
		phaseUI.SetActive(true);
		pauseButton.SetActive (false); 
		Time.timeScale = 0; 
	}

	public void startWave() {
		Time.timeScale = 1; 
		phaseUI.SetActive (false); 
		pauseButton.SetActive (true); 
		start = true; 
	}
	
	public void disablePhase() {
		phaseUI.SetActive(false); 
	}
}
