using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMusic : MonoBehaviour {


	AudioSource audiotoPause ; 

	void Awake() { 
		audiotoPause = GameObject.FindGameObjectWithTag ("GameMusic").GetComponent<AudioSource> (); 
	}

	public void toggleMusic() {
		PlaylistManager.instance.pauseMusic (); 
	}
		
}
