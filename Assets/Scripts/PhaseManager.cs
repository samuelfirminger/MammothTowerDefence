using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour {

	public static PhaseManager instance ; 
	public GameObject phaseUI;

	void Awake() {
		if (instance != null) {
			Debug.Log ("More than one PlayerStats in scene."); 
			return; 
		}  
		instance = this;
	}
		
	// Use this for initialization
	void Start () {
		//phaseUI.enabled = true; 
	}
	
	public void disablePhase() {
		//phaseUI.enabled = false; 
	}
}
