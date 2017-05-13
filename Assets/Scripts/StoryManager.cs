using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {

	public Text txt; 
	
	void Awake() { 

		txt.text = "fuckin loser";
		TypeWriter typeWriter = GetComponent<TypeWriter> (); 
		typeWriter.twAnimation (txt.text, 0f); 

	}




}
