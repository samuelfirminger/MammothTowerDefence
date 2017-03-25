using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour {

	public static PhaseManager instance ; 

	public GameObject phaseUI;
	public GameObject pauseButton; 
	public GameObject gameOver ; 
	public GameObject restart; 
	public bool start;
	GameObject[] turretsInShop; 
	GameObject turretButton ; 

	void Awake() {
		if (instance != null) {
			Debug.Log ("More than one PlayerStats in scene."); 
			return; 
		}  
		instance = this;
	}
		
	// Use this for initialization
	void Start () {
		phaseUI.SetActive(true); 
		gameOver.SetActive (false); 
		turretsInShop = GameObject.FindGameObjectsWithTag ("TurretShop"); 
	}

	public void enablePhase() {
		start = false;
		phaseUI.SetActive(true);
		pauseButton.SetActive (false);
		foreach (GameObject turretButton in turretsInShop) {
			turretButton.SetActive (true); 
		}
		Time.timeScale = 0; 
	}

	public void startWave() {
		Time.timeScale = 1; 
		phaseUI.SetActive (false); 
		pauseButton.SetActive (true); 
		start = true; 
		foreach (GameObject turretButton in turretsInShop) {
			turretButton.SetActive (false); 
		}
		TurretManager.instance.chooseTurretToBuild (null); 

	}
	
	public void disablePhase() {
		phaseUI.SetActive(false); 
	}

	public void gameOverPrompt() { 
		gameOver.SetActive (true);
		restart.SetActive (true); 
		pauseButton.SetActive (false); 
	}
		
}
