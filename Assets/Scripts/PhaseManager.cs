using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour {

	public static PhaseManager instance ; 

	//get canvas objects to show/disable
	[Header("UI elements on Canvas")]
	public GameObject phaseUI;
	public GameObject pauseButton;
	public GameObject speedButton ; 
	public GameObject gameOver ; 
	public GameObject restart; 
	private bool start;
	GameObject[] turretsInShop; 
	GameObject turretButton ; 

	//get buy/sell button and text 
	[Header("Sell Button")] 
	public GameObject sellUI;
	public GameObject sellButton ; 
	private Text sellText; 

	//turret shop UI 
    public GameObject turretShop ;


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
		sellButton.SetActive (true); 
		turretShop.SetActive (true); 
		speedButton.SetActive (true); 
		turretsInShop = GameObject.FindGameObjectsWithTag ("TurretShop"); 

		//initialise sell button
		sellText = sellUI.GetComponent<Text> ();
		sellText.text = "SELL TURRETS"; 


	}

	public void enableBuildPhase() {
		start = false;
		phaseUI.SetActive(true);
		pauseButton.SetActive (false);
		turretShop.SetActive (true); 
		sellUI.SetActive (true); 
		sellButton.SetActive (true); 

		foreach (GameObject turretButton in turretsInShop) {
			turretButton.SetActive (true); 
		}
	}

	public void startWave() {
		phaseUI.SetActive (false); 
		pauseButton.SetActive (true); 
		turretShop.SetActive (false); 
		sellUI.SetActive (false); 
		sellButton.SetActive (false); 

		start = true;
       // Sound.instance.newWaveSound();
		foreach (GameObject turretButton in turretsInShop) {
			turretButton.SetActive (false); 
		}
		TurretManager.instance.chooseTurretToBuild (null); 
		EnemySpawnManager.instance.setWaveStart(true);

	}
	
	public void disablePhase() {
		phaseUI.SetActive(false); 
	}

	public void gameOverPrompt() {
        Sound.instance.gameOverSound();
		gameOver.SetActive (true);
		restart.SetActive (true); 
		pauseButton.SetActive (false); 
	}

	public void intoSellMode() {
		if (TurretManager.instance.getSellState() == true) {
			sellText.text = "BUY TURRETS";
		} else {
			sellText.text = "SELL TURRETS"; 
		}
	}
		

	public bool getStartState() {
		return start;
	}

	public void setStartState(bool state) {
		start = state; 
	}
	
		
}
