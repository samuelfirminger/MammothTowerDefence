using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to delay the appearance of images, particulary the gran image in the story scene
public class ImageDelay : MonoBehaviour {

	public GameObject imageToDelay ;
	public string tagOfImage ; 
	GameObject[] array ; 
	public float delay ; 

	// Use this for initialization
	void Start () {
		array = GameObject.FindGameObjectsWithTag (tagOfImage); 
		StartCoroutine (delayed ()); 
	}

	IEnumerator delayed() {
		foreach (GameObject gran in array) { 
			gran.SetActive (false); 
		}
		yield return new WaitForSeconds (delay); 
		foreach (GameObject gran in array) { 
			gran.SetActive (true); 
		}        
	}

}
