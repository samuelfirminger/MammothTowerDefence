using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to hide the image in the story scene
public class Gran : MonoBehaviour {
	public GameObject granImage ;
	// Use this for initialization
	void Awake () {
		granImage.SetActive (false); 
	}
	

}
