﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {
    //Make Singleton class
    public static PlayerStats instance;
    void Awake() {
		if (instance != null) {
			Debug.Log ("More than one PlayerStats in scene."); 
			return; 
		}  
        instance = this;
    }
    
    public static int Health;
    public int initialHealth = 20;
    public GameObject healthUI;
    private Text healthText;
    private string healthDisplay;
	   
    public static int Cash;
	public int initialCash = 500;
    public GameObject cashUI;
    private Text cashText;
    private string cashDisplay;

	public GameObject waveUI ; 
	private Text waveText ; 

    
	void Start(){
        //Initialse UI access:
        healthText = healthUI.GetComponent<Text>();
        cashText = cashUI.GetComponent<Text>();
		waveText = waveUI.GetComponent<Text> (); 

        //Set initial cash and health values
		Cash = initialCash;
        Health = initialHealth;
        updateCash();
        updateHealth();   
	}   
    
    public void adjustCash(int cashValue) {
        Cash += cashValue;
        updateCash();
    }
    
    public void decreaseHealth(int damageValue) {
        Debug.Log("Inflicting " + damageValue + " to health");
        Health -= damageValue;
        updateHealth();
		//make prompt that game is over, add button to start again 
		if (Health <= 0) {
			PhaseManager.instance.gameOverPrompt (); 
			Time.timeScale = 0; 
			//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex); 
		}
    }
		
    private void updateHealth() {
        healthText.text = "HEALTH: " + Health.ToString();
    }
    
    private void updateCash() {
        cashText.text = "$" + Cash.ToString();
    }

	public void updateWave(int waveNum) {
		waveText.text = "WAVE " + (waveNum+1).ToString(); 
	}
}
