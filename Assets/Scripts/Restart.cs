using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	public static Restart instance ; 
	public GameObject restart; 

	// Use this for initialization
	void Start () {
		restart.SetActive(false); 
	}

	//Unused but keeping just in case
	void restartLevel() { 
        BriefingManager.instance.resetBriefings();
        BetweenScenes.clearAllData();
		SceneManager.LoadScene(SceneManager.GetActiveScene ().buildIndex); 
	}

	//Use this to switch levels
	public void NextLevelButton(string levelName)
	{   
		BetweenScenes.clearAllData();
		SceneManager.LoadScene(levelName);
	}

}
