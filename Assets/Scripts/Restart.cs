using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	public static Restart instance ; 
	public GameObject restart; 

	// Use this for initialization
	void Start () {
		restart.SetActive (false); 
	}
	
	public void restartLevel() { 
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex); 
	}
}
