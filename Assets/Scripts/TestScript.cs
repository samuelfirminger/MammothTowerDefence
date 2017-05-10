using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    BriefingManager briefingManager;
    GameObject[] cardsLevel1, cardsLevel2, cardsLevel3;
    
	// Use this for initialization
	void Awake () {
		briefingManager = BriefingManager.instance;
        cardsLevel1 = GameObject.FindGameObjectsWithTag("CardLevel1");
        hideCards(cardsLevel1);
        cardsLevel2 = GameObject.FindGameObjectsWithTag("CardLevel2");
        hideCards(cardsLevel2);
        cardsLevel3 = GameObject.FindGameObjectsWithTag("CardLevel3"); 
        hideCards(cardsLevel3);
        
        //Debug.Log("L1: " + cardsLevel1.Length);
        //Debug.Log("L2: " + cardsLevel2.Length);
        //Debug.Log("L3: " + cardsLevel3.Length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
       
    public void getCards1() {
        //Dynamically generating cards: doesnt work, panels wont become children of cardHolder
        /*briefingManager.createCard("Malware", briefingManager.portrait0, 1f, 
                    2f, ".jpg", false, 
                    "Suspicious");*/     
        
        hideCards(cardsLevel2);
        hideCards(cardsLevel3);
        showCards(cardsLevel1);
    }   
    
    public void getCards2() {
        hideCards(cardsLevel1);
        hideCards(cardsLevel3);
        showCards(cardsLevel2);
    }
    
    public void getCards3() {
        hideCards(cardsLevel1);
        hideCards(cardsLevel2);
        showCards(cardsLevel3);
    }  
    
    private void hideCards(GameObject[] cards) {
        foreach (GameObject card in cards) {
            //card.enabled = false;
        }
    }
    
    private void showCards(GameObject[] cards) {
        foreach (GameObject card in cards) {
            //card.enabled = true;
        }
    }
}
