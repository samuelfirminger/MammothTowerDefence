using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
