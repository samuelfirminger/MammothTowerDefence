using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script called whenever a button enables a user to quit the game fully
public class ButtonExitProgram : MonoBehaviour {
    void Start() {
        Time.timeScale = 1;
    }
    void Update() { }
    public void ExitPress()
    { 
            Application.Quit();

    }
}
