using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMusic : MonoBehaviour {

	public void toggleMusic() {
		PlaylistManager.instance.pauseMusic (); 
	}

	public void changeSong() { 
		PlaylistManager.instance.changeSong ();  
	}

		
}
