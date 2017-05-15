using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour {

    public GameObject page1;
    public GameObject page2;
    private int pageOn = 1;
    
	void Awake () {
        pageOn = 1;		
	}
	
	void Start () {
        if(pageOn == 1) {
            page2.SetActive(false);
            page1.SetActive(true);    
        } else {
            page2.SetActive(true);
            page1.SetActive(false);  
        }		
	}
    
    public void nextInstruction() {  
        pageOn = 2;
        page1.SetActive(false);
        page2.SetActive(true);
    }
    
    public void previousInstruction() {
        pageOn = 1;
        page2.SetActive(false);
        page1.SetActive(true);
    }
}
