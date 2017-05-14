using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour {

	public void pressExit() {
        //Remove stored turret data from level
        BetweenScenes.clearAllData();
        SceneManager.LoadScene("StartScreen");
    }
}
