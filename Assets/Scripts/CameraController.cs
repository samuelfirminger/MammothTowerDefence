using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera sideCam;
    public Camera topCam;
    private bool isTopDown;
    
	// Use this for initialization
	void Start () {
        isTopDown = false;
        topCam.enabled = false;
	}
	
	public void cameraPress() {
        if(isTopDown) {
            isTopDown = false;
            topCam.enabled  = false;
            sideCam.enabled = true;
        } else {
            isTopDown = true;
            topCam.enabled  = true;
            sideCam.enabled = false;
        }
    }
}
