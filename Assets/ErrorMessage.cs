using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMessage : MonoBehaviour {

    public GameObject errorPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void closeError() {
        errorPanel.SetActive(false);
    }
    
    public void showError() {
        errorPanel.SetActive(true);
    }
    
}
