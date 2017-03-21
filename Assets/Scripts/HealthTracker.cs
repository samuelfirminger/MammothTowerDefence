using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour {
    function Update ()
{
     var wantedPos = Camera.main.WorldToScreenPoint (target.position);
     transform.position = wantedPos;
}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
