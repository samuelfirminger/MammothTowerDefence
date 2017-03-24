using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

	public GameObject pauseUI;
	private Text pauseText;
	private string pauseDisplay;



	// Use this for initialization
	void Start () {
		pauseText = pauseUI.GetComponent<Text>();
	}

	void pausePressed() { 

	}
    
}
