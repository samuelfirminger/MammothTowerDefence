using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;

public class PlaylistManager : MonoBehaviour {

	public static PlaylistManager instance ; 

	public AudioClip[] clips; 
	private AudioSource audioSource; 
	public bool paused = false ; 
	private bool resumePlay = false ; 
	private bool firstSong = true ; 

	private Button disableButton ; 

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this; 
		}
			
		audioSource = FindObjectOfType<AudioSource> (); 
		audioSource.loop = false;
		disableButton = GameObject.FindGameObjectWithTag ("MusicToggle").GetComponent<Button> (); 

		disableButton.GetComponentInChildren<Text> ().text = "Disable Music"; 

		GameObject[] objs = GameObject.FindGameObjectsWithTag ("GameMusic"); 
		if (objs.Length > 1) {
			Destroy (this.gameObject); 
		}
		DontDestroyOnLoad (this.gameObject); 

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
			disableButton.GetComponentInChildren<Text> ().text = "DISABLE MUSIC"; 
		} else {
			disableButton.GetComponentInChildren<Text> ().text = "ENABLE MUSIC"; 
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying && !paused) {
			if (resumePlay) {
				resumePlay = !resumePlay;
			} else {
				if (firstSong) {
					audioSource.clip = clips [0];
					firstSong = false; 
				} else {
					audioSource.clip = getRandomSong ();
				}
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


	public void changeSong() { 
		audioSource.clip = getRandomSong (); 
	}



}
