using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to display an error message if the parser detects an invalid statement
public class ErrorMessage : MonoBehaviour {

    public GameObject errorPanel;

    public void closeError() {
        errorPanel.SetActive(false);
    }
    
    public void showError() {
        errorPanel.SetActive(true);
    }
    
}
