using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class PlaylistManager : MonoBehaviour {

	public static PlaylistManager instance ; 

	public AudioClip[] clips; 
	private AudioSource audioSource; 
	public bool paused = false ; 
	private bool resumePlay = false ; 

	private Button disableButton ; 

	// Use this for initialization
	void Awake () {
		audioSource = FindObjectOfType<AudioSource> (); 
		audioSource.loop = false;
		disableButton = GameObject.FindGameObjectWithTag ("MusicToggle").GetComponent<Button> (); 

		disableButton.GetComponentInChildren<Text> ().text = "Disable Music"; 

		GameObject[] objs = GameObject.FindGameObjectsWithTag ("GameMusic"); 
		if (objs.Length > 1) {
			Destroy (this.gameObject); 
		}
		DontDestroyOnLoad (this.gameObject); 
		instance = this; 
	}

	private AudioClip getRandomSong() { 
		return clips[Random.Range(0, clips.Length)] ; 
	}

	public void pauseMusic() {
		
		audioSource.Pause();
		changeButtonText (); 
		paused = !paused; 
		if (!resumePlay) {
			resumePlay = true; 
		}
	}

	public void changeButtonText() {
		if (!paused) {
			disableButton.GetComponentInChildren<Text> ().text = "Disable Music"; 
		} else {
			disableButton.GetComponentInChildren<Text> ().text = "Enable Music"; 
		}
	}

	
	// Update is called once per frame
	void Update () {
		
		if (!audioSource.isPlaying && !paused) {
			if (resumePlay) {
				resumePlay = !resumePlay;
			} else {
				audioSource.clip = getRandomSong (); 
			}
			audioSource.Play (); 
		}
		try { 
			disableButton = GameObject.FindGameObjectWithTag ("MusicToggle").GetComponent<Button> ();
			changeButtonText() ; 		
		}
		catch (System.NullReferenceException e) {
			//Do Nothing
		}

	}
}
