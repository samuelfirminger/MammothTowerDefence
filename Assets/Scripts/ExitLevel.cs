using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script called for any button that exits a level
public class ExitLevel : MonoBehaviour {
	public void pressExit() {
        //Remove stored turret data from level
        BetweenScenes.clearAllData();
        SceneManager.LoadScene("StartScreen");
    }
}
