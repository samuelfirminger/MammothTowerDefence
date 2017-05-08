using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BriefingManager : MonoBehaviour {
    public Sprite portrait0;
    public Sprite portrait1;
    public Sprite portrait2;
    public GameObject briefingPortrait;
    
    private Image portraitImage;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void setBriefingPortrait(int n) {
        switch(n) {
            /*case(0) : briefingPortrait.Sprite = portrait0 ; break;
            case(1) : briefingPortrait.Sprite = portrait1 ; break;
            case(2) : briefingPortrait.Sprite = portrait2 ; break;*/
        }
    }
    
}
