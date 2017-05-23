using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 

//this script is attached to each turret shop button to hold, display
//and remove the buttons
public class ShopInfo : MonoBehaviour {


	//get box (button)
	[Header("Buttons Here")] 
	public GameObject typeButton;
	public GameObject costButton; 


	//get text inside boxes (buttons) 
	[Header("Child Text Of Button")] 
	public GameObject typeName; 
	private Text typeText; 
	public GameObject costValue;
	private Text costText; 

	[Header("Values")]
	public string Name; 
	public int Cost;

	// Use this for initialization
	void Start () {
		typeText = typeName.GetComponent<Text> ();
		costText = costValue.GetComponent<Text> (); 
		typeButton.SetActive (false); 
		costButton.SetActive (false); 
		typeName.SetActive (false); 
		costValue.SetActive (false); 
	}
	
	//get UI info for tower on mouse enter 
	public void getInfo() { 
		setShopName (); 
		setCostValue (); 

		typeButton.SetActive (true); 
		costButton.SetActive (true); 
		typeName.SetActive (true); 
		costValue.SetActive (true); 
	
	}

	//disable UI info for tower on mouse exit
	public void disableInfo() { 
		typeButton.SetActive (false); 
		costButton.SetActive (false); 
		typeName.SetActive (false); 
		costValue.SetActive (false); 
	}


	private void setShopName() {
		typeText.text = Name; 
	}
		
	private void setCostValue() {
		costText.text = "$" + Cost.ToString();  
	}
}
